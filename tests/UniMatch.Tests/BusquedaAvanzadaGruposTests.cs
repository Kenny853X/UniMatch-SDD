using UniMatch.Controlador;
using UniMatch.Entidades;
using Xunit;

namespace UniMatch.Tests;

public class BusquedaAvanzadaGruposTests
{
    public BusquedaAvanzadaGruposTests() => Limpiar();

    [Fact]
    public void BuscaPorCadaFiltroIndividual()
    {
        AgregarGrupos();

        Assert.Equal([1, 2], TListaGrupoEstudio.BuscarAvanzado(asignatura: "program").Select(x => x.Codigo));
        Assert.Equal([2], TListaGrupoEstudio.BuscarAvanzado(dia: "martes").Select(x => x.Codigo));
        Assert.Equal([2, 3], TListaGrupoEstudio.BuscarAvanzado(modalidad: "VIRTUAL").Select(x => x.Codigo));
        Assert.Equal([1, 2], TListaGrupoEstudio.BuscarAvanzado(disponibilidad: FiltroDisponibilidad.Disponibles).Select(x => x.Codigo));
        Assert.Equal([3], TListaGrupoEstudio.BuscarAvanzado(disponibilidad: FiltroDisponibilidad.Llenos).Select(x => x.Codigo));
    }

    [Fact]
    public void BuscaConFiltrosCombinados()
    {
        AgregarGrupos();

        Assert.Equal(3, TListaGrupoEstudio.BuscarAvanzado().Count());

        var resultado = TListaGrupoEstudio.BuscarAvanzado(
            asignatura: "PROGRAMACIÓN",
            dia: "Lunes",
            modalidad: "Presencial",
            horaInicio: new TimeOnly(8, 0),
            horaFin: new TimeOnly(12, 0),
            disponibilidad: FiltroDisponibilidad.Disponibles);

        var grupo = Assert.Single(resultado);
        Assert.Equal(1, grupo.Codigo);
    }

    [Fact]
    public void FiltraIntervalosQueContienenCompletamenteLaReunion()
    {
        AgregarGrupos();

        Assert.Equal([1], TListaGrupoEstudio.BuscarAvanzado(horaInicio: new TimeOnly(9, 0), horaFin: new TimeOnly(11, 0)).Select(x => x.Codigo));
        Assert.Empty(TListaGrupoEstudio.BuscarAvanzado(horaInicio: new TimeOnly(9, 30), horaFin: new TimeOnly(10, 30)));
    }

    [Fact]
    public void DevuelveVacioCuandoNoHayCoincidencias()
    {
        AgregarGrupos();

        Assert.Empty(TListaGrupoEstudio.BuscarAvanzado(asignatura: "Química"));
    }

    [Fact]
    public void DevuelveVacioCuandoLaListaEstaVacia()
    {
        Assert.Empty(TListaGrupoEstudio.BuscarAvanzado());
    }

    private static void AgregarGrupos()
    {
        TListaGrupoEstudio.Agregar(new GrupoEstudio
        {
            Codigo = 1, Nombre = "Programación presencial", Asignatura = "Programación",
            Dia = "Lunes", HoraInicio = new TimeOnly(9, 0), HoraFin = new TimeOnly(11, 0),
            Modalidad = "Presencial", CupoMaximo = 2
        });
        TListaGrupoEstudio.Agregar(new GrupoEstudio
        {
            Codigo = 2, Nombre = "Programación virtual", Asignatura = "Programación avanzada",
            Dia = "Martes", HoraInicio = new TimeOnly(10, 0), HoraFin = new TimeOnly(12, 0),
            Modalidad = "Virtual", CupoMaximo = 2
        });
        TListaGrupoEstudio.Agregar(new GrupoEstudio
        {
            Codigo = 3, Nombre = "Matemática llena", Asignatura = "Matemática",
            Dia = "Lunes", HoraInicio = new TimeOnly(14, 0), HoraFin = new TimeOnly(16, 0),
            Modalidad = "Virtual", CupoMaximo = 1
        });
        TListaEstudiante.Agregar(new Estudiante
        {
            Codigo = 1, Nombres = "Ana", Apellidos = "Pérez", Carrera = "Sistemas",
            Asignatura = "Matemática", DiaDisponible = "Lunes",
            HoraInicio = new TimeOnly(8, 0), HoraFin = new TimeOnly(18, 0)
        });
        TListaInscripcion.Registrar(1, 3);
    }

    private static void Limpiar()
    {
        TListaEstudiante.Lista.Clear();
        TListaGrupoEstudio.Lista.Clear();
        TListaInscripcion.Lista.Clear();
    }
}
