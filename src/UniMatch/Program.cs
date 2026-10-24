using UniMatch.Formularios;

namespace UniMatch;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmPrincipal());
    }
}
