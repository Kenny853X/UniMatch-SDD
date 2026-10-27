#nullable enable
namespace UniMatch.Formularios;

partial class FrmEditEstudiante
{
    private System.ComponentModel.IContainer? components;
    private TextBox txtCodigo = null!, txtNombres = null!, txtApellidos = null!, txtCarrera = null!, txtAsignatura = null!;
    private ComboBox cboDia = null!;
    private DateTimePicker dtInicio = null!, dtFin = null!;
    private Button btnGuardar = null!, btnCancelar = null!;

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        txtCodigo = new(); txtNombres = new(); txtApellidos = new(); txtCarrera = new(); txtAsignatura = new();
        cboDia = new() { DropDownStyle = ComboBoxStyle.DropDownList };
        cboDia.Items.AddRange(["Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo"]);
        dtInicio = new() { Format = DateTimePickerFormat.Time, ShowUpDown = true };
        dtFin = new() { Format = DateTimePickerFormat.Time, ShowUpDown = true };
        btnGuardar = Ui.BotonPrimario("Guardar cambios"); btnCancelar = Ui.BotonCancelar();

        var general = Campos(
            ("Código de estudiante", txtCodigo), ("Nombres", txtNombres), ("Apellidos", txtApellidos),
            ("Carrera", txtCarrera), ("Asignatura de interés", txtAsignatura));
        var disponibilidad = Campos(("Día disponible", cboDia), ("Hora de inicio", dtInicio), ("Hora de finalización", dtFin));
        var secciones = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, Padding = new Padding(28, 8, 28, 12) };
        secciones.RowStyles.Add(new RowStyle(SizeType.Percent, 58)); secciones.RowStyles.Add(new RowStyle(SizeType.Percent, 42));
        secciones.Controls.Add(Seccion("Información académica", general), 0, 0);
        secciones.Controls.Add(Seccion("Disponibilidad horaria", disponibilidad), 0, 1);
        var encabezado = Ui.Encabezado("Datos del estudiante", "Completa la información necesaria para registrar la disponibilidad del estudiante.");
        var acciones = Ui.BarraAcciones(btnGuardar, btnCancelar);
        Controls.Add(secciones); Controls.Add(acciones); Controls.Add(encabezado);
        Ui.ConfigurarCampos(this);
        btnCancelar.Click += (_, _) => DialogResult = DialogResult.Cancel;
        btnGuardar.Click += GuardarClick;
    }

    private static GroupBox Seccion(string titulo, Control contenido) => new()
    {
        Text = titulo, Dock = DockStyle.Fill, ForeColor = Ui.Navy, Padding = new Padding(12),
        Font = new Font("Segoe UI", 10F, FontStyle.Bold), Controls = { contenido }
    };

    private static TableLayoutPanel Campos(params (string Etiqueta, Control Control)[] campos)
    {
        var tabla = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = campos.Length, Padding = new Padding(8, 4, 8, 4) };
        tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34)); tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66));
        for (var i = 0; i < campos.Length; i++)
        {
            tabla.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / campos.Length));
            tabla.Controls.Add(new Label { Text = campos[i].Etiqueta, AutoSize = true, Anchor = AnchorStyles.Left, ForeColor = Ui.Text, Font = new Font("Segoe UI", 10F) }, 0, i);
            campos[i].Control.Dock = DockStyle.Fill; tabla.Controls.Add(campos[i].Control, 1, i);
        }
        return tabla;
    }
}
