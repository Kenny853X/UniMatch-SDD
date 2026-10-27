using UniMatch.Controlador;
using UniMatch.Entidades;
using Xunit;

namespace UniMatch.Tests;

public class RecomendacionGruposTests
{
    public RecomendacionGruposTests() => Limpiar();

    [Fact]
    public void RecomiendaSoloGruposCompatiblesYDisponibles()
    {
        var estudiante = EstudianteBase();
        TListaEstudiante.Agregar(estudiante);
        AgregarGrupo(1, "Primera opción", new TimeOnly(9, 0), new TimeOnly(10, 0));
        AgregarGrupo(2, "Grupo lleno", new TimeOnly(10, 0), new TimeOnly(11, 0), cupo: 1);
        AgregarGrupo(3, "Otra asignatura", new TimeOnly(11, 0), new TimeOnly(12, 0), asignatura: "Matemática");
        TListaEstudiante.Agregar(EstudianteBase(2));
        Assert.True(TListaInscripcion.Registrar(2, 2).Exito);

        var recomendaciones = TRecomendacionGrupos.ParaEstudiante(estudiante.Codigo).ToList();

        var grupo = Assert.Single(recomendaciones);
        Assert.Equal(1, grupo.Codigo);
    }

    [Fact]
    public void ExcluyeInscritoYConflictoDeHorario()
    {
        var estudiante = EstudianteBase();
        TListaEstudiante.Agregar(estudiante);
        AgregarGrupo(1, "Inscrito", new TimeOnly(8, 0), new TimeOnly(9, 0));
        AgregarGrupo(2, "Superpuesto", new TimeOnly(8, 30), new TimeOnly(9, 30));
        AgregarGrupo(3, "Consecutivo", new TimeOnly(9, 0), new TimeOnly(10, 0));
        Assert.True(TListaInscripcion.Registrar(1, 1).Exito);

        var recomendaciones = TRecomendacionGrupos.ParaEstudiante(estudiante).ToList();

        var grupo = Assert.Single(recomendaciones);
        Assert.Equal(3, grupo.Codigo);
    }

    [Fact]
    public void OrdenaPorDiaHoraYNombre()
    {
        var estudiante = EstudianteBase();
        TListaEstudiante.Agregar(estudiante);
        AgregarGrupo(1, "Zeta", new TimeOnly(10, 0), new TimeOnly(11, 0));
        AgregarGrupo(2, "Alfa", new TimeOnly(9, 0), new TimeOnly(10, 0));

        Assert.Equal([2, 1], TRecomendacionGrupos.ParaEstudiante(1).Select(x => x.Codigo));
    }

    [Fact]
    public void DevuelveVacioParaEstudianteInexistenteOSinGrupos()
    {
        Assert.Empty(TRecomendacionGrupos.ParaEstudiante(999));

        TListaEstudiante.Agregar(EstudianteBase());
        Assert.Empty(TRecomendacionGrupos.ParaEstudiante(1));
    }

    private static Estudiante EstudianteBase(int codigo = 1) => new()
    {
        Codigo = codigo, Nombres = "Ana", Apellidos = "Pérez", Carrera = "Sistemas",
        Asignatura = "Programación", DiaDisponible = "Lunes",
        HoraInicio = new TimeOnly(8, 0), HoraFin = new TimeOnly(12, 0)
    };

    private static void AgregarGrupo(
        int codigo,
        string nombre,
        TimeOnly horaInicio,
        TimeOnly horaFin,
        string asignatura = "Programación",
        int cupo = 2)
    {
        Assert.True(TListaGrupoEstudio.Agregar(new GrupoEstudio
        {
            Codigo = codigo, Nombre = nombre, Asignatura = asignatura, Dia = "Lunes",
            HoraInicio = horaInicio, HoraFin = horaFin, Modalidad = "Virtual", CupoMaximo = cupo
        }).Exito);
    }

    private static void Limpiar()
    {
        TListaEstudiante.Lista.Clear();
        TListaGrupoEstudio.Lista.Clear();
        TListaInscripcion.Lista.Clear();
    }
}
