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
}
