namespace UniMatch.Controlador;

public sealed record ResultadoOperacion(bool Exito, string Mensaje)
{
    public static ResultadoOperacion Correcto(string mensaje = "") => new(true, mensaje);
    public static ResultadoOperacion Error(string mensaje) => new(false, mensaje);
}
