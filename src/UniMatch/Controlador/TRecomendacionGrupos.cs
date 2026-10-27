using UniMatch.Entidades;

namespace UniMatch.Controlador;

public static class TRecomendacionGrupos
{
    private static readonly string[] OrdenDias =
        ["Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo"];

    public static IEnumerable<GrupoEstudio> ParaEstudiante(int codigoEstudiante)
    {
        var estudiante = TListaEstudiante.Buscar(codigoEstudiante);
        return estudiante is null ? [] : ParaEstudiante(estudiante);
    }

    public static IEnumerable<GrupoEstudio> ParaEstudiante(Estudiante estudiante)
    {
        var inscritos = TListaInscripcion.PorEstudiante(estudiante.Codigo)
            .Select(inscripcion => inscripcion.CodigoGrupo)
            .ToHashSet();

        return TListaGrupoEstudio.Lista
            .Where(grupo => !inscritos.Contains(grupo.Codigo))
            .Where(grupo => TListaInscripcion.Compatible(estudiante, grupo, out _))
            .Where(grupo => TListaGrupoEstudio.CuposDisponibles(grupo) > 0)
            .Where(grupo => !TCompatibilidadHorario.BuscarConflicto(estudiante, grupo, out _))
            .OrderBy(grupo => PosicionDia(grupo.Dia))
            .ThenBy(grupo => grupo.HoraInicio)
            .ThenBy(grupo => grupo.Nombre, StringComparer.OrdinalIgnoreCase)
            .ToList();
    }

    private static int PosicionDia(string dia)
    {
        var posicion = Array.FindIndex(OrdenDias,
            diaRegistrado => string.Equals(diaRegistrado, dia.Trim(), StringComparison.OrdinalIgnoreCase));
        return posicion < 0 ? OrdenDias.Length : posicion;
    }
}
