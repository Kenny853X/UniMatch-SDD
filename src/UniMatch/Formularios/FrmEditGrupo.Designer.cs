#nullable enable
namespace UniMatch.Formularios;

partial class FrmEditGrupo
{
    private System.ComponentModel.IContainer? components;
    private TextBox txtCodigo = null!, txtNombre = null!, txtAsignatura = null!;
    private ComboBox cboDia = null!, cboModalidad = null!;
    private DateTimePicker dtInicio = null!, dtFin = null!;
    private NumericUpDown nudCupo = null!;
    private Button btnGuardar = null!, btnCancelar = null!;

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        txtCodigo = new(); txtNombre = new(); txtAsignatura = new();
        cboDia = new() { DropDownStyle = ComboBoxStyle.DropDownList };
        cboDia.Items.AddRange(["Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo"]);
        cboModalidad = new() { DropDownStyle = ComboBoxStyle.DropDownList };
        cboModalidad.Items.AddRange(["Presencial", "Virtual"]);
        dtInicio = new() { Format = DateTimePickerFormat.Time, ShowUpDown = true };
        dtFin = new() { Format = DateTimePickerFormat.Time, ShowUpDown = true };
        nudCupo = new() { Minimum = 1, Maximum = 1000 };
        btnGuardar = Ui.BotonPrimario("Guardar cambios"); btnCancelar = Ui.BotonCancelar();

        var general = Campos(("Código de grupo", txtCodigo), ("Nombre del grupo", txtNombre), ("Asignatura", txtAsignatura));
        var programacion = Campos(("Día de reunión", cboDia), ("Hora de inicio", dtInicio), ("Hora de finalización", dtFin), ("Modalidad", cboModalidad), ("Cupo máximo", nudCupo));
        var secciones = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, Padding = new Padding(28, 8, 28, 12) };
        secciones.RowStyles.Add(new RowStyle(SizeType.Percent, 42)); secciones.RowStyles.Add(new RowStyle(SizeType.Percent, 58));
        secciones.Controls.Add(Seccion("Información general", general), 0, 0);
        secciones.Controls.Add(Seccion("Programación y capacidad", programacion), 0, 1);
        var encabezado = Ui.Encabezado("Datos del grupo", "Define el horario, modalidad y capacidad del grupo de estudio.");
        var acciones = Ui.BarraAcciones(btnGuardar, btnCancelar);
        Controls.Add(secciones); Controls.Add(acciones); Controls.Add(encabezado);
        Ui.ConfigurarCampos(this);
        btnCancelar.Click += (_, _) => DialogResult = DialogResult.Cancel; btnGuardar.Click += GuardarClick;
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
