namespace UniMatch.Formularios;

partial class FrmParticipantes
{
    private ComboBox cboGrupo = null!;
    private DataGridView grid = null!;
    private Label lblEstado = null!, lblResumen = null!;
    private Button btnCerrar = null!;

    private void InitializeComponent()
    {
        cboGrupo = new() { DropDownStyle = ComboBoxStyle.DropDownList, Dock = DockStyle.Fill };
        grid = new DataGridView { Dock = DockStyle.Fill, AutoGenerateColumns = true };
        lblEstado = Ui.EstadoVacio("Seleccione un grupo para consultar sus participantes.");
        lblResumen = new Label { Dock = DockStyle.Fill, AutoSize = false, Padding = new Padding(18, 8, 18, 8), TextAlign = ContentAlignment.MiddleLeft, ForeColor = Ui.Navy, Font = new Font("Segoe UI", 10F, FontStyle.Bold), BackColor = Ui.LightBlue };
        btnCerrar = Ui.BotonCancelar("Cerrar");
        var selector = new TableLayoutPanel { Dock = DockStyle.Top, Height = 66, Padding = new Padding(24, 12, 24, 8), ColumnCount = 2 };
        selector.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22)); selector.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 78));
        selector.Controls.Add(new Label { Text = "Grupo de estudio", AutoSize = true, Anchor = AnchorStyles.Left, ForeColor = Ui.MutedText }, 0, 0); selector.Controls.Add(cboGrupo, 1, 0);
        var resumen = new Panel { Dock = DockStyle.Bottom, Height = 48, Padding = new Padding(24, 0, 24, 8) }; resumen.Controls.Add(lblResumen);
        var acciones = Ui.BarraAcciones(btnCerrar);
        Controls.Add(Ui.ContenedorTabla(grid, lblEstado)); Controls.Add(acciones); Controls.Add(resumen); Controls.Add(selector); Controls.Add(Ui.Encabezado("Participantes", "Consulta quiénes forman parte del grupo seleccionado."));
        Ui.ConfigurarTabla(grid);
        cboGrupo.SelectedIndexChanged += (_, _) => Cargar(); btnCerrar.Click += (_, _) => Close();
    }
}
