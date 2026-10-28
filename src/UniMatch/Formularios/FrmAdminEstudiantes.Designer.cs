namespace UniMatch.Formularios;

partial class FrmAdminEstudiantes
{
    private DataGridView grid = null!;
    private Label lblEstado = null!, lblConteo = null!;
    private TextBox txtBuscar = null!;

    private Button btnBuscar = null!, btnLimpiar = null!,
        btnInsertar = null!, btnModificar = null!,
        btnEliminar = null!, btnRegresar = null!;

    private void InitializeComponent()
    {
        SuspendLayout();

        grid = new DataGridView
        {
            Dock = DockStyle.Fill,
            AutoGenerateColumns = true
        };

        lblEstado = Ui.EstadoVacio(
            "No hay estudiantes registrados.");

        lblConteo = Ui.EtiquetaEstado("0 estudiantes", Ui.Blue);

        txtBuscar = new TextBox
        {
            Width = 300,
            PlaceholderText = "Nombre, apellido o asignatura...",
            Margin = new Padding(6, 14, 12, 6)
        };

        btnBuscar = Ui.BotonPrimario("Buscar");
        btnLimpiar = Ui.BotonSecundario("Limpiar");

        btnInsertar = Ui.BotonPrimario("Nuevo estudiante");
        btnModificar = Ui.BotonSecundario("Modificar");
        btnEliminar = Ui.BotonEliminar();
        btnRegresar = Ui.BotonCancelar("Regresar");

        var lblBuscar = new Label
        {
            Text = "Buscar",
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
            "Estudiantes",
            "Administra la información académica y la disponibilidad de los estudiantes.");

        var acciones = Ui.BarraAcciones(
            btnInsertar,
            btnModificar,
            btnEliminar,
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
            using var formulario = new FrmEditEstudiante();

            if (formulario.ShowDialog(this) == DialogResult.OK)
                Cargar(txtBuscar.Text);
        };

        btnModificar.Click += (_, _) => Editar();
        btnEliminar.Click += (_, _) => Eliminar();
        btnRegresar.Click += (_, _) => Close();

        ResumeLayout(true);
    }
}