namespace UniMatch.Formularios;

partial class FrmAdminGrupos
{
    private DataGridView grid = null!;
    private Label lblEstado = null!, lblConteo = null!;
    private TextBox txtBuscar = null!;

    private Button btnBuscar = null!, btnLimpiar = null!,
        btnInsertar = null!, btnModificar = null!,
        btnEliminar = null!, btnParticipantes = null!,
        btnRegresar = null!;

    private void InitializeComponent()
    {
        SuspendLayout();

        grid = new DataGridView
        {
            Dock = DockStyle.Fill,
            AutoGenerateColumns = true
        };

        lblEstado = Ui.EstadoVacio(
            "No hay grupos que coincidan con la búsqueda.");

        lblConteo = Ui.EtiquetaEstado("0 grupos", Ui.Turquoise);

        txtBuscar = new TextBox
        {
            Width = 260,
            PlaceholderText = "Buscar por asignatura...",
            Margin = new Padding(6, 14, 12, 6)
        };

        btnBuscar = Ui.BotonPrimario("Buscar");
        btnLimpiar = Ui.BotonSecundario("Limpiar");

        btnInsertar = Ui.BotonPrimario("Nuevo grupo");
        btnModificar = Ui.BotonSecundario("Modificar");
        btnEliminar = Ui.BotonEliminar();
        btnParticipantes = Ui.BotonSecundario("Ver participantes");
        btnRegresar = Ui.BotonCancelar("Regresar");

        var lblBuscar = new Label
        {
            Text = "Asignatura",
            AutoSize = true,
            ForeColor = Ui.MutedText,
            Margin = new Padding(6, 16, 6, 6)
        };

        var barraBusqueda = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            WrapContents = true,
            FlowDirection = FlowDirection.LeftToRight,
            Padding = new Padding(18, 8, 18, 8),
            BackColor = Ui.Surface
        };

        barraBusqueda.Controls.AddRange(
        [
            lblBuscar,
            txtBuscar,
            btnBuscar,
            btnLimpiar,
            lblConteo
        ]);

        var encabezado = Ui.Encabezado(
            "Grupos de estudio",
            "Consulta horarios, modalidades y disponibilidad de cupos.");

        var acciones = Ui.BarraAcciones(
            btnInsertar,
            btnModificar,
            btnEliminar,
            btnParticipantes,
            btnRegresar);

        Controls.Add(Ui.ContenedorTabla(grid, lblEstado));
        Controls.Add(acciones);
        Controls.Add(barraBusqueda);
        Controls.Add(encabezado);

        Ui.ConfigurarTabla(grid);

        btnBuscar.Click += (_, _) => Cargar(txtBuscar.Text);

        btnLimpiar.Click += (_, _) =>
        {
            txtBuscar.Clear();
            Cargar();
        };

        btnInsertar.Click += (_, _) =>
        {
            using var formulario = new FrmEditGrupo();

            if (formulario.ShowDialog(this) == DialogResult.OK)
                Cargar(txtBuscar.Text);
        };

        btnModificar.Click += (_, _) => Editar();
        btnEliminar.Click += (_, _) => Eliminar();
        btnParticipantes.Click += (_, _) => VerParticipantes();
        btnRegresar.Click += (_, _) => Close();

        ResumeLayout(true);
    }
}