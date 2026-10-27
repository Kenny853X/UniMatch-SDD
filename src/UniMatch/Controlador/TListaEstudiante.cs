using UniMatch.Entidades;

namespace UniMatch.Controlador;

public static class TListaEstudiante
{
    public static List<Estudiante> Lista { get; } = [];
    public static Estudiante? Buscar(int codigo) => Lista.FirstOrDefault(x => x.Codigo == codigo);

    public static ResultadoOperacion Agregar(Estudiante estudiante)
    {
        var validacion = Validar(estudiante, true);
        if (!validacion.Exito) return validacion;
        Lista.Add(estudiante);
        return ResultadoOperacion.Correcto("Estudiante registrado correctamente.");
    }

    public static ResultadoOperacion Modificar(Estudiante estudiante)
    {
        var actual = Buscar(estudiante.Codigo);
        if (actual is null) return ResultadoOperacion.Error("El estudiante seleccionado no existe.");
        var validacion = Validar(estudiante, false);
        if (!validacion.Exito) return validacion;
        if (TListaInscripcion.PorEstudiante(estudiante.Codigo).Any() &&
            !TListaInscripcion.CompatibleConCambios(estudiante, estudiante.Codigo, out var motivo))
            return ResultadoOperacion.Error(motivo);
        if (TCompatibilidadHorario.EstudianteTieneConflictos(estudiante, out motivo))
            return ResultadoOperacion.Error(motivo);
        actual.Nombres = estudiante.Nombres;
        actual.Apellidos = estudiante.Apellidos;
        actual.Carrera = estudiante.Carrera;
        actual.Asignatura = estudiante.Asignatura;
        actual.DiaDisponible = estudiante.DiaDisponible;
        actual.HoraInicio = estudiante.HoraInicio;
        actual.HoraFin = estudiante.HoraFin;
        return ResultadoOperacion.Correcto("Estudiante actualizado correctamente.");
    }

    public static ResultadoOperacion Eliminar(int codigo)
    {
        var estudiante = Buscar(codigo);
        if (estudiante is null) return ResultadoOperacion.Error("El estudiante seleccionado no existe.");
        Lista.Remove(estudiante);
        TListaInscripcion.EliminarDeEstudiante(codigo);
        return ResultadoOperacion.Correcto("Estudiante e inscripciones relacionadas eliminados.");
    }

    private static ResultadoOperacion Validar(Estudiante e, bool nuevo)
    {
        if (e.Codigo <= 0) return ResultadoOperacion.Error("El código debe ser un entero mayor que cero.");
        if (nuevo && Buscar(e.Codigo) is not null) return ResultadoOperacion.Error("El código de estudiante ya existe.");
        if (string.IsNullOrWhiteSpace(e.Nombres) || string.IsNullOrWhiteSpace(e.Apellidos) ||
            string.IsNullOrWhiteSpace(e.Carrera) || string.IsNullOrWhiteSpace(e.Asignatura))
            return ResultadoOperacion.Error("Complete todos los campos obligatorios.");
        if (string.IsNullOrWhiteSpace(e.DiaDisponible)) return ResultadoOperacion.Error("Seleccione un día disponible.");
        if (e.HoraInicio >= e.HoraFin) return ResultadoOperacion.Error("La hora de inicio debe ser anterior a la hora de finalización.");
        e.Nombres = e.Nombres.Trim(); e.Apellidos = e.Apellidos.Trim(); e.Carrera = e.Carrera.Trim(); e.Asignatura = e.Asignatura.Trim(); e.DiaDisponible = e.DiaDisponible.Trim();
        return ResultadoOperacion.Correcto();
    }
}
