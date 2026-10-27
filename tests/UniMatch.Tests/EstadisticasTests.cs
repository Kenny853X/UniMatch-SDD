using UniMatch.Controlador;
using UniMatch.Entidades;
using Xunit;

namespace UniMatch.Tests;

public class EstadisticasTests
{
    [Fact]
    public void CalculaTotalesCuposYOcupacion()
    {
        var estudiantes = new[] { Estudiante(1), Estudiante(2) };
        var grupos = new[] { Grupo(1, 2), Grupo(2, 3) };
        var inscripciones = new[] { Inscripcion(1, 1), Inscripcion(2, 1), Inscripcion(1, 2) };

        var resumen = TEstadisticas.Resumen(estudiantes, grupos, inscripciones);

        Assert.Equal(2, resumen.TotalEstudiantes);
        Assert.Equal(2, resumen.TotalGrupos);
        Assert.Equal(3, resumen.TotalInscripciones);
        Assert.Equal(5, resumen.CuposMaximos);
        Assert.Equal(3, resumen.CuposOcupados);
        Assert.Equal(2, resumen.CuposDisponibles);
        Assert.Equal(60m, resumen.PorcentajeOcupacion);
        Assert.Equal(1, resumen.GruposLlenos);
    }

    [Fact]
    public void CalculaGruposLlenosYSinParticipantes()
    {
        var grupos = new[] { Grupo(1, 1), Grupo(2, 3), Grupo(3, 2) };
        var inscripciones = new[] { Inscripcion(1, 1) };

        var resumen = TEstadisticas.Resumen([], grupos, inscripciones);

        Assert.Equal(1, resumen.GruposLlenos);
        Assert.Equal(2, resumen.GruposSinParticipantes);
    }

    [Fact]
    public void AgrupaPorAsignaturaYModalidadYOrdenaDemanda()
    {
        var grupos = new[] { Grupo(1, 3, "Programación", "Virtual"), Grupo(2, 2, "Matemática", "Virtual") };
        var inscripciones = new[] { Inscripcion(1, 1), Inscripcion(2, 1), Inscripcion(3, 2) };

        Assert.Equal("Programación", TEstadisticas.InscripcionesPorAsignatura(grupos, inscripciones)[0].Asignatura);
        Assert.Equal(2, TEstadisticas.GruposPorModalidad(grupos)[0].Grupos);
        Assert.Equal(1, TEstadisticas.GruposMasSolicitados(grupos, inscripciones)[0].Codigo);
    }

    [Fact]
    public void ListasVaciasNoDividenPorCero()
    {
        var resumen = TEstadisticas.Resumen([], [], []);

        Assert.Equal(0, resumen.PorcentajeOcupacion);
        Assert.Empty(TEstadisticas.OcupacionPorGrupo([], []));
        Assert.Empty(TEstadisticas.InscripcionesPorAsignatura([], []));
    }

    [Fact]
    public void LasEstadisticasReflejanCambiosEnLasInscripciones()
    {
        var grupos = new List<GrupoEstudio> { Grupo(1, 2) };
        var inscripciones = new List<Inscripcion>();

        Assert.Equal(0, TEstadisticas.Resumen([], grupos, inscripciones).CuposOcupados);
        inscripciones.Add(Inscripcion(1, 1));
        Assert.Equal(1, TEstadisticas.Resumen([], grupos, inscripciones).CuposOcupados);
        inscripciones.Clear();
        Assert.Equal(0, TEstadisticas.Resumen([], grupos, inscripciones).CuposOcupados);
    }

    private static Estudiante Estudiante(int codigo) => new() { Codigo = codigo };
    private static GrupoEstudio Grupo(int codigo, int cupo, string asignatura = "Programación", string modalidad = "Virtual") =>
        new() { Codigo = codigo, Nombre = $"Grupo {codigo}", Asignatura = asignatura, Modalidad = modalidad, CupoMaximo = cupo };
    private static Inscripcion Inscripcion(int estudiante, int grupo) =>
        new() { CodigoEstudiante = estudiante, CodigoGrupo = grupo };
}
