namespace UniMatch.Formularios;

partial class FrmAdminEstudiantes
{
    private DataGridView grid = null!; private Button btnInsertar = null!, btnModificar = null!, btnEliminar = null!, btnRegresar = null!;
    private void InitializeComponent()
    {
        grid = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AutoGenerateColumns = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, AllowUserToAddRows = false };
        btnInsertar = Ui.Boton("Insertar"); btnModificar = Ui.Boton("Modificar"); btnEliminar = Ui.Boton("Eliminar"); btnRegresar = Ui.Boton("Regresar");
        var buttons = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 55, Padding = new Padding(10) }; buttons.Controls.AddRange([btnInsertar, btnModificar, btnEliminar, btnRegresar]);
        Controls.Add(grid); Controls.Add(buttons); Controls.Add(Ui.Titulo("Administración de estudiantes"));
        btnInsertar.Click += (_, _) => { if (new FrmEditEstudiante().ShowDialog(this) == DialogResult.OK) Cargar(); };
        btnModificar.Click += (_, _) => Editar(); btnEliminar.Click += (_, _) => Eliminar(); btnRegresar.Click += (_, _) => Close();
    }
}
