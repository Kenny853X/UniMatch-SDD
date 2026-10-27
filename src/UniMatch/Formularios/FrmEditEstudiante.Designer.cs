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
        cboDia = new() { DropDownStyle = ComboBoxStyle.DropDownList };
        cboDia.Items.AddRange(["Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo"]);
        dtInicio = new() { Format = DateTimePickerFormat.Time, ShowUpDown = true };
        dtFin = new() { Format = DateTimePickerFormat.Time, ShowUpDown = true };
        btnGuardar = Ui.Boton("Guardar"); btnCancelar = Ui.Boton("Cancelar", secundario: true);

        var fields = new TableLayoutPanel
        {
            Dock = DockStyle.Fill, Padding = new Padding(18), ColumnCount = 2, RowCount = 8,
            AutoSize = false
        };
        fields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34));
        fields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66));
        string[] labels = ["Código", "Nombres", "Apellidos", "Carrera", "Asignatura", "Día disponible", "Hora de inicio", "Hora de finalización"];
        Control[] controls = [txtCodigo, txtNombres, txtApellidos, txtCarrera, txtAsignatura, cboDia, dtInicio, dtFin];
        for (var i = 0; i < labels.Length; i++)
        {
            fields.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            fields.Controls.Add(new Label { Text = labels[i], AutoSize = true, Anchor = AnchorStyles.Left }, 0, i);
            fields.Controls.Add(controls[i], 1, i); controls[i].Dock = DockStyle.Fill;
        }

        var datos = new GroupBox { Text = "Información y disponibilidad", Dock = DockStyle.Fill, Padding = new Padding(4) };
        datos.Controls.Add(fields);
        var buttons = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 62, Padding = new Padding(18, 10, 18, 10) };
        buttons.Controls.AddRange([btnGuardar, btnCancelar]);
        Controls.Add(buttons); Controls.Add(datos); Controls.Add(Ui.Titulo("Datos del estudiante"));
        btnCancelar.Click += (_, _) => DialogResult = DialogResult.Cancel;
        btnGuardar.Click += GuardarClick;
    }
}
