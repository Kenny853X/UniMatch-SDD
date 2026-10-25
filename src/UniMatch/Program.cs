using UniMatch.Formularios;
using UniMatch.Controlador;

namespace UniMatch;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        DatosEjemplo.Cargar();
        Application.Run(new FrmPrincipal());
    }
}
