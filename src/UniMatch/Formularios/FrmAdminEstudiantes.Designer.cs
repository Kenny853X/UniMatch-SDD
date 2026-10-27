namespace UniMatch.Formularios;

partial class FrmAdminEstudiantes
{
    private DataGridView grid = null!;
    private Label lblEstado = null!, lblConteo = null!;
    private TextBox txtBuscar = null!;
    private Button btnBuscar = null!, btnLimpiar = null!, btnInsertar = null!, btnModificar = null!, btnEliminar = null!, btnRegresar = null!;

    private void InitializeComponent()
    {
        grid = new DataGridView { Dock = DockStyle.Fill, AutoGenerateColumns = true };
        lblEstado = Ui.EstadoVacio("No hay estudiantes registrados.");
        lblConteo = Ui.EtiquetaEstado("0 estudiantes", Ui.Blue);
        txtBuscar = new TextBox { Width = 260, PlaceholderText = "Nombre, apellido o asignatura..." };
        btnBuscar = Ui.BotonSecundario("Buscar"); btnLimpiar = Ui.BotonSecundario("Limpiar");
        btnInsertar = Ui.BotonPrimario("Nuevo estudiante"); btnModificar = Ui.BotonSecundario("Modificar");
        btnEliminar = Ui.BotonEliminar(); btnRegresar = Ui.BotonCancelar("Regresar");
        var buscar = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 48, Padding = new Padding(0, 0, 0, 8), WrapContents = false };
        buscar.Controls.Add(new Label { Text = "Buscar", AutoSize = true, ForeColor = Ui.MutedText, Padding = new Padding(0, 10, 8, 0) });
        buscar.Controls.Add(txtBuscar); buscar.Controls.Add(btnBuscar); buscar.Controls.Add(btnLimpiar); buscar.Controls.Add(lblConteo);
        var encabezado = Ui.Encabezado("Estudiantes", "Administra la información académica y la disponibilidad de los estudiantes.", buscar);
        var acciones = Ui.BarraAcciones(btnInsertar, btnModificar, btnEliminar, btnRegresar);
        Controls.Add(Ui.ContenedorTabla(grid, lblEstado)); Controls.Add(acciones); Controls.Add(encabezado);
        Ui.ConfigurarTabla(grid);
        btnBuscar.Click += (_, _) => Cargar(txtBuscar.Text);
        btnLimpiar.Click += (_, _) => { txtBuscar.Clear(); Cargar(); };
        btnInsertar.Click += (_, _) => { if (new FrmEditEstudiante().ShowDialog(this) == DialogResult.OK) Cargar(txtBuscar.Text); };
        btnModificar.Click += (_, _) => Editar(); btnEliminar.Click += (_, _) => Eliminar(); btnRegresar.Click += (_, _) => Close();
    }
}
