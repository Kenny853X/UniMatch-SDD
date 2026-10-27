using UniMatch.Entidades;

namespace UniMatch.Controlador;

public sealed record ResumenEstadisticas(
    int TotalEstudiantes,
    int TotalGrupos,
    int TotalInscripciones,
    int CuposMaximos,
    int CuposOcupados,
    int CuposDisponibles,
    decimal PorcentajeOcupacion,
    int GruposLlenos,
    int GruposSinParticipantes);

public sealed record EstadisticaAsignatura(string Asignatura, int Inscripciones);

public sealed record EstadisticaModalidad(string Modalidad, int Grupos);

public sealed record OcupacionGrupo(
    int Codigo,
    string Nombre,
    string Asignatura,
    string Modalidad,
    int CupoMaximo,
    int Inscritos,
    int CuposDisponibles,
    decimal PorcentajeOcupacion,
    string Estado);

public static class TEstadisticas
{
    public static ResumenEstadisticas Resumen() =>
        Resumen(TListaEstudiante.Lista, TListaGrupoEstudio.Lista, TListaInscripcion.Lista);

    public static ResumenEstadisticas Resumen(
        IEnumerable<Estudiante> estudiantes,
        IEnumerable<GrupoEstudio> grupos,
        IEnumerable<Inscripcion> inscripciones)
    {
        var estudiantesLista = estudiantes.ToList();
        var gruposLista = grupos.ToList();
        var inscripcionesLista = inscripciones.ToList();
        var ocupaciones = ObtenerOcupacion(gruposLista, inscripcionesLista);
        var cuposMaximos = gruposLista.Sum(grupo => Math.Max(0, grupo.CupoMaximo));
        var cuposOcupados = ocupaciones.Sum(ocupacion => ocupacion.Inscritos);
        var porcentaje = cuposMaximos == 0 ? 0 : cuposOcupados * 100m / cuposMaximos;

        return new ResumenEstadisticas(
            estudiantesLista.Count,
            gruposLista.Count,
            inscripcionesLista.Count,
            cuposMaximos,
            cuposOcupados,
            ocupaciones.Sum(ocupacion => ocupacion.CuposDisponibles),
            porcentaje,
            ocupaciones.Count(ocupacion => ocupacion.Estado == "Lleno"),
            ocupaciones.Count(ocupacion => ocupacion.Inscritos == 0));
    }

    public static IReadOnlyList<EstadisticaAsignatura> InscripcionesPorAsignatura() =>
        InscripcionesPorAsignatura(TListaGrupoEstudio.Lista, TListaInscripcion.Lista);

    public static IReadOnlyList<EstadisticaAsignatura> InscripcionesPorAsignatura(
        IEnumerable<GrupoEstudio> grupos,
        IEnumerable<Inscripcion> inscripciones)
    {
        var gruposPorCodigo = grupos.ToDictionary(grupo => grupo.Codigo);
        return inscripciones
            .Where(inscripcion => gruposPorCodigo.ContainsKey(inscripcion.CodigoGrupo))
            .GroupBy(inscripcion => gruposPorCodigo[inscripcion.CodigoGrupo].Asignatura)
            .Select(grupo => new EstadisticaAsignatura(grupo.Key, grupo.Count()))
            .OrderByDescending(item => item.Inscripciones)
            .ThenBy(item => item.Asignatura, StringComparer.OrdinalIgnoreCase)
            .ToList();
    }

    public static IReadOnlyList<EstadisticaModalidad> GruposPorModalidad() =>
        GruposPorModalidad(TListaGrupoEstudio.Lista);

    public static IReadOnlyList<EstadisticaModalidad> GruposPorModalidad(
        IEnumerable<GrupoEstudio> grupos) =>
        grupos
            .GroupBy(grupo => grupo.Modalidad)
            .Select(grupo => new EstadisticaModalidad(grupo.Key, grupo.Count()))
            .OrderByDescending(item => item.Grupos)
            .ThenBy(item => item.Modalidad, StringComparer.OrdinalIgnoreCase)
            .ToList();

    public static IReadOnlyList<OcupacionGrupo> GruposMasSolicitados() =>
        GruposMasSolicitados(TListaGrupoEstudio.Lista, TListaInscripcion.Lista);

    public static IReadOnlyList<OcupacionGrupo> GruposMasSolicitados(
        IEnumerable<GrupoEstudio> grupos,
        IEnumerable<Inscripcion> inscripciones) =>
        ObtenerOcupacion(grupos, inscripciones)
            .OrderByDescending(grupo => grupo.Inscritos)
            .ThenBy(grupo => grupo.Nombre, StringComparer.OrdinalIgnoreCase)
            .ToList();

    public static IReadOnlyList<OcupacionGrupo> OcupacionPorGrupo() =>
        OcupacionPorGrupo(TListaGrupoEstudio.Lista, TListaInscripcion.Lista);

    public static IReadOnlyList<OcupacionGrupo> OcupacionPorGrupo(
        IEnumerable<GrupoEstudio> grupos,
        IEnumerable<Inscripcion> inscripciones) =>
        ObtenerOcupacion(grupos, inscripciones)
            .OrderBy(grupo => grupo.Nombre, StringComparer.OrdinalIgnoreCase)
            .ToList();

    private static List<OcupacionGrupo> ObtenerOcupacion(
        IEnumerable<GrupoEstudio> grupos,
        IEnumerable<Inscripcion> inscripciones)
    {
        var inscritosPorGrupo = inscripciones
            .GroupBy(inscripcion => inscripcion.CodigoGrupo)
            .ToDictionary(grupo => grupo.Key, grupo => grupo.Count());

        return grupos.Select(grupo =>
        {
            var inscritos = inscritosPorGrupo.GetValueOrDefault(grupo.Codigo);
            var cupoMaximo = Math.Max(0, grupo.CupoMaximo);
            var disponibles = Math.Max(0, cupoMaximo - inscritos);
            var porcentaje = cupoMaximo == 0 ? 0 : inscritos * 100m / cupoMaximo;
            return new OcupacionGrupo(
                grupo.Codigo,
                grupo.Nombre,
                grupo.Asignatura,
                grupo.Modalidad,
                cupoMaximo,
                inscritos,
                disponibles,
                porcentaje,
                inscritos >= cupoMaximo && cupoMaximo > 0 ? "Lleno" : "Disponible");
        }).ToList();
    }
}
