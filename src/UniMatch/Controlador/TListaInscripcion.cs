using UniMatch.Entidades;

namespace UniMatch.Controlador;

public static class TListaInscripcion
{
    public sealed record ReunionAgenda(
        int CodigoGrupo,
        string NombreGrupo,
        string Asignatura,
        string Dia,
        TimeOnly HoraInicio,
        TimeOnly HoraFin,
        string Modalidad);

    public static List<Inscripcion> Lista { get; } = [];

    public static IEnumerable<Inscripcion> PorEstudiante(int codigo) =>
        Lista.Where(x => x.CodigoEstudiante == codigo);

    public static IEnumerable<Inscripcion> PorGrupo(int codigo) =>
        Lista.Where(x => x.CodigoGrupo == codigo);

    public static IEnumerable<ReunionAgenda> AgendaDeEstudiante(int codigoEstudiante) =>
        from inscripcion in PorEstudiante(codigoEstudiante)
        join grupo in TListaGrupoEstudio.Lista on inscripcion.CodigoGrupo equals grupo.Codigo
        orderby OrdenDia(grupo.Dia), grupo.HoraInicio, grupo.HoraFin, grupo.Codigo
        select new ReunionAgenda(
            grupo.Codigo,
            grupo.Nombre,
            grupo.Asignatura,
            grupo.Dia,
            grupo.HoraInicio,
            grupo.HoraFin,
            grupo.Modalidad);

    public static double HorasSemanales(int codigoEstudiante) =>
        AgendaDeEstudiante(codigoEstudiante)
            .Sum(reunion => (reunion.HoraFin - reunion.HoraInicio).TotalHours);

    public static bool Existe(int codigoEstudiante, int codigoGrupo) =>
        Lista.Any(x => x.CodigoEstudiante == codigoEstudiante && x.CodigoGrupo == codigoGrupo);

    public static ResultadoOperacion Registrar(int codigoEstudiante, int codigoGrupo)
    {
        var estudiante = TListaEstudiante.Buscar(codigoEstudiante);
        var grupo = TListaGrupoEstudio.Buscar(codigoGrupo);
        if (estudiante is null || grupo is null)
            return ResultadoOperacion.Error("El estudiante y el grupo deben existir.");
        if (Existe(codigoEstudiante, codigoGrupo))
            return ResultadoOperacion.Error("El estudiante ya está inscrito en este grupo.");
        if (PorGrupo(codigoGrupo).Count() >= grupo.CupoMaximo)
            return ResultadoOperacion.Error("El grupo no tiene cupos disponibles.");
        if (!Compatible(estudiante, grupo, out var motivo))
            return ResultadoOperacion.Error(motivo);
        if (TCompatibilidadHorario.BuscarConflicto(estudiante, grupo, out var grupoConflicto))
        {
            return ResultadoOperacion.Error(
                $"El estudiante ya tiene una reunión superpuesta con {TCompatibilidadHorario.Describir(grupoConflicto!)}.");
        }

        Lista.Add(new Inscripcion { CodigoEstudiante = codigoEstudiante, CodigoGrupo = codigoGrupo });
        return ResultadoOperacion.Correcto("Inscripción registrada correctamente.");
    }

    public static ResultadoOperacion Cancelar(int codigoEstudiante, int codigoGrupo)
    {
        var registro = Lista.FirstOrDefault(x =>
            x.CodigoEstudiante == codigoEstudiante && x.CodigoGrupo == codigoGrupo);
        if (registro is null)
            return ResultadoOperacion.Error("La inscripción seleccionada no existe.");
        Lista.Remove(registro);
        return ResultadoOperacion.Correcto("Inscripción cancelada correctamente.");
    }

    public static bool Compatible(Estudiante estudiante, GrupoEstudio grupo, out string motivo)
    {
        if (!Normalizar(estudiante.Asignatura).Equals(Normalizar(grupo.Asignatura), StringComparison.OrdinalIgnoreCase))
        {
            motivo = "La asignatura del estudiante no coincide con la del grupo.";
            return false;
        }
        if (!Normalizar(estudiante.DiaDisponible).Equals(Normalizar(grupo.Dia), StringComparison.OrdinalIgnoreCase))
        {
            motivo = "El día disponible del estudiante no coincide con el día del grupo.";
            return false;
        }
        if (estudiante.HoraInicio > grupo.HoraInicio || estudiante.HoraFin < grupo.HoraFin)
        {
            motivo = "La disponibilidad del estudiante no cubre completamente el horario del grupo.";
            return false;
        }
        motivo = string.Empty;
        return true;
    }

    public static bool CompatibleConCambios(Estudiante estudiante, int codigoGrupo, out string motivo)
    {
        foreach (var inscripcion in PorEstudiante(estudiante.Codigo))
        {
            var grupo = TListaGrupoEstudio.Buscar(inscripcion.CodigoGrupo);
            if (grupo is not null && !Compatible(estudiante, grupo, out motivo))
                return false;
        }
        motivo = string.Empty;
        return true;
    }

    public static bool CompatibleConCambios(GrupoEstudio grupo, out string motivo)
    {
        foreach (var inscripcion in PorGrupo(grupo.Codigo))
        {
            var estudiante = TListaEstudiante.Buscar(inscripcion.CodigoEstudiante);
            if (estudiante is not null && !Compatible(estudiante, grupo, out motivo))
                return false;
        }
        motivo = string.Empty;
        return true;
    }

    public static bool SinConflictosDeHorario(GrupoEstudio grupo, out string motivo)
    {
        foreach (var inscripcion in PorGrupo(grupo.Codigo))
        {
            var estudiante = TListaEstudiante.Buscar(inscripcion.CodigoEstudiante);
            if (estudiante is null) continue;

            if (TCompatibilidadHorario.BuscarConflicto(estudiante, grupo, out var conflicto))
            {
                motivo = $"El estudiante {estudiante.NombreCompleto} tendría conflicto entre " +
                    $"{TCompatibilidadHorario.Describir(grupo)} y {TCompatibilidadHorario.Describir(conflicto!)}.";
                return false;
            }
        }

        motivo = string.Empty;
        return true;
    }

    public static void EliminarDeEstudiante(int codigo) =>
        Lista.RemoveAll(x => x.CodigoEstudiante == codigo);

    public static void EliminarDeGrupo(int codigo) =>
        Lista.RemoveAll(x => x.CodigoGrupo == codigo);

    private static string Normalizar(string texto) => texto.Trim();

    private static int OrdenDia(string dia) =>
        dia.Trim().ToLowerInvariant() switch
        {
            "lunes" => 1,
            "martes" => 2,
            "miércoles" or "miercoles" => 3,
            "jueves" => 4,
            "viernes" => 5,
            "sábado" or "sabado" => 6,
            "domingo" => 7,
            _ => 8
        };
}
