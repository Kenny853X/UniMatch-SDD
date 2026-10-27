using UniMatch.Controlador;
using UniMatch.Entidades;
using Xunit;

namespace UniMatch.Tests;

public class ReglasUniMatchTests
{
    public ReglasUniMatchTests() { TListaEstudiante.Lista.Clear(); TListaGrupoEstudio.Lista.Clear(); TListaInscripcion.Lista.Clear(); }

    private static Estudiante EstudianteBase(int codigo = 1, string asignatura = "Programación")
        => new() { Codigo = codigo, Nombres = "Ana", Apellidos = "Pérez", Carrera = "Sistemas", Asignatura = asignatura, DiaDisponible = "Lunes", HoraInicio = new TimeOnly(8, 0), HoraFin = new TimeOnly(12, 0) };

    private static GrupoEstudio GrupoBase(int codigo = 1, int cupo = 2)
        => new() { Codigo = codigo, Nombre = "Grupo A", Asignatura = "Programación", Dia = "Lunes", HoraInicio = new TimeOnly(9, 0), HoraFin = new TimeOnly(11, 0), Modalidad = "Virtual", CupoMaximo = cupo };

    [Fact]
    public void RegistraInscripcionCompatibleYCalculaCupo()
    {
        TListaEstudiante.Agregar(EstudianteBase()); TListaGrupoEstudio.Agregar(GrupoBase());
        var resultado = TListaInscripcion.Registrar(1, 1);
        Assert.True(resultado.Exito);
        Assert.Equal(1, TListaGrupoEstudio.CuposDisponibles(TListaGrupoEstudio.Buscar(1)!));
    }

    [Fact]
    public void RechazaDuplicadoYSinCupo()
    {
        TListaEstudiante.Agregar(EstudianteBase()); TListaGrupoEstudio.Agregar(GrupoBase(cupo: 1));
        Assert.True(TListaInscripcion.Registrar(1, 1).Exito);
        Assert.False(TListaInscripcion.Registrar(1, 1).Exito);
        TListaEstudiante.Agregar(EstudianteBase(2)); Assert.False(TListaInscripcion.Registrar(2, 1).Exito);
    }

    [Fact]
    public void RechazaAsignaturaYHorarioIncompatibles()
    {
        TListaEstudiante.Agregar(EstudianteBase(asignatura: "Matemática")); TListaGrupoEstudio.Agregar(GrupoBase());
        var resultado = TListaInscripcion.Registrar(1, 1);
        Assert.False(resultado.Exito);
        Assert.Contains("asignatura", resultado.Mensaje, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void EliminarRegistroEliminaRelacionesSinAfectarOtros()
    {
        TListaEstudiante.Agregar(EstudianteBase()); TListaEstudiante.Agregar(EstudianteBase(2)); TListaGrupoEstudio.Agregar(GrupoBase());
        TListaInscripcion.Registrar(1, 1); TListaInscripcion.Registrar(2, 1);
        Assert.True(TListaEstudiante.Eliminar(1).Exito);
        Assert.Single(TListaInscripcion.Lista);
        Assert.Equal(2, TListaInscripcion.Lista[0].CodigoEstudiante);
    }

    [Fact]
    public void RechazaReducirCupoPorDebajoDeParticipantes()
    {
        TListaEstudiante.Agregar(EstudianteBase()); TListaGrupoEstudio.Agregar(GrupoBase(cupo: 2)); TListaInscripcion.Registrar(1, 1);
        var grupo = GrupoBase(cupo: 0); grupo.Codigo = 1;
        Assert.False(TListaGrupoEstudio.Modificar(grupo).Exito);
        Assert.Equal(2, TListaGrupoEstudio.Buscar(1)!.CupoMaximo);
    }

    [Fact]
    public void AceptaDisponibilidadExacta()
    {
        var estudiante = EstudianteBase(); estudiante.HoraInicio = new TimeOnly(9, 0); estudiante.HoraFin = new TimeOnly(11, 0);
        TListaEstudiante.Agregar(estudiante); TListaGrupoEstudio.Agregar(GrupoBase());
        Assert.True(TListaInscripcion.Registrar(1, 1).Exito);
    }

    [Fact]
    public void RechazaInscripcionConHorarioSuperpuesto()
    {
        TListaEstudiante.Agregar(EstudianteBase());
        TListaGrupoEstudio.Agregar(GrupoBase(1));
        var segundo = GrupoBase(2);
        segundo.Nombre = "Grupo superpuesto";
        segundo.HoraInicio = new TimeOnly(10, 30);
        segundo.HoraFin = new TimeOnly(11, 30);
        TListaGrupoEstudio.Agregar(segundo);

        Assert.True(TListaInscripcion.Registrar(1, 1).Exito);
        var resultado = TListaInscripcion.Registrar(1, 2);

        Assert.False(resultado.Exito);
        Assert.Contains("superpuesta", resultado.Mensaje, StringComparison.OrdinalIgnoreCase);
        Assert.Single(TListaInscripcion.Lista);
    }

    [Fact]
    public void AceptaHorariosConsecutivos()
    {
        TListaEstudiante.Agregar(EstudianteBase());
        TListaGrupoEstudio.Agregar(GrupoBase(1));
        var segundo = GrupoBase(2);
        segundo.Nombre = "Grupo consecutivo";
        segundo.HoraInicio = new TimeOnly(11, 0);
        segundo.HoraFin = new TimeOnly(12, 0);
        TListaGrupoEstudio.Agregar(segundo);

        Assert.True(TListaInscripcion.Registrar(1, 1).Exito);
        Assert.True(TListaInscripcion.Registrar(1, 2).Exito);
    }

    [Fact]
    public void HorariosDeDistintosDiasNoSeSuperponenPeroSeValidaDisponibilidad()
    {
        var estudiante = EstudianteBase();
        TListaEstudiante.Agregar(estudiante);
        var grupoLunes = GrupoBase(1);
        TListaGrupoEstudio.Agregar(grupoLunes);
        var grupoMartes = GrupoBase(2);
        grupoMartes.Dia = "Martes";
        TListaGrupoEstudio.Agregar(grupoMartes);

        Assert.False(TCompatibilidadHorario.SeSuperponen(grupoLunes, grupoMartes));
        Assert.True(TListaInscripcion.Registrar(1, 1).Exito);
        var resultado = TListaInscripcion.Registrar(1, 2);

        Assert.False(resultado.Exito);
        Assert.Contains("día disponible", resultado.Mensaje, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void RechazaModificarGrupoSiProvocaConflictoYConservaDatos()
    {
        TListaEstudiante.Agregar(EstudianteBase());
        TListaGrupoEstudio.Agregar(GrupoBase(1));
        var segundo = GrupoBase(2);
        segundo.Nombre = "Grupo consecutivo";
        segundo.HoraInicio = new TimeOnly(11, 0);
        segundo.HoraFin = new TimeOnly(12, 0);
        TListaGrupoEstudio.Agregar(segundo);
        Assert.True(TListaInscripcion.Registrar(1, 1).Exito);
        Assert.True(TListaInscripcion.Registrar(1, 2).Exito);

        var cambio = GrupoBase(2);
        cambio.Nombre = "Cambio incompatible";
        cambio.HoraInicio = new TimeOnly(10, 30);
        cambio.HoraFin = new TimeOnly(11, 30);
        var resultado = TListaGrupoEstudio.Modificar(cambio);

        Assert.False(resultado.Exito);
        Assert.Contains(
    "conflicto",
    resultado.Mensaje,
    StringComparison.OrdinalIgnoreCase);
        Assert.Equal(new TimeOnly(11, 0), TListaGrupoEstudio.Buscar(2)!.HoraInicio);
    }

    [Fact]
    public void AceptaModificarGrupoSinConflictoConsigoMismo()
    {
        TListaEstudiante.Agregar(EstudianteBase());
        var grupo = GrupoBase();
        TListaGrupoEstudio.Agregar(grupo);
        Assert.True(TListaInscripcion.Registrar(1, 1).Exito);

        grupo = GrupoBase();
        grupo.Nombre = "Mismo grupo ajustado";
        grupo.HoraInicio = new TimeOnly(9, 30);
        grupo.HoraFin = new TimeOnly(11, 30);
        var resultado = TListaGrupoEstudio.Modificar(grupo);

        Assert.True(resultado.Exito);
        Assert.Equal("Mismo grupo ajustado", TListaGrupoEstudio.Buscar(1)!.Nombre);
    }

    [Fact]
    public void CargaTodasLasInscripcionesDeEjemplo()
    {
        DatosEjemplo.Cargar();

        Assert.Equal(10, TListaEstudiante.Lista.Count);
        Assert.Equal(6, TListaGrupoEstudio.Lista.Count);
        Assert.Equal(9, TListaInscripcion.Lista.Count);
        Assert.Equal(0, TListaGrupoEstudio.Participantes(105));
        Assert.Equal(0, TListaGrupoEstudio.CuposDisponibles(TListaGrupoEstudio.Buscar(101)!));
    }
}
