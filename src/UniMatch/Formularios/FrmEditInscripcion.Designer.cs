namespace UniMatch.Formularios;

partial class FrmEditInscripcion
{
    private ComboBox cboEstudiante = null!, cboGrupo = null!; private Label lblDetalle = null!; private Button btnGuardar = null!, btnCancelar = null!;
    private void InitializeComponent()
    {
        cboEstudiante = new() { DropDownStyle = ComboBoxStyle.DropDownList, Dock = DockStyle.Fill }; cboGrupo = new() { DropDownStyle = ComboBoxStyle.DropDownList, Dock = DockStyle.Fill };
        lblDetalle = new Label { AutoSize = true, Dock = DockStyle.Fill, Padding = new Padding(0, 10, 0, 10) };         btnGuardar = Ui.Boton("Inscribir"); btnCancelar = Ui.Boton("Cancelar", secundario: true);
        var fields = new TableLayoutPanel { Dock = DockStyle.Top, Padding = new Padding(20), ColumnCount = 2, RowCount = 3, AutoSize = true };
        fields.Controls.Add(new Label { Text = "Estudiante", AutoSize = true }, 0, 0); fields.Controls.Add(cboEstudiante, 1, 0); fields.Controls.Add(new Label { Text = "Grupo de estudio", AutoSize = true }, 0, 1); fields.Controls.Add(cboGrupo, 1, 1); fields.Controls.Add(new Label { Text = "Detalle", AutoSize = true }, 0, 2); fields.Controls.Add(lblDetalle, 1, 2);
        var buttons = new FlowLayoutPanel { Dock = DockStyle.Top, Padding = new Padding(20), AutoSize = true }; buttons.Controls.AddRange([btnGuardar, btnCancelar]); Controls.Add(buttons); Controls.Add(fields); Controls.Add(Ui.Titulo("Registrar inscripción"));
        cboGrupo.SelectedIndexChanged += (_, _) => ActualizarDetalle(); btnGuardar.Click += Guardar; btnCancelar.Click += (_, _) => DialogResult = DialogResult.Cancel;
    }
}
