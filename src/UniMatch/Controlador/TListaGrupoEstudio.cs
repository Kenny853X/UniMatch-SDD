using UniMatch.Entidades;

namespace UniMatch.Controlador;

public enum FiltroDisponibilidad
{
    Todos,
    Disponibles,
    Llenos
}

public static class TListaGrupoEstudio
{
    public static List<GrupoEstudio> Lista { get; } = [];
    public static GrupoEstudio? Buscar(int codigo) => Lista.FirstOrDefault(x => x.Codigo == codigo);
    public static IEnumerable<GrupoEstudio> BuscarPorAsignatura(string texto) =>
        string.IsNullOrWhiteSpace(texto) ? Lista : Lista.Where(x => x.Asignatura.Contains(texto.Trim(), StringComparison.OrdinalIgnoreCase));

    public static IEnumerable<GrupoEstudio> BuscarAvanzado(
        string? asignatura = null,
        string? dia = null,
        string? modalidad = null,
        TimeOnly? horaInicio = null,
        TimeOnly? horaFin = null,
        FiltroDisponibilidad disponibilidad = FiltroDisponibilidad.Todos)
    {
        if (horaInicio.HasValue && horaFin.HasValue && horaInicio.Value > horaFin.Value)
            return [];

        var consulta = Lista.AsEnumerable();
        if (!string.IsNullOrWhiteSpace(asignatura))
            consulta = consulta.Where(x => x.Asignatura.Contains(asignatura.Trim(), StringComparison.OrdinalIgnoreCase));
        if (!string.IsNullOrWhiteSpace(dia))
            consulta = consulta.Where(x => string.Equals(x.Dia.Trim(), dia.Trim(), StringComparison.OrdinalIgnoreCase));
        if (!string.IsNullOrWhiteSpace(modalidad))
            consulta = consulta.Where(x => string.Equals(x.Modalidad.Trim(), modalidad.Trim(), StringComparison.OrdinalIgnoreCase));
        if (horaInicio.HasValue)
            consulta = consulta.Where(x => x.HoraInicio >= horaInicio.Value);
        if (horaFin.HasValue)
            consulta = consulta.Where(x => x.HoraFin <= horaFin.Value);
        if (disponibilidad == FiltroDisponibilidad.Disponibles)
            consulta = consulta.Where(x => CuposDisponibles(x) > 0);
        else if (disponibilidad == FiltroDisponibilidad.Llenos)
            consulta = consulta.Where(x => CuposDisponibles(x) <= 0);

        return consulta;
    }

    public static ResultadoOperacion Agregar(GrupoEstudio grupo)
    {
        var validacion = Validar(grupo, true);
        if (!validacion.Exito) return validacion;
        Lista.Add(grupo);
        return ResultadoOperacion.Correcto("Grupo registrado correctamente.");
    }

    public static ResultadoOperacion Modificar(GrupoEstudio grupo)
    {
        var actual = Buscar(grupo.Codigo);
        if (actual is null) return ResultadoOperacion.Error("El grupo seleccionado no existe.");
        var validacion = Validar(grupo, false);
        if (!validacion.Exito) return validacion;
        if (!TListaInscripcion.CompatibleConCambios(grupo, out var motivo))
            return ResultadoOperacion.Error(motivo);
        if (!TListaInscripcion.SinConflictosDeHorario(grupo, out motivo))
            return ResultadoOperacion.Error(motivo);
        actual.Nombre = grupo.Nombre; actual.Asignatura = grupo.Asignatura; actual.Dia = grupo.Dia;
        actual.HoraInicio = grupo.HoraInicio; actual.HoraFin = grupo.HoraFin; actual.Modalidad = grupo.Modalidad; actual.CupoMaximo = grupo.CupoMaximo;
        return ResultadoOperacion.Correcto("Grupo actualizado correctamente.");
    }

    public static ResultadoOperacion Eliminar(int codigo)
    {
        var grupo = Buscar(codigo);
        if (grupo is null) return ResultadoOperacion.Error("El grupo seleccionado no existe.");
        Lista.Remove(grupo);
        TListaInscripcion.EliminarDeGrupo(codigo);
        return ResultadoOperacion.Correcto("Grupo e inscripciones relacionadas eliminados.");
    }

    public static int Participantes(int codigo) => TListaInscripcion.PorGrupo(codigo).Count();
    public static int CuposDisponibles(GrupoEstudio grupo) => grupo.CupoMaximo - Participantes(grupo.Codigo);

    private static ResultadoOperacion Validar(GrupoEstudio g, bool nuevo)
    {
        if (g.Codigo <= 0) return ResultadoOperacion.Error("El código debe ser un entero mayor que cero.");
        if (nuevo && Buscar(g.Codigo) is not null) return ResultadoOperacion.Error("El código de grupo ya existe.");
        if (string.IsNullOrWhiteSpace(g.Nombre) || string.IsNullOrWhiteSpace(g.Asignatura))
            return ResultadoOperacion.Error("Complete todos los campos obligatorios.");
        if (string.IsNullOrWhiteSpace(g.Dia) || string.IsNullOrWhiteSpace(g.Modalidad))
            return ResultadoOperacion.Error("Seleccione el día y la modalidad.");
        if (g.HoraInicio >= g.HoraFin) return ResultadoOperacion.Error("La hora de inicio debe ser anterior a la hora de finalización.");
        if (g.CupoMaximo <= 0) return ResultadoOperacion.Error("El cupo máximo debe ser mayor que cero.");
        if (g.CupoMaximo < Participantes(g.Codigo)) return ResultadoOperacion.Error("El cupo no puede ser menor que los participantes inscritos.");
        g.Nombre = g.Nombre.Trim(); g.Asignatura = g.Asignatura.Trim(); g.Dia = g.Dia.Trim(); g.Modalidad = g.Modalidad.Trim();
        return ResultadoOperacion.Correcto();
    }
}
