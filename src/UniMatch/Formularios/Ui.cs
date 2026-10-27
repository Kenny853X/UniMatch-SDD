using System.Drawing;

namespace UniMatch.Formularios;

internal static class Ui
{
    public static readonly Color Navy = Color.FromArgb(22, 43, 74);
    public static readonly Color Blue = Color.FromArgb(31, 111, 183);
    public static readonly Color Turquoise = Color.FromArgb(24, 156, 164);
    public static readonly Color LightBlue = Color.FromArgb(232, 242, 252);
    public static readonly Color Green = Color.FromArgb(34, 139, 98);
    public static readonly Color Red = Color.FromArgb(190, 65, 65);
    public static readonly Color Amber = Color.FromArgb(214, 143, 45);
    public static readonly Color Surface = Color.White;
    public static readonly Color Background = Color.FromArgb(245, 248, 252);
    public static readonly Color Text = Color.FromArgb(45, 55, 70);
    public static readonly Color MutedText = Color.FromArgb(95, 105, 120);
    public static readonly Color Border = Color.FromArgb(218, 225, 234);

    public static void Preparar(Form form, string titulo)
    {
        form.Text = titulo;
        form.StartPosition = FormStartPosition.CenterScreen;
        form.MinimumSize = new Size(820, 560);
        form.BackColor = Background;
        form.Font = new Font("Segoe UI", 10F);
        form.AutoScaleMode = AutoScaleMode.Dpi;
        form.AutoSizeMode = AutoSizeMode.GrowOnly;
    }

    public static Label Titulo(string texto) => new()
    {
        Text = texto, AutoSize = true, Font = new Font("Segoe UI", 20F, FontStyle.Bold),
        ForeColor = Navy, Margin = new Padding(0, 0, 0, 4)
    };

    public static Label Subtitulo(string texto) => new()
    {
        Text = texto, AutoSize = true, ForeColor = MutedText,
        Font = new Font("Segoe UI", 10F), Margin = new Padding(0, 0, 0, 12)
    };

    public static Label Seccion(string texto) => new()
    {
        Text = texto, AutoSize = true, ForeColor = Navy,
        Font = new Font("Segoe UI", 12F, FontStyle.Bold), Margin = new Padding(0, 0, 0, 8)
    };

    public static Panel Tarjeta(Control contenido, Color? color = null) => new()
    {
        BackColor = color ?? Surface, Dock = DockStyle.Fill, Padding = new Padding(20),
        Margin = new Padding(8), BorderStyle = BorderStyle.FixedSingle,
        Controls = { contenido }
    };

    public static Label EstadoVacio(string texto) => new()
    {
        Text = texto, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter,
        ForeColor = MutedText, BackColor = Surface,
        Font = new Font("Segoe UI", 11F, FontStyle.Italic), Padding = new Padding(20)
    };

    public static Label EtiquetaEstado(string texto, Color color) => new()
    {
        Text = texto, AutoSize = true, ForeColor = color, BackColor = Color.FromArgb(238, 245, 250),
        Font = new Font("Segoe UI", 9F, FontStyle.Bold), Padding = new Padding(10, 5, 10, 5),
        Margin = new Padding(0, 4, 0, 4)
    };

    public static Button Boton(string texto, bool peligro = false, bool secundario = false) => new()
    {
        Text = texto, AutoSize = false, Height = 40, MinimumSize = new Size(118, 40),
        Padding = new Padding(14, 0, 14, 0), BackColor = peligro ? Red : secundario ? Surface : Blue,
        ForeColor = peligro || !secundario ? Color.White : Navy, FlatStyle = FlatStyle.Flat,
        Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), Cursor = Cursors.Hand,
        FlatAppearance = { BorderSize = 0, MouseOverBackColor = LightBlue }
    };

    public static Button BotonPrimario(string texto) => Boton(texto);
    public static Button BotonSecundario(string texto) => Boton(texto, secundario: true);
    public static Button BotonEliminar(string texto = "Eliminar") => Boton(texto, peligro: true);
    public static Button BotonCancelar(string texto = "Cancelar") => Boton(texto, secundario: true);

    public static Panel Encabezado(string titulo, string descripcion, Control? extra = null)
    {
        var contenido = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, WrapContents = false,
            AutoSize = false, Padding = new Padding(24, 20, 24, 12)
        };
        contenido.Controls.Add(Titulo(titulo));
        contenido.Controls.Add(Subtitulo(descripcion));
        if (extra is not null) contenido.Controls.Add(extra);
        return new Panel { Dock = DockStyle.Top, Height = extra is null ? 92 : 142, BackColor = Background, Controls = { contenido } };
    }

    public static Panel BarraAcciones(params Control[] controles)
    {
        var barra = new FlowLayoutPanel
        {
            Dock = DockStyle.Bottom, Height = 68, Padding = new Padding(20, 12, 20, 12),
            BackColor = Surface, FlowDirection = FlowDirection.LeftToRight, WrapContents = false
        };
        barra.Controls.AddRange(controles);
        return barra;
    }

    public static Panel ContenedorTabla(DataGridView grid, Label estado)
    {
        var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 0, 20, 12), BackColor = Background };
        panel.Controls.Add(grid);
        panel.Controls.Add(estado);
        return panel;
    }

    public static void ConfigurarTabla(DataGridView grid)
    {
        grid.BackgroundColor = Surface; grid.BorderStyle = BorderStyle.FixedSingle;
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
            BackColor = Surface, ForeColor = Text,
            SelectionBackColor = LightBlue, SelectionForeColor = Navy,
            Padding = new Padding(8, 0, 8, 0), NullValue = "-"
        };
        grid.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(248, 250, 253) };
        grid.RowTemplate.Height = 36; grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.AllowUserToResizeRows = false; grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.MultiSelect = false; grid.AllowUserToAddRows = false; grid.ReadOnly = true;
    }

    public static void ConfigurarCampos(Control contenedor)
    {
        foreach (Control control in contenedor.Controls)
        {
            control.Margin = new Padding(6);
            if (control is TextBox or ComboBox or DateTimePicker or NumericUpDown)
            {
                control.BackColor = Surface; control.ForeColor = Navy;
                control.Font = new Font("Segoe UI", 10F);
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
