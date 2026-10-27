namespace UniMatch.Formularios;

partial class FrmAdminEstudiantes
{
    private DataGridView grid = null!; private Label lblEstado = null!; private Button btnInsertar = null!, btnModificar = null!, btnEliminar = null!, btnRegresar = null!;
    private void InitializeComponent()
    {
        grid = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AutoGenerateColumns = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, AllowUserToAddRows = false };
        lblEstado = Ui.EstadoVacio("No hay estudiantes registrados.");
        btnInsertar = Ui.Boton("Nuevo estudiante"); btnModificar = Ui.Boton("Modificar", secundario: true); btnEliminar = Ui.Boton("Eliminar", peligro: true); btnRegresar = Ui.Boton("Regresar", secundario: true);
        var buttons = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 55, Padding = new Padding(10) }; buttons.Controls.AddRange([btnInsertar, btnModificar, btnEliminar, btnRegresar]);
        Controls.Add(grid); Controls.Add(lblEstado); Controls.Add(buttons); Controls.Add(Ui.Titulo("Administración de estudiantes"));
        Ui.ConfigurarTabla(grid);
        btnInsertar.Click += (_, _) => { if (new FrmEditEstudiante().ShowDialog(this) == DialogResult.OK) Cargar(); };
        btnModificar.Click += (_, _) => Editar(); btnEliminar.Click += (_, _) => Eliminar(); btnRegresar.Click += (_, _) => Close();
    }
}
