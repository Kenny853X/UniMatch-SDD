#nullable enable
namespace UniMatch.Formularios;

partial class FrmPrincipal
{
    private System.ComponentModel.IContainer? components;
    private Button btnEstudiantes = null!, btnGrupos = null!, btnInscripciones = null!, btnSalir = null!;
    private Label lblEstudiantes = null!, lblGrupos = null!, lblInscripciones = null!;

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        btnEstudiantes = Ui.Boton("Administrar estudiantes");
        btnGrupos = Ui.Boton("Administrar grupos");
        btnInscripciones = Ui.Boton("Administrar inscripciones");
        btnSalir = Ui.Boton("Salir", peligro: true, secundario: true);
        lblEstudiantes = Contador("0", "Estudiantes registrados");
        lblGrupos = Contador("0", "Grupos de estudio");
        lblInscripciones = Contador("0", "Inscripciones activas");

        var tarjetas = new TableLayoutPanel
        {
            Dock = DockStyle.Top, Height = 142, Padding = new Padding(28, 12, 28, 12),
            ColumnCount = 3, RowCount = 1
        };
        tarjetas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        tarjetas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        tarjetas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
        tarjetas.Controls.Add(Tarjeta(lblEstudiantes, Ui.Blue), 0, 0);
        tarjetas.Controls.Add(Tarjeta(lblGrupos, Ui.Green), 1, 0);
        tarjetas.Controls.Add(Tarjeta(lblInscripciones, Color.FromArgb(208, 126, 40)), 2, 0);

        var acciones = new TableLayoutPanel
        {
            Dock = DockStyle.Top, AutoSize = true, Padding = new Padding(28, 18, 28, 8),
            ColumnCount = 1, RowCount = 4
        };
        acciones.Controls.Add(Seccion("Módulos principales"), 0, 0);
        acciones.Controls.Add(btnEstudiantes, 0, 1);
        acciones.Controls.Add(btnGrupos, 0, 2);
        acciones.Controls.Add(btnInscripciones, 0, 3);
        var pie = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 62, Padding = new Padding(28, 10, 28, 10) };
        pie.Controls.Add(btnSalir);

        Controls.Add(pie); Controls.Add(acciones); Controls.Add(tarjetas);
        Controls.Add(Ui.Titulo("UniMatch | Gestión de grupos de estudio"));
        btnEstudiantes.Click += (_, _) => { using var form = new FrmAdminEstudiantes(); form.ShowDialog(this); ActualizarResumen(); };
        btnGrupos.Click += (_, _) => { using var form = new FrmAdminGrupos(); form.ShowDialog(this); ActualizarResumen(); };
        btnInscripciones.Click += (_, _) => { using var form = new FrmAdminInscripciones(); form.ShowDialog(this); ActualizarResumen(); };
        btnSalir.Click += (_, _) => Close();
    }

    private static Panel Tarjeta(Label contenido, Color color) => new()
    {
        Dock = DockStyle.Fill, BackColor = color, Margin = new Padding(7), Padding = new Padding(16),
        Controls = { contenido }
    };

    private static Label Contador(string valor, string descripcion) => new()
    {
        Text = $"{valor}\n{descripcion}", Dock = DockStyle.Fill, ForeColor = Color.White,
        Font = new Font("Segoe UI", 12F, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft
    };

    private static Label Seccion(string texto) => new()
    {
        Text = texto, AutoSize = true, Font = new Font("Segoe UI", 11F, FontStyle.Bold),
        ForeColor = Ui.Navy, Margin = new Padding(0, 0, 0, 8)
    };
}
