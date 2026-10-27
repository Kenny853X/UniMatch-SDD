namespace UniMatch.Formularios;

partial class FrmAdminGrupos
{
    private DataGridView grid = null!;
    private Label lblEstado = null!, lblConteo = null!;
    private TextBox txtBuscar = null!;
    private Button btnBuscar = null!, btnLimpiar = null!, btnInsertar = null!, btnModificar = null!, btnEliminar = null!, btnParticipantes = null!, btnRegresar = null!;

    private void InitializeComponent()
    {
        grid = new DataGridView { Dock = DockStyle.Fill, AutoGenerateColumns = true };
        lblEstado = Ui.EstadoVacio("No hay grupos que coincidan con la búsqueda.");
        lblConteo = Ui.EtiquetaEstado("0 grupos", Ui.Turquoise);
        txtBuscar = new TextBox { Width = 260, PlaceholderText = "Buscar por asignatura..." };
        btnBuscar = Ui.BotonSecundario("Buscar"); btnLimpiar = Ui.BotonSecundario("Limpiar");
        btnInsertar = Ui.BotonPrimario("Nuevo grupo"); btnModificar = Ui.BotonSecundario("Modificar");
        btnEliminar = Ui.BotonEliminar(); btnParticipantes = Ui.BotonSecundario("Ver participantes"); btnRegresar = Ui.BotonCancelar("Regresar");
        var buscar = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 48, Padding = new Padding(0, 0, 0, 8), WrapContents = false };
        buscar.Controls.Add(new Label { Text = "Buscar", AutoSize = true, ForeColor = Ui.MutedText, Padding = new Padding(0, 10, 8, 0) });
        buscar.Controls.Add(txtBuscar); buscar.Controls.Add(btnBuscar); buscar.Controls.Add(btnLimpiar); buscar.Controls.Add(lblConteo);
        var encabezado = Ui.Encabezado("Grupos de estudio", "Consulta horarios, modalidades y disponibilidad de cupos.", buscar);
        var acciones = Ui.BarraAcciones(btnInsertar, btnModificar, btnEliminar, btnParticipantes, btnRegresar);
        Controls.Add(Ui.ContenedorTabla(grid, lblEstado)); Controls.Add(acciones); Controls.Add(encabezado);
        Ui.ConfigurarTabla(grid);
        btnBuscar.Click += (_, _) => Cargar(txtBuscar.Text); btnLimpiar.Click += (_, _) => { txtBuscar.Clear(); Cargar(); };
        btnInsertar.Click += (_, _) => { if (new FrmEditGrupo().ShowDialog(this) == DialogResult.OK) Cargar(txtBuscar.Text); };
        btnModificar.Click += (_, _) => Editar(); btnEliminar.Click += (_, _) => Eliminar(); btnParticipantes.Click += (_, _) => VerParticipantes(); btnRegresar.Click += (_, _) => Close();
    }
}
