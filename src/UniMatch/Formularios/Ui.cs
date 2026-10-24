using System.Drawing;

namespace UniMatch.Formularios;

internal static class Ui
{
    public static void Preparar(Form form, string titulo)
    {
        form.Text = titulo; form.StartPosition = FormStartPosition.CenterScreen;
        form.MinimumSize = new Size(760, 480); form.BackColor = Color.WhiteSmoke;
        form.Font = new Font("Segoe UI", 10F);
    }

    public static Label Titulo(string texto) => new()
    {
        Text = texto, Dock = DockStyle.Top, Height = 52, Padding = new Padding(20, 12, 0, 0),
        Font = new Font("Segoe UI", 18F, FontStyle.Bold), ForeColor = Color.FromArgb(30, 60, 110)
    };

    public static Button Boton(string texto) => new()
    {
        Text = texto, AutoSize = true, Height = 36, Padding = new Padding(12, 0, 12, 0),
        BackColor = Color.FromArgb(30, 90, 150), ForeColor = Color.White, FlatStyle = FlatStyle.Flat
    };

    public static void Avisar(string mensaje, bool error = false) =>
        MessageBox.Show(mensaje, error ? "Validación" : "UniMatch",
            MessageBoxButtons.OK, error ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
}
