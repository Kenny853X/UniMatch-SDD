using UniMatch.Entidades;

namespace UniMatch.Controlador;

public static class TCompatibilidadHorario
{
    public static bool SeSuperponen(GrupoEstudio primero, GrupoEstudio segundo)
    {
        if (!MismoDia(primero.Dia, segundo.Dia)) return false;
        return primero.HoraInicio < segundo.HoraFin && primero.HoraFin > segundo.HoraInicio;
    }

    public static bool MismoDia(string primero, string segundo) =>
        string.Equals(primero.Trim(), segundo.Trim(), StringComparison.OrdinalIgnoreCase);

    public static bool BuscarConflicto(
        Estudiante estudiante,
        GrupoEstudio nuevoGrupo,
        out GrupoEstudio? grupoConflicto)
    {
        grupoConflicto = TListaInscripcion.PorEstudiante(estudiante.Codigo)
            .Select(inscripcion => TListaGrupoEstudio.Buscar(inscripcion.CodigoGrupo))
            .Where(grupo => grupo is not null && grupo.Codigo != nuevoGrupo.Codigo)
            .FirstOrDefault(grupo => SeSuperponen(grupo!, nuevoGrupo));
        return grupoConflicto is not null;
    }

    public static bool EstudianteTieneConflictos(Estudiante estudiante, out string motivo)
    {
        var grupos = TListaInscripcion.PorEstudiante(estudiante.Codigo)
            .Select(inscripcion => TListaGrupoEstudio.Buscar(inscripcion.CodigoGrupo))
            .Where(grupo => grupo is not null)
            .Cast<GrupoEstudio>()
            .ToList();

        for (var indice = 0; indice < grupos.Count; indice++)
        {
            for (var siguiente = indice + 1; siguiente < grupos.Count; siguiente++)
            {
                if (!SeSuperponen(grupos[indice], grupos[siguiente])) continue;
                motivo = $"El estudiante ya tiene reuniones superpuestas entre " +
                    $"{Describir(grupos[indice])} y {Describir(grupos[siguiente])}.";
                return true;
            }
        }

        motivo = string.Empty;
        return false;
    }

    public static string Describir(GrupoEstudio grupo) =>
        $"'{grupo.Nombre}' (código {grupo.Codigo}, {grupo.Dia.Trim()} de {grupo.HoraInicio:HH\\:mm} a {grupo.HoraFin:HH\\:mm})";
}
