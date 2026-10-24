namespace UniMatch.Formularios;

partial class FrmAdminGrupos
{
    private DataGridView grid = null!; private TextBox txtBuscar = null!; private Button btnBuscar = null!, btnLimpiar = null!, btnInsertar = null!, btnModificar = null!, btnEliminar = null!, btnParticipantes = null!, btnRegresar = null!;
    private void InitializeComponent()
    {
        grid = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AutoGenerateColumns = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, AllowUserToAddRows = false };
        txtBuscar = new TextBox { Width = 220 }; btnBuscar = Ui.Boton("Buscar"); btnLimpiar = Ui.Boton("Limpiar"); btnInsertar = Ui.Boton("Insertar"); btnModificar = Ui.Boton("Modificar"); btnEliminar = Ui.Boton("Eliminar"); btnParticipantes = Ui.Boton("Ver participantes"); btnRegresar = Ui.Boton("Regresar");
        var search = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 48, Padding = new Padding(10) }; search.Controls.AddRange([new Label { Text = "Asignatura:", AutoSize = true, Padding = new Padding(0, 8, 0, 0) }, txtBuscar, btnBuscar, btnLimpiar]);
        var buttons = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 55, Padding = new Padding(10) }; buttons.Controls.AddRange([btnInsertar, btnModificar, btnEliminar, btnParticipantes, btnRegresar]);
        Controls.Add(grid); Controls.Add(buttons); Controls.Add(search); Controls.Add(Ui.Titulo("Administración de grupos"));
        btnBuscar.Click += (_, _) => Cargar(txtBuscar.Text); btnLimpiar.Click += (_, _) => { txtBuscar.Clear(); Cargar(); };
        btnInsertar.Click += (_, _) => { if (new FrmEditGrupo().ShowDialog(this) == DialogResult.OK) Cargar(); }; btnModificar.Click += (_, _) => Editar(); btnEliminar.Click += (_, _) => Eliminar(); btnParticipantes.Click += (_, _) => VerParticipantes(); btnRegresar.Click += (_, _) => Close();
    }
}
