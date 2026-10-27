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
            Dock = DockStyle.Top, Height = 128, WrapContents = true,
            FlowDirection = FlowDirection.LeftToRight, Padding = new Padding(0, 0, 0, 8),
            Margin = new Padding(0)
        };
        filtros.Controls.AddRange([filtroAsignatura, filtroDia, filtroModalidad, filtroCupos, chkHorario, filtroDesde, filtroHasta, btnBuscar, btnLimpiar, lblConteo]);

        var encabezado = new Panel { Dock = DockStyle.Top, Height = 210, BackColor = Ui.Background, Padding = new Padding(24, 20, 24, 0) };
        encabezado.Controls.Add(filtros);
        encabezado.Controls.Add(Ui.Subtitulo("Combina asignatura, día, modalidad, horario y disponibilidad de cupos."));
        encabezado.Controls.Add(Ui.Titulo("Búsqueda avanzada de grupos"));
        encabezado.Controls[1].Location = new Point(0, 48);
        encabezado.Controls[2].Location = new Point(0, 4);

        var acciones = Ui.BarraAcciones(btnParticipantes, btnRegresar);
        Controls.Add(Ui.ContenedorTabla(grid, lblEstado));
        Controls.Add(acciones);
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
            e.CellStyle.ForeColor = e.Value?.ToString() == "Disponible" ? Ui.Green : Ui.Red;
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
        var panel = new Panel { Width = 150, Height = 52, Margin = new Padding(0, 0, 10, 4) };
        panel.Controls.Add(new Label { Text = etiqueta, AutoSize = true, ForeColor = Ui.MutedText, Location = new Point(0, 0) });
        control.Location = new Point(0, 22);
        panel.Controls.Add(control);
        return panel;
    }
}
