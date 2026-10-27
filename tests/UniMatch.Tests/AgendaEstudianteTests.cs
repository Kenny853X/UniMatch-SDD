using UniMatch.Controlador;
using UniMatch.Entidades;
using Xunit;

namespace UniMatch.Tests;

public class AgendaEstudianteTests
{
    public AgendaEstudianteTests() => Limpiar();

    [Fact]
    public void ConstruyeAgendaConVariasInscripcionesYCalculaHoras()
    {
        TListaEstudiante.Agregar(EstudianteBase());
        AgregarGrupo(1, "Primera reunión", "Lunes", new TimeOnly(8, 0), new TimeOnly(10, 0));
        AgregarGrupo(2, "Segunda reunión", "Lunes", new TimeOnly(10, 0), new TimeOnly(11, 30));
        Assert.True(TListaInscripcion.Registrar(1, 1).Exito);
        Assert.True(TListaInscripcion.Registrar(1, 2).Exito);

        var agenda = TListaInscripcion.AgendaDeEstudiante(1).ToList();

        Assert.Equal(2, agenda.Count);
        Assert.Equal(3.5, TListaInscripcion.HorasSemanales(1));
    }

    [Fact]
    public void OrdenaPorDiaDeLunesADomingoYDespuesPorHora()
    {
        TListaEstudiante.Agregar(EstudianteBase());
        AgregarGrupo(1, "Tarde del martes", "Martes", new TimeOnly(13, 0), new TimeOnly(14, 0));
        AgregarGrupo(2, "Mañana del lunes", "Lunes", new TimeOnly(10, 0), new TimeOnly(11, 0));
        AgregarGrupo(3, "Mañana del martes", "Martes", new TimeOnly(8, 0), new TimeOnly(9, 0));
        TListaInscripcion.Lista.AddRange([
            new Inscripcion { CodigoEstudiante = 1, CodigoGrupo = 1 },
            new Inscripcion { CodigoEstudiante = 1, CodigoGrupo = 2 },
            new Inscripcion { CodigoEstudiante = 1, CodigoGrupo = 3 }
        ]);

        Assert.Equal([2, 3, 1], TListaInscripcion.AgendaDeEstudiante(1).Select(x => x.CodigoGrupo));
    }

    [Fact]
    public void AgendaSinInscripcionesDevuelveVacio()
    {
        TListaEstudiante.Agregar(EstudianteBase());

        Assert.Empty(TListaInscripcion.AgendaDeEstudiante(1));
        Assert.Equal(0, TListaInscripcion.HorasSemanales(1));
    }

    [Fact]
    public void ActualizaDespuesDeRegistrarYEliminarInscripcion()
    {
        TListaEstudiante.Agregar(EstudianteBase());
        AgregarGrupo(1, "Reunión", "Lunes", new TimeOnly(9, 0), new TimeOnly(11, 0));

        Assert.Empty(TListaInscripcion.AgendaDeEstudiante(1));
        Assert.True(TListaInscripcion.Registrar(1, 1).Exito);
        Assert.Single(TListaInscripcion.AgendaDeEstudiante(1));
        Assert.True(TListaInscripcion.Cancelar(1, 1).Exito);
        Assert.Empty(TListaInscripcion.AgendaDeEstudiante(1));
    }

    [Fact]
    public void NoIncluyeReunionesDeOtroEstudiante()
    {
        TListaEstudiante.Agregar(EstudianteBase());
        TListaEstudiante.Agregar(EstudianteBase(2));
        AgregarGrupo(1, "Grupo uno", "Lunes", new TimeOnly(9, 0), new TimeOnly(10, 0));
        AgregarGrupo(2, "Grupo dos", "Lunes", new TimeOnly(10, 0), new TimeOnly(11, 0));
        TListaInscripcion.Lista.AddRange([
            new Inscripcion { CodigoEstudiante = 1, CodigoGrupo = 1 },
            new Inscripcion { CodigoEstudiante = 2, CodigoGrupo = 2 }
        ]);

        var agenda = TListaInscripcion.AgendaDeEstudiante(1).ToList();

        var reunion = Assert.Single(agenda);
        Assert.Equal(1, reunion.CodigoGrupo);
    }

    [Fact]
    public void ManejaListasVaciasYEstudianteInexistente()
    {
        Assert.Empty(TListaInscripcion.AgendaDeEstudiante(999));
        Assert.Equal(0, TListaInscripcion.HorasSemanales(999));
    }

    private static Estudiante EstudianteBase(int codigo = 1) => new()
    {
        Codigo = codigo,
        Nombres = "Ana",
        Apellidos = "Pérez",
        Carrera = "Sistemas",
        Asignatura = "Programación",
        DiaDisponible = "Lunes",
        HoraInicio = new TimeOnly(8, 0),
        HoraFin = new TimeOnly(18, 0)
    };

    private static void AgregarGrupo(int codigo, string nombre, string dia, TimeOnly inicio, TimeOnly fin)
    {
        Assert.True(TListaGrupoEstudio.Agregar(new GrupoEstudio
        {
            Codigo = codigo,
            Nombre = nombre,
            Asignatura = "Programación",
            Dia = dia,
            HoraInicio = inicio,
            HoraFin = fin,
            Modalidad = "Virtual",
            CupoMaximo = 5
        }).Exito);
    }

    private static void Limpiar()
    {
        TListaEstudiante.Lista.Clear();
        TListaGrupoEstudio.Lista.Clear();
        TListaInscripcion.Lista.Clear();
    }
}
