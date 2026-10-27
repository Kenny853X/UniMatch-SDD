namespace UniMatch.Formularios;

partial class FrmEditInscripcion
{
    private ComboBox cboEstudiante = null!, cboGrupo = null!;
    private Label lblDetalle = null!;
    private Button btnGuardar = null!, btnCancelar = null!;

    private void InitializeComponent()
    {
        cboEstudiante = new() { DropDownStyle = ComboBoxStyle.DropDownList, Dock = DockStyle.Fill };
        cboGrupo = new() { DropDownStyle = ComboBoxStyle.DropDownList, Dock = DockStyle.Fill };
        lblDetalle = new Label { Dock = DockStyle.Fill, AutoSize = false, Padding = new Padding(16), ForeColor = Ui.Navy, BackColor = Ui.LightBlue, Font = new Font("Segoe UI", 11F, FontStyle.Bold) };
        btnGuardar = Ui.BotonPrimario("Confirmar inscripción"); btnCancelar = Ui.BotonCancelar();
        var seleccion = Campos(("Estudiante", cboEstudiante), ("Grupo de estudio", cboGrupo));
        var resumen = new GroupBox { Text = "Resumen de compatibilidad", Dock = DockStyle.Fill, ForeColor = Ui.Navy, Padding = new Padding(12), Font = new Font("Segoe UI", 10F, FontStyle.Bold), Controls = { lblDetalle } };
        var cuerpo = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, Padding = new Padding(28, 8, 28, 12) };
        cuerpo.RowStyles.Add(new RowStyle(SizeType.Percent, 48)); cuerpo.RowStyles.Add(new RowStyle(SizeType.Percent, 52));
        cuerpo.Controls.Add(Seccion("Selecciona los registros", seleccion), 0, 0); cuerpo.Controls.Add(resumen, 0, 1);
        var encabezado = Ui.Encabezado("Nueva inscripción", "Verifica los datos del grupo antes de confirmar la inscripción.");
        var acciones = Ui.BarraAcciones(btnGuardar, btnCancelar);
        Controls.Add(cuerpo); Controls.Add(acciones); Controls.Add(encabezado);
        Ui.ConfigurarCampos(this);
        cboEstudiante.SelectedIndexChanged += (_, _) => ActualizarDetalle();
        cboGrupo.SelectedIndexChanged += (_, _) => ActualizarDetalle();
        btnGuardar.Click += Guardar; btnCancelar.Click += (_, _) => DialogResult = DialogResult.Cancel;
    }

    private static GroupBox Seccion(string titulo, Control contenido) => new()
    {
        Text = titulo, Dock = DockStyle.Fill, ForeColor = Ui.Navy, Padding = new Padding(12),
        Font = new Font("Segoe UI", 10F, FontStyle.Bold), Controls = { contenido }
    };

    private static TableLayoutPanel Campos(params (string Etiqueta, Control Control)[] campos)
    {
        var tabla = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = campos.Length, Padding = new Padding(8, 4, 8, 4) };
        tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30)); tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));
        for (var i = 0; i < campos.Length; i++)
        {
            tabla.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / campos.Length));
            tabla.Controls.Add(new Label { Text = campos[i].Etiqueta, AutoSize = true, Anchor = AnchorStyles.Left, ForeColor = Ui.Text, Font = new Font("Segoe UI", 10F) }, 0, i);
            campos[i].Control.Dock = DockStyle.Fill; tabla.Controls.Add(campos[i].Control, 1, i);
        }
        return tabla;
    }
}
