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
        cboEstudiante = new ComboBox
        {
            Dock = DockStyle.Fill,
            DropDownStyle = ComboBoxStyle.DropDownList,
            Margin = new Padding(6, 4, 6, 4)
        };
        lblNombre = Info("Seleccione un estudiante", true);
        lblCarrera = Info("Carrera: -");
        lblAsignatura = Info("Asignatura: -");
        lblDisponibilidad = Info("Disponibilidad: -");
        lblEstado = Ui.EstadoVacio("No existen grupos compatibles para el estudiante seleccionado.");
        lblConteo = Ui.EtiquetaEstado("0 recomendaciones", Ui.Turquoise);
        grid = new DataGridView
        {
            Dock = DockStyle.Fill,
            AutoGenerateColumns = true,
            Margin = new Padding(0)
        };
        btnActualizar = Ui.BotonPrimario("Actualizar");
        btnRegresar = Ui.BotonCancelar("Regresar");

        var selector = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 3,
            RowCount = 1,
            Padding = new Padding(18, 8, 18, 8),
            BackColor = Ui.Surface,
            ColumnStyles =
            {
                new ColumnStyle(SizeType.AutoSize),
                new ColumnStyle(SizeType.Percent, 100F),
                new ColumnStyle(SizeType.AutoSize)
            }
        };
        selector.Controls.Add(new Label
        {
            Text = "Estudiante",
            AutoSize = true,
            Anchor = AnchorStyles.Left,
            ForeColor = Ui.MutedText,
            Margin = new Padding(0, 9, 12, 0)
        }, 0, 0);
        selector.Controls.Add(cboEstudiante, 1, 0);
        selector.Controls.Add(btnActualizar, 2, 0);

        var tarjetaContenido = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 4,
            Padding = new Padding(14, 8, 14, 8),
            BackColor = Ui.Surface,
            RowStyles =
            {
                new RowStyle(SizeType.Absolute, 32),
                new RowStyle(SizeType.Percent, 33.33F),
                new RowStyle(SizeType.Percent, 33.33F),
                new RowStyle(SizeType.Percent, 33.34F)
            }
        };
        tarjetaContenido.Controls.Add(lblNombre, 0, 0);
        tarjetaContenido.Controls.Add(lblCarrera, 0, 1);
        tarjetaContenido.Controls.Add(lblAsignatura, 0, 2);
        tarjetaContenido.Controls.Add(lblDisponibilidad, 0, 3);
        var tarjeta = Ui.Tarjeta(tarjetaContenido);
        tarjeta.Margin = new Padding(18, 10, 18, 10);

        var resumen = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 1,
            Padding = new Padding(20, 2, 20, 2),
            BackColor = Ui.Background,
            ColumnStyles =
            {
                new ColumnStyle(SizeType.AutoSize),
                new ColumnStyle(SizeType.Percent, 100F)
            }
        };
        resumen.Controls.Add(lblConteo, 0, 0);
        resumen.Controls.Add(new Label
        {
            Text = "Grupos que cumplen asignatura, disponibilidad, cupo y agenda",
            AutoSize = true,
            Anchor = AnchorStyles.Left,
            ForeColor = Ui.MutedText,
            Margin = new Padding(12, 0, 0, 0),
            TextAlign = ContentAlignment.MiddleLeft
        }, 1, 0);

        var encabezado = Ui.Encabezado("Recomendaciones inteligentes", "Encuentra grupos compatibles con la asignatura, disponibilidad y agenda del estudiante.");
        var acciones = Ui.BarraAcciones(btnRegresar);
        var contenido = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 6,
            BackColor = Ui.Background,
            RowStyles =
            {
                new RowStyle(SizeType.Absolute, 92),
                new RowStyle(SizeType.Absolute, 64),
                new RowStyle(SizeType.Absolute, 156),
                new RowStyle(SizeType.Absolute, 44),
                new RowStyle(SizeType.Percent, 100F),
                new RowStyle(SizeType.Absolute, 72)
            }
        };
        contenido.Controls.Add(encabezado, 0, 0);
        contenido.Controls.Add(selector, 0, 1);
        contenido.Controls.Add(tarjeta, 0, 2);
        contenido.Controls.Add(resumen, 0, 3);
        contenido.Controls.Add(Ui.ContenedorTabla(grid, lblEstado), 0, 4);
        contenido.Controls.Add(acciones, 0, 5);
        Controls.Add(contenido);
        Ui.ConfigurarTabla(grid);

        cboEstudiante.SelectedIndexChanged += (_, _) => ActualizarSeleccion();
        btnActualizar.Click += (_, _) => ActualizarSeleccion();
        btnRegresar.Click += (_, _) => Close();
        Ui.ConfigurarCampos(this);
    }

    private static Label Info(string texto, bool destacado = false) => new()
    {
        Text = texto,
        AutoSize = false,
        Dock = DockStyle.Fill,
        ForeColor = destacado ? Ui.Navy : Ui.Text,
        Font = new Font("Segoe UI", destacado ? 13F : 10F, destacado ? FontStyle.Bold : FontStyle.Regular),
        TextAlign = ContentAlignment.MiddleLeft,
        AutoEllipsis = true,
        Margin = new Padding(0)
    };
}
