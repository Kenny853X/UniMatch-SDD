#nullable enable
namespace UniMatch.Formularios;

partial class FrmReportes
{
    private Label lblEstudiantes = null!, lblGrupos = null!, lblInscripciones = null!, lblDisponibles = null!, lblOcupacion = null!, lblLlenos = null!, lblEstado = null!;
    private DataGridView grid = null!;
    private Button btnActualizar = null!, btnRegresar = null!;
    private GraficoBarras graficoAsignaturas = null!, graficoModalidades = null!, graficoOcupacion = null!;

    private void InitializeComponent()
    {
        lblEstudiantes = Contador("0", "Estudiantes");
        lblGrupos = Contador("0", "Grupos");
        lblInscripciones = Contador("0", "Inscripciones");
        lblDisponibles = Contador("0", "Cupos disponibles");
        lblOcupacion = Contador("0%", "Ocupación general");
        lblLlenos = Contador("0", "Grupos llenos");
        btnActualizar = Ui.BotonPrimario("Actualizar datos");
        btnRegresar = Ui.BotonCancelar("Regresar");
        grid = new DataGridView { Dock = DockStyle.Fill, AutoGenerateColumns = true };
        lblEstado = Ui.EstadoVacio("No hay grupos registrados para mostrar.");
        graficoAsignaturas = new GraficoBarras();
        graficoModalidades = new GraficoBarras();
        graficoOcupacion = new GraficoBarras();

        var encabezado = Ui.Encabezado("Estadísticas y reportes",
            "Analiza los registros, la demanda y la ocupación actual de UniMatch.", btnActualizar);
        var tarjetas = new TableLayoutPanel
        {
            Dock = DockStyle.Top, Height = 100, Padding = new Padding(18, 8, 18, 8),
            ColumnCount = 6, RowCount = 1
        };
        for (var i = 0; i < tarjetas.ColumnCount; i++)
            tarjetas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6667F));
        tarjetas.Controls.Add(Ui.Tarjeta(lblEstudiantes, Ui.Blue), 0, 0);
        tarjetas.Controls.Add(Ui.Tarjeta(lblGrupos, Ui.Turquoise), 1, 0);
        tarjetas.Controls.Add(Ui.Tarjeta(lblInscripciones, Ui.Navy), 2, 0);
        tarjetas.Controls.Add(Ui.Tarjeta(lblDisponibles, Ui.Green), 3, 0);
        tarjetas.Controls.Add(Ui.Tarjeta(lblOcupacion, Ui.Amber), 4, 0);
        tarjetas.Controls.Add(Ui.Tarjeta(lblLlenos, Ui.Red), 5, 0);

        var graficos = new TableLayoutPanel
        {
            Dock = DockStyle.Top, Height = 230, Padding = new Padding(18, 8, 18, 8),
            ColumnCount = 3, RowCount = 1
        };
        for (var i = 0; i < graficos.ColumnCount; i++)
            graficos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333F));
        graficos.Controls.Add(graficoAsignaturas, 0, 0);
        graficos.Controls.Add(graficoModalidades, 1, 0);
        graficos.Controls.Add(graficoOcupacion, 2, 0);

        var tituloTabla = new Panel { Dock = DockStyle.Top, Height = 36, Padding = new Padding(20, 6, 20, 0) };
        tituloTabla.Controls.Add(Ui.Seccion("Detalle de ocupación por grupo"));
        var tabla = Ui.ContenedorTabla(grid, lblEstado);
        tabla.Padding = new Padding(20, 0, 20, 12);
        var acciones = Ui.BarraAcciones(btnRegresar);
        Controls.Add(tabla);
        Controls.Add(acciones);
        Controls.Add(tituloTabla);
        Controls.Add(graficos);
        Controls.Add(tarjetas);
        Controls.Add(encabezado);
        Ui.ConfigurarTabla(grid);
        grid.CellFormatting += FormatearEstado;
        btnActualizar.Click += (_, _) => CargarDatos();
        btnRegresar.Click += (_, _) => Close();
    }

    private static Label Contador(string valor, string descripcion) => new()
    {
        Text = $"{valor}\n{descripcion}", Dock = DockStyle.Fill, ForeColor = Color.White,
        Font = new Font("Segoe UI", 10.5F, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft
    };

    private void FormatearEstado(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.ColumnIndex >= grid.Columns.Count ||
            grid.Columns[e.ColumnIndex].Name != "Estado" || e.CellStyle is null) return;
        var lleno = string.Equals(e.Value?.ToString(), "Lleno", StringComparison.OrdinalIgnoreCase);
        e.CellStyle.BackColor = lleno ? Color.FromArgb(252, 234, 234) : Color.FromArgb(229, 245, 236);
        e.CellStyle.ForeColor = lleno ? Color.FromArgb(145, 35, 35) : Color.FromArgb(22, 105, 70);
        e.CellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        if (grid.Rows[e.RowIndex].Selected)
        {
            e.CellStyle.BackColor = Ui.SelectedBlue;
            e.CellStyle.ForeColor = Color.White;
        }
    }
}
