namespace UniMatch.Entidades;

public sealed class Estudiante
{
    public int Codigo { get; set; }
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Carrera { get; set; } = string.Empty;
    public string Asignatura { get; set; } = string.Empty;
    public string DiaDisponible { get; set; } = string.Empty;
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFin { get; set; }

    public string NombreCompleto => $"{Nombres} {Apellidos}";
}
