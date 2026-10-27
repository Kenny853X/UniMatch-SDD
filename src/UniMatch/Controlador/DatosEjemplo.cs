using UniMatch.Entidades;

namespace UniMatch.Controlador;

public static class DatosEjemplo
{
    public static bool Habilitados { get; set; } = true;
    private static bool cargados;

    public static void Cargar()
    {
        if (!Habilitados) return;
        if (cargados && TListaEstudiante.Lista.Count > 0) return;
        if (TListaEstudiante.Lista.Count > 0 || TListaGrupoEstudio.Lista.Count > 0 || TListaInscripcion.Lista.Count > 0)
            return;

        var estudiantes = new[]
        {
            E(1, "Valentina", "Mendoza", "Ingeniería de Sistemas", "Programación", "Lunes", 8, 12),
            E(2, "Mateo", "Cedeño", "Ingeniería de Sistemas", "Programación", "Lunes", 9, 11),
            E(3, "Camila", "Paredes", "Ingeniería Civil", "Matemática", "Martes", 14, 18),
            E(4, "Santiago", "Vera", "Ingeniería de Sistemas", "Bases de Datos", "Miércoles", 8, 12),
            E(5, "Daniela", "Mora", "Ingeniería Industrial", "Redes", "Jueves", 15, 19),
            E(6, "Nicolás", "León", "Ingeniería de Software", "Programación", "Lunes", 10, 13),
            E(7, "Sofía", "Mendoza", "Ingeniería de Sistemas", "Inteligencia Artificial", "Viernes", 9, 13),
            E(8, "Andrés", "Torres", "Diseño Digital", "Experiencia de Usuario", "Sábado", 8, 12),
            E(9, "María", "Zambrano", "Ingeniería Civil", "Matemática", "Martes", 15, 17),
            E(10, "Joaquín", "Salazar", "Ingeniería de Software", "Bases de Datos", "Miércoles", 10, 14)
        };
        foreach (var estudiante in estudiantes) TListaEstudiante.Agregar(estudiante);

        var grupos = new[]
        {
            G(101, "Programación desde cero", "Programación", "Lunes", 9, 11, "Presencial", 2),
            G(102, "Laboratorio de algoritmos", "Programación", "Lunes", 11, 12, "Virtual", 4),
            G(103, "Cálculo aplicado", "Matemática", "Martes", 15, 17, "Presencial", 3),
            G(104, "Diseño de bases relacionales", "Bases de Datos", "Miércoles", 9, 11, "Virtual", 2),
            G(105, "Redes para principiantes", "Redes", "Jueves", 16, 18, "Presencial", 3),
            G(106, "Introducción a la IA", "Inteligencia Artificial", "Viernes", 10, 12, "Virtual", 2)
        };
        foreach (var grupo in grupos) TListaGrupoEstudio.Agregar(grupo);

        foreach (var (estudiante, grupo) in new[]
        {
            (1, 101), (2, 101), (1, 102), (6, 102),
            (3, 103), (9, 103), (4, 104), (10, 104), (7, 106)
        })
        {
            var resultado = TListaInscripcion.Registrar(estudiante, grupo);
            if (!resultado.Exito)
                throw new InvalidOperationException($"No se pudo cargar la inscripción de ejemplo {estudiante}-{grupo}: {resultado.Mensaje}");
        }

        cargados = true;
    }

    private static Estudiante E(int codigo, string nombres, string apellidos, string carrera, string asignatura, string dia, int inicio, int fin) =>
        new()
        {
            Codigo = codigo, Nombres = nombres, Apellidos = apellidos, Carrera = carrera,
            Asignatura = asignatura, DiaDisponible = dia,
            HoraInicio = new TimeOnly(inicio, 0), HoraFin = new TimeOnly(fin, 0)
        };

    private static GrupoEstudio G(int codigo, string nombre, string asignatura, string dia, int inicio, int fin, string modalidad, int cupo) =>
        new()
        {
            Codigo = codigo, Nombre = nombre, Asignatura = asignatura, Dia = dia,
            HoraInicio = new TimeOnly(inicio, 0), HoraFin = new TimeOnly(fin, 0),
            Modalidad = modalidad, CupoMaximo = cupo
        };
}
