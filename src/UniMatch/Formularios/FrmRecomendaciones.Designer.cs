#nullable enable
namespace UniMatch.Formularios;

partial class FrmRecomendaciones
{
    private DataGridView grid = null!;
    private ComboBox cboEstudiante = null!;
    private Label lblNombre = null!, lblCarrera = null!, lblAsignatura = null!, lblDisponibilidad = null!;
    private Label lblEstado = null!, lblConteo = null!;
    private Button btnActualizar = null!, btnRegresar = null!;

    private void InitializeComponent()
    {
        cboEstudiante = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
        lblNombre = new Label { Text = "Seleccione un estudiante", AutoSize = true, Font = new Font("Segoe UI", 13F, FontStyle.Bold), ForeColor = Ui.Navy };
        lblCarrera = Info("Carrera: -");
        lblAsignatura = Info("Asignatura: -");
        lblDisponibilidad = Info("Disponibilidad: -");
        lblEstado = Ui.EstadoVacio("No existen grupos compatibles para el estudiante seleccionado.");
        lblConteo = Ui.EtiquetaEstado("0 recomendaciones", Ui.Turquoise);
        grid = new DataGridView { Dock = DockStyle.Fill, AutoGenerateColumns = true };
        btnActualizar = Ui.BotonPrimario("Actualizar");
        btnRegresar = Ui.BotonCancelar("Regresar");

        var selector = new Panel { Dock = DockStyle.Top, Height = 72, Padding = new Padding(24, 10, 24, 10), BackColor = Ui.Surface };
        selector.Controls.Add(new Label { Text = "Estudiante", AutoSize = true, ForeColor = Ui.MutedText, Location = new Point(0, 8) });
        cboEstudiante.Location = new Point(105, 4); cboEstudiante.Width = 430;
        selector.Controls.Add(cboEstudiante);
        selector.Controls.Add(btnActualizar);
        btnActualizar.Location = new Point(555, 0);

        var tarjetaContenido = new TableLayoutPanel
        {
            Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4, Padding = new Padding(4),
            BackColor = Ui.Surface
        };
        tarjetaContenido.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
        for (var i = 1; i < 4; i++) tarjetaContenido.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
        tarjetaContenido.Controls.Add(lblNombre, 0, 0);
        tarjetaContenido.Controls.Add(lblCarrera, 0, 1);
        tarjetaContenido.Controls.Add(lblAsignatura, 0, 2);
        tarjetaContenido.Controls.Add(lblDisponibilidad, 0, 3);
        var tarjeta = Ui.Tarjeta(tarjetaContenido);
        tarjeta.Dock = DockStyle.Top; tarjeta.Height = 142; tarjeta.Margin = new Padding(24, 12, 24, 12);

        var resumen = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 36, Padding = new Padding(24, 0, 24, 4), WrapContents = false };
        resumen.Controls.Add(lblConteo);
        var encabezado = Ui.Encabezado("Recomendaciones inteligentes", "Encuentra grupos compatibles con la asignatura, disponibilidad y agenda del estudiante.");
        var acciones = Ui.BarraAcciones(btnRegresar);

        Controls.Add(Ui.ContenedorTabla(grid, lblEstado));
        Controls.Add(acciones);
        Controls.Add(resumen);
        Controls.Add(tarjeta);
        Controls.Add(selector);
        Controls.Add(encabezado);
        Ui.ConfigurarTabla(grid);

        cboEstudiante.SelectedIndexChanged += (_, _) => ActualizarSeleccion();
        btnActualizar.Click += (_, _) => ActualizarSeleccion();
        btnRegresar.Click += (_, _) => Close();
        Ui.ConfigurarCampos(this);
    }

    private static Label Info(string texto) => new()
    {
        Text = texto, AutoSize = true, ForeColor = Ui.Text, Font = new Font("Segoe UI", 10F)
    };
}
