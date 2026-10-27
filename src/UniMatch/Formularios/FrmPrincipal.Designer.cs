#nullable enable
namespace UniMatch.Formularios;

partial class FrmPrincipal
{
    private System.ComponentModel.IContainer? components;
    private Label lblBienvenida = null!, lblSeccion = null!, lblGruposResumen = null!, lblActividad = null!;
    private Label lblEstudiantes = null!, lblGrupos = null!, lblInscripciones = null!, lblDisponibles = null!, lblLlenos = null!;
    private Button btnEstudiantes = null!, btnGrupos = null!, btnInscripciones = null!, btnBuscar = null!, btnRecomendaciones = null!, btnAgenda = null!, btnReportes = null!, btnSalir = null!;

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        btnEstudiantes = Ui.BotonSecundario("Estudiantes");
        btnGrupos = Ui.BotonSecundario("Grupos");
        btnInscripciones = Ui.BotonSecundario("Inscripciones");
        btnBuscar = Ui.BotonSecundario("Buscar grupos");
        btnRecomendaciones = Ui.BotonSecundario("Recomendaciones");
        btnAgenda = Ui.BotonSecundario("Mi agenda");
        btnReportes = Ui.BotonSecundario("Reportes");
        btnSalir = Ui.Boton("Salir", peligro: true);
        foreach (var button in new[] { btnEstudiantes, btnGrupos, btnInscripciones, btnBuscar, btnRecomendaciones, btnAgenda, btnReportes })
        {
            button.Width = 210;
            button.TextAlign = ContentAlignment.MiddleCenter;
            button.BackColor = Ui.Navy;
            button.ForeColor = Color.White;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(38, 72, 112);
        }
        btnAgenda.Enabled = false;
        lblEstudiantes = Contador("0", "Estudiantes");
        lblGrupos = Contador("0", "Grupos de estudio");
        lblInscripciones = Contador("0", "Inscripciones");
        lblDisponibles = Contador("0", "Cupos disponibles");
        lblLlenos = Contador("0", "Grupos llenos");
        lblBienvenida = new Label { Text = "Bienvenido a UniMatch", AutoSize = true, Font = new Font("Segoe UI", 10F), ForeColor = Ui.MutedText };
        lblSeccion = Ui.Titulo("Inicio");
        lblGruposResumen = new Label { Dock = DockStyle.Fill, AutoSize = false, Font = new Font("Segoe UI", 10F), ForeColor = Ui.Text, Padding = new Padding(4), Text = "No hay grupos registrados." };
        lblActividad = new Label { Dock = DockStyle.Fill, AutoSize = false, Font = new Font("Segoe UI", 10F), ForeColor = Ui.Text, Padding = new Padding(4), Text = "Aún no hay actividad registrada." };

        var marca = new Panel { Dock = DockStyle.Top, Height = 96, Padding = new Padding(22, 20, 12, 8), BackColor = Ui.Navy };
        marca.Controls.Add(new Label { Text = "UniMatch", AutoSize = true, ForeColor = Color.White, Font = new Font("Segoe UI", 22F, FontStyle.Bold) });
        marca.Controls.Add(new Label { Text = "GESTIÓN UNIVERSITARIA", AutoSize = true, ForeColor = Color.FromArgb(170, 214, 224), Font = new Font("Segoe UI", 8F, FontStyle.Bold), Location = new Point(2, 55) });
        var menu = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, WrapContents = false, Padding = new Padding(8, 18, 8, 12), BackColor = Ui.Navy };
        menu.Controls.Add(new Label { Text = "NAVEGACIÓN", AutoSize = true, ForeColor = Color.FromArgb(160, 185, 210), Font = new Font("Segoe UI", 8F, FontStyle.Bold), Margin = new Padding(10, 0, 0, 8) });
        menu.Controls.AddRange([btnEstudiantes, btnGrupos, btnInscripciones, btnBuscar, btnRecomendaciones, btnAgenda, btnReportes]);
        var sidebar = new Panel { Dock = DockStyle.Left, Width = 238, BackColor = Ui.Navy };
        sidebar.Controls.Add(menu); sidebar.Controls.Add(marca);
        var footer = new Panel { Dock = DockStyle.Bottom, Height = 62, Padding = new Padding(12, 10, 12, 10), BackColor = Ui.Navy };
        btnSalir.Width = 210; footer.Controls.Add(btnSalir);
        sidebar.Controls.Add(footer);

        var encabezado = new Panel { Dock = DockStyle.Top, Height = 88, Padding = new Padding(32, 24, 32, 8), BackColor = Ui.Background };
        encabezado.Controls.Add(lblSeccion); encabezado.Controls.Add(lblBienvenida);
        lblSeccion.Location = new Point(32, 38); lblBienvenida.Location = new Point(34, 14);
        var tarjetas = new TableLayoutPanel { Dock = DockStyle.Top, Height = 132, Padding = new Padding(24, 8, 24, 8), ColumnCount = 5, RowCount = 1 };
        for (var i = 0; i < 5; i++) tarjetas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
        tarjetas.Controls.Add(Ui.Tarjeta(lblEstudiantes, Ui.Blue), 0, 0);
        tarjetas.Controls.Add(Ui.Tarjeta(lblGrupos, Ui.Turquoise), 1, 0);
        tarjetas.Controls.Add(Ui.Tarjeta(lblInscripciones, Ui.Navy), 2, 0);
        tarjetas.Controls.Add(Ui.Tarjeta(lblDisponibles, Ui.Green), 3, 0);
        tarjetas.Controls.Add(Ui.Tarjeta(lblLlenos, Ui.Amber), 4, 0);
        var grupos = new Panel { Dock = DockStyle.Fill, Padding = new Padding(24, 12, 12, 20), BackColor = Ui.Background };
        grupos.Controls.Add(Ui.Tarjeta(lblGruposResumen));
        var actividad = new Panel { Dock = DockStyle.Fill, Padding = new Padding(12, 12, 24, 20), BackColor = Ui.Background };
        actividad.Controls.Add(Ui.Tarjeta(lblActividad));
        var secciones = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1 };
        secciones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62)); secciones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38));
        secciones.Controls.Add(grupos, 0, 0); secciones.Controls.Add(actividad, 1, 0);
        var contenido = new Panel { Dock = DockStyle.Fill, BackColor = Ui.Background };
        contenido.Controls.Add(secciones); contenido.Controls.Add(tarjetas); contenido.Controls.Add(encabezado);
        Controls.Add(contenido); Controls.Add(sidebar);
        btnEstudiantes.Click += (_, _) => { using var form = new FrmAdminEstudiantes(); form.ShowDialog(this); ActualizarResumen(); };
        btnGrupos.Click += (_, _) => { using var form = new FrmAdminGrupos(); form.ShowDialog(this); ActualizarResumen(); };
        btnInscripciones.Click += (_, _) => { using var form = new FrmAdminInscripciones(); form.ShowDialog(this); ActualizarResumen(); };
        btnBuscar.Click += (_, _) => { using var form = new FrmBuscarGrupos(); form.ShowDialog(this); ActualizarResumen(); };
        btnRecomendaciones.Click += (_, _) => { using var form = new FrmRecomendaciones(); form.ShowDialog(this); ActualizarResumen(); };
        btnReportes.Click += (_, _) => { using var form = new FrmReportes(); form.ShowDialog(this); ActualizarResumen(); };
        btnSalir.Click += (_, _) => Close();
    }

    private static Label Contador(string valor, string descripcion) => new()
    {
        Text = $"{valor}\n{descripcion}", Dock = DockStyle.Fill, ForeColor = Color.White,
        Font = new Font("Segoe UI", 12F, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft
    };
}
