#nullable enable
namespace UniMatch.Formularios;

partial class FrmEditEstudiante
{
    private System.ComponentModel.IContainer? components;
    private TextBox txtCodigo = null!, txtNombres = null!, txtApellidos = null!, txtCarrera = null!, txtAsignatura = null!;
    private ComboBox cboDia = null!; private DateTimePicker dtInicio = null!, dtFin = null!;
    private Button btnGuardar = null!, btnCancelar = null!;
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        txtCodigo = new(); txtNombres = new(); txtApellidos = new(); txtCarrera = new(); txtAsignatura = new();
        cboDia = new() { DropDownStyle = ComboBoxStyle.DropDownList }; cboDia.Items.AddRange(["Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo"]);
        dtInicio = new() { Format = DateTimePickerFormat.Time, ShowUpDown = true }; dtFin = new() { Format = DateTimePickerFormat.Time, ShowUpDown = true };
        btnGuardar = Ui.Boton("Guardar"); btnCancelar = Ui.Boton("Cancelar");
        var fields = new TableLayoutPanel { Dock = DockStyle.Top, Padding = new Padding(20), ColumnCount = 2, RowCount = 8, AutoSize = true };
        string[] labels = ["Código", "Nombres", "Apellidos", "Carrera", "Asignatura", "Día disponible", "Hora de inicio", "Hora de finalización"];
        Control[] controls = [txtCodigo, txtNombres, txtApellidos, txtCarrera, txtAsignatura, cboDia, dtInicio, dtFin];
        for (var i = 0; i < labels.Length; i++) { fields.RowStyles.Add(new RowStyle(SizeType.Absolute, 42)); fields.Controls.Add(new Label { Text = labels[i], AutoSize = true, Anchor = AnchorStyles.Left }, 0, i); fields.Controls.Add(controls[i], 1, i); controls[i].Dock = DockStyle.Fill; }
        var buttons = new FlowLayoutPanel { Dock = DockStyle.Top, Padding = new Padding(20), AutoSize = true }; buttons.Controls.AddRange([btnGuardar, btnCancelar]);
        Controls.Add(buttons); Controls.Add(fields); Controls.Add(Ui.Titulo("Datos del estudiante"));
        btnCancelar.Click += (_, _) => DialogResult = DialogResult.Cancel;
        btnGuardar.Click += GuardarClick;
    }
}
