namespace UniMatch.Entidades;

public sealed class GrupoEstudio
{
    public int Codigo { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Asignatura { get; set; } = string.Empty;
    public string Dia { get; set; } = string.Empty;
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFin { get; set; }
    public string Modalidad { get; set; } = string.Empty;
    public int CupoMaximo { get; set; }
}
