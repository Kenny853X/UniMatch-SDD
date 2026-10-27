#nullable enable
namespace UniMatch.Formularios;

partial class FrmEditGrupo
{
    private System.ComponentModel.IContainer? components;
    private TextBox txtCodigo = null!, txtNombre = null!, txtAsignatura = null!; private ComboBox cboDia = null!, cboModalidad = null!;
    private DateTimePicker dtInicio = null!, dtFin = null!; private NumericUpDown nudCupo = null!; private Button btnGuardar = null!, btnCancelar = null!;
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container(); txtCodigo = new(); txtNombre = new(); txtAsignatura = new();
        cboDia = new() { DropDownStyle = ComboBoxStyle.DropDownList }; cboDia.Items.AddRange(["Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo"]);
        cboModalidad = new() { DropDownStyle = ComboBoxStyle.DropDownList }; cboModalidad.Items.AddRange(["Presencial", "Virtual"]);
        dtInicio = new() { Format = DateTimePickerFormat.Time, ShowUpDown = true }; dtFin = new() { Format = DateTimePickerFormat.Time, ShowUpDown = true };
        nudCupo = new() { Minimum = 1, Maximum = 1000 }; btnGuardar = Ui.Boton("Guardar"); btnCancelar = Ui.Boton("Cancelar");
        var fields = new TableLayoutPanel { Dock = DockStyle.Top, Padding = new Padding(20), ColumnCount = 2, RowCount = 8, AutoSize = true };
        string[] labels = ["Código", "Nombre del grupo", "Asignatura", "Día de reunión", "Hora de inicio", "Hora de finalización", "Modalidad", "Cupo máximo"];
        Control[] controls = [txtCodigo, txtNombre, txtAsignatura, cboDia, dtInicio, dtFin, cboModalidad, nudCupo];
        for (var i = 0; i < labels.Length; i++) { fields.RowStyles.Add(new RowStyle(SizeType.Absolute, 42)); fields.Controls.Add(new Label { Text = labels[i], AutoSize = true, Anchor = AnchorStyles.Left }, 0, i); fields.Controls.Add(controls[i], 1, i); controls[i].Dock = DockStyle.Fill; }
        var buttons = new FlowLayoutPanel { Dock = DockStyle.Top, Padding = new Padding(20), AutoSize = true }; buttons.Controls.AddRange([btnGuardar, btnCancelar]);
        Controls.Add(buttons); Controls.Add(fields); Controls.Add(Ui.Titulo("Datos del grupo"));
        btnCancelar.Click += (_, _) => DialogResult = DialogResult.Cancel; btnGuardar.Click += GuardarClick;
    }
}
