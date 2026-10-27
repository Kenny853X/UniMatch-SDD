using System.Drawing;

namespace UniMatch.Formularios;

internal static class Ui
{
    public static readonly Color Navy = Color.FromArgb(24, 62, 98);
    public static readonly Color Blue = Color.FromArgb(38, 112, 180);
    public static readonly Color LightBlue = Color.FromArgb(232, 242, 252);
    public static readonly Color Green = Color.FromArgb(34, 139, 98);
    public static readonly Color Red = Color.FromArgb(190, 65, 65);
    public static readonly Color Border = Color.FromArgb(218, 225, 234);

    public static void Preparar(Form form, string titulo)
    {
        form.Text = titulo; form.StartPosition = FormStartPosition.CenterScreen;
        form.MinimumSize = new Size(760, 480); form.BackColor = Color.FromArgb(247, 249, 252);
        form.Font = new Font("Segoe UI", 10F);
        form.AutoScaleMode = AutoScaleMode.Dpi;
    }

    public static Label Titulo(string texto) => new()
    {
        Text = texto, Dock = DockStyle.Top, Height = 64, Padding = new Padding(28, 16, 0, 0),
        Font = new Font("Segoe UI", 18F, FontStyle.Bold), ForeColor = Navy
    };

    public static Label EstadoVacio(string texto) => new()
    {
        Text = texto, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter,
        ForeColor = Color.FromArgb(95, 105, 120), BackColor = Color.White,
        Font = new Font("Segoe UI", 11F, FontStyle.Italic), Padding = new Padding(20)
    };

    public static Button Boton(string texto, bool peligro = false, bool secundario = false) => new()
    {
        Text = texto, AutoSize = true, Height = 38, MinimumSize = new Size(110, 36),
        Padding = new Padding(14, 0, 14, 0), BackColor = peligro ? Red : secundario ? Color.White : Blue,
        ForeColor = peligro || !secundario ? Color.White : Navy, FlatStyle = FlatStyle.Flat,
        FlatAppearance = { BorderSize = 0 }
    };

    public static void ConfigurarTabla(DataGridView grid)
    {
        grid.BackgroundColor = Color.White; grid.BorderStyle = BorderStyle.None;
        grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        grid.GridColor = Border; grid.RowHeadersVisible = false;
        grid.EnableHeadersVisualStyles = false; grid.ColumnHeadersHeight = 42;
        grid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
        {
            BackColor = Navy, ForeColor = Color.White, Font = new Font("Segoe UI", 10F, FontStyle.Bold),
            Alignment = DataGridViewContentAlignment.MiddleLeft, Padding = new Padding(8, 0, 0, 0)
        };
        grid.DefaultCellStyle = new DataGridViewCellStyle
        {
            BackColor = Color.White, ForeColor = Color.FromArgb(45, 55, 70),
            SelectionBackColor = LightBlue, SelectionForeColor = Navy,
            Padding = new Padding(8, 0, 8, 0), NullValue = "-"
        };
        grid.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(248, 250, 253) };
        grid.RowTemplate.Height = 36; grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.AllowUserToResizeRows = false;
    }

    public static void ConfigurarCampos(Control contenedor)
    {
        foreach (Control control in contenedor.Controls)
        {
            control.Margin = new Padding(6);
            if (control is TextBox or ComboBox or DateTimePicker or NumericUpDown)
            {
                control.BackColor = Color.White; control.ForeColor = Navy;
            }
            if (control.HasChildren) ConfigurarCampos(control);
        }
    }

    public static void Encabezados(DataGridView grid, params (string Nombre, string Encabezado)[] columnas)
    {
        foreach (var (nombre, encabezado) in columnas)
            if (grid.Columns.Contains(nombre)) grid.Columns[nombre].HeaderText = encabezado;
    }

    public static void Avisar(string mensaje, bool error = false) =>
        MessageBox.Show(mensaje, error ? "Validación" : "UniMatch",
            MessageBoxButtons.OK, error ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
}
