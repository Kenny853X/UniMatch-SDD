namespace UniMatch.Formularios;

partial class FrmBuscarGrupos
{
    private DataGridView grid = null!;
    private Label lblEstado = null!, lblConteo = null!;
    private TextBox txtAsignatura = null!;
    private ComboBox cboDia = null!, cboModalidad = null!, cboDisponibilidad = null!;
    private CheckBox chkHorario = null!;
    private DateTimePicker dtpDesde = null!, dtpHasta = null!;
    private Button btnBuscar = null!, btnLimpiar = null!, btnParticipantes = null!, btnRegresar = null!;

    private void InitializeComponent()
    {
        grid = new DataGridView { Dock = DockStyle.Fill, AutoGenerateColumns = true };
        lblEstado = Ui.EstadoVacio("No existen grupos que coincidan con los filtros seleccionados.");
        lblConteo = Ui.EtiquetaEstado("0 grupos", Ui.Turquoise);
        txtAsignatura = new TextBox { Dock = DockStyle.Fill, PlaceholderText = "Asignatura..." };
        cboDia = Combo("Día", "Todos", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo");
        cboModalidad = Combo("Modalidad", "Todas", "Presencial", "Virtual");
        cboDisponibilidad = Combo("Cupos", "Todos", "Disponibles", "Llenos");
        chkHorario = new CheckBox { Text = "Filtrar horario", AutoSize = true, ForeColor = Ui.Navy, Checked = false };
        dtpDesde = Hora();
        dtpHasta = Hora();
        btnBuscar = Ui.BotonPrimario("Buscar");
        btnLimpiar = Ui.BotonSecundario("Limpiar filtros");
        btnParticipantes = Ui.BotonSecundario("Ver participantes");
        btnRegresar = Ui.BotonCancelar("Regresar");

        var filtroAsignatura = Filtro("Asignatura", txtAsignatura);
        var filtroDia = Filtro("Día", cboDia);
        var filtroModalidad = Filtro("Modalidad", cboModalidad);
        var filtroCupos = Filtro("Disponibilidad", cboDisponibilidad);
        var filtroDesde = Filtro("Desde", dtpDesde);
        var filtroHasta = Filtro("Hasta", dtpHasta);
        var filtros = new FlowLayoutPanel
        {
            Dock = DockStyle.Top, Height = 132, WrapContents = true,
            FlowDirection = FlowDirection.LeftToRight, Padding = new Padding(0, 4, 0, 8),
            Margin = new Padding(0)
        };
        filtros.Controls.AddRange([filtroAsignatura, filtroDia, filtroModalidad, filtroCupos, chkHorario, filtroDesde, filtroHasta]);
        var encabezado = Ui.Encabezado("Búsqueda avanzada de grupos", "Combina asignatura, día, modalidad, horario y disponibilidad de cupos.");
        var accionesBusqueda = new FlowLayoutPanel
        {
            Dock = DockStyle.Top, Height = 62, WrapContents = false,
            FlowDirection = FlowDirection.LeftToRight, Padding = new Padding(18, 8, 18, 8),
            BackColor = Ui.Surface
        };
        accionesBusqueda.Controls.AddRange([btnBuscar, btnLimpiar, lblConteo]);

        var acciones = Ui.BarraAcciones(btnParticipantes, btnRegresar);
        Controls.Add(Ui.ContenedorTabla(grid, lblEstado));
        Controls.Add(acciones);
        Controls.Add(accionesBusqueda);
        Controls.Add(filtros);
        Controls.Add(encabezado);
        Ui.ConfigurarTabla(grid);

        btnBuscar.Click += (_, _) => Cargar();
        btnLimpiar.Click += (_, _) => Limpiar();
        btnParticipantes.Click += (_, _) => VerParticipantes();
        btnRegresar.Click += (_, _) => Close();
        chkHorario.CheckedChanged += (_, _) =>
        {
            dtpDesde.Enabled = chkHorario.Checked;
            dtpHasta.Enabled = chkHorario.Checked;
        };
        grid.CellFormatting += (_, e) =>
        {
            if (e.RowIndex < 0 || grid.Columns[e.ColumnIndex].Name != "Estado") return;
            var disponible = e.Value?.ToString() == "Disponible";
            e.CellStyle.BackColor = disponible ? Color.FromArgb(229, 245, 236) : Color.FromArgb(252, 234, 234);
            e.CellStyle.ForeColor = disponible ? Color.FromArgb(22, 105, 70) : Color.FromArgb(145, 35, 35);
            if (grid.Rows[e.RowIndex].Selected)
            {
                e.CellStyle.BackColor = Ui.SelectedBlue;
                e.CellStyle.ForeColor = Color.White;
            }
            e.CellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        };
        dtpDesde.Enabled = false;
        dtpHasta.Enabled = false;
        Ui.ConfigurarCampos(this);
    }

    private static ComboBox Combo(string nombre, params string[] opciones) => new()
    {
        Name = nombre, Width = 145, DropDownStyle = ComboBoxStyle.DropDownList,
        DataSource = opciones.ToList()
    };

    private static DateTimePicker Hora() => new()
    {
        Width = 115, Format = DateTimePickerFormat.Custom, CustomFormat = "HH:mm",
        ShowUpDown = true, Value = DateTime.Today.AddHours(8)
    };

    private static Panel Filtro(string etiqueta, Control control)
    {
        var panel = new Panel { Width = 150, Height = 60, Margin = new Padding(0, 0, 10, 4) };
        panel.Controls.Add(new Label { Text = etiqueta, AutoSize = true, ForeColor = Ui.MutedText, Location = new Point(0, 2) });
        control.Location = new Point(0, 24);
        panel.Controls.Add(control);
        return panel;
    }
}
