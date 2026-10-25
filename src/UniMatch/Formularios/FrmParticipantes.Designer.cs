namespace UniMatch.Formularios;

partial class FrmParticipantes
{
    private ComboBox cboGrupo = null!; private DataGridView grid = null!; private Label lblResumen = null!; private Button btnCerrar = null!;
    private void InitializeComponent()
    {
        cboGrupo = new() { DropDownStyle = ComboBoxStyle.DropDownList, Dock = DockStyle.Fill }; grid = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AutoGenerateColumns = true, AllowUserToAddRows = false };
        lblResumen = new Label { Dock = DockStyle.Bottom, Height = 35, Padding = new Padding(12), Font = new Font("Segoe UI", 10F, FontStyle.Bold) }; btnCerrar = Ui.Boton("Cerrar");
        var top = new TableLayoutPanel { Dock = DockStyle.Top, Height = 50, Padding = new Padding(12), ColumnCount = 2 }; top.Controls.Add(new Label { Text = "Grupo:", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 0); top.Controls.Add(cboGrupo, 1, 0);
        var bottom = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 55, Padding = new Padding(10) }; bottom.Controls.Add(btnCerrar);
        Controls.Add(grid); Controls.Add(lblResumen); Controls.Add(bottom); Controls.Add(top); Controls.Add(Ui.Titulo("Participantes del grupo"));
        Ui.ConfigurarTabla(grid);
        cboGrupo.SelectedIndexChanged += (_, _) => Cargar(); btnCerrar.Click += (_, _) => Close();
    }
}
