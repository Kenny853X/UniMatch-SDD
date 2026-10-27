#nullable enable
namespace UniMatch.Formularios;

partial class FrmAgendaEstudiante
{
    private ComboBox cboEstudiante = null!;
    private Label lblNombre = null!, lblCodigo = null!, lblCarrera = null!, lblAsignatura = null!, lblDisponibilidad = null!;
    private Label lblGrupos = null!, lblReuniones = null!, lblHoras = null!, lblEstado = null!;
    private DataGridView grid = null!;
    private Button btnActualizar = null!, btnParticipantes = null!, btnRegresar = null!;
    private readonly Panel[] panelesDia = new Panel[7];

    private void InitializeComponent()
    {
        cboEstudiante = new ComboBox
        {
            Dock = DockStyle.Fill,
            DropDownStyle = ComboBoxStyle.DropDownList,
            Margin = new Padding(6, 4, 6, 4)
        };
        btnActualizar = Ui.BotonPrimario("Actualizar agenda");
        btnParticipantes = Ui.BotonSecundario("Ver participantes");
        btnRegresar = Ui.BotonCancelar("Regresar");
        lblNombre = Informacion("Seleccione un estudiante", true);
        lblCodigo = Informacion("Código: -");
        lblCarrera = Informacion("Carrera: -");
        lblAsignatura = Informacion("Asignatura: -");
        lblDisponibilidad = Informacion("Disponibilidad: -");
        lblGrupos = Indicador("0", "Grupos inscritos");
        lblReuniones = Indicador("0", "Reuniones programadas");
        lblHoras = Indicador("0 h", "Horas semanales");
        lblEstado = Ui.EstadoVacio("Registra un estudiante para comenzar a consultar agendas.");
        grid = new DataGridView { Dock = DockStyle.Fill, AutoGenerateColumns = true, Margin = new Padding(0) };

        var selector = new TableLayoutPanel
        {
            Dock = DockStyle.Fill, ColumnCount = 3, RowCount = 1,
            Padding = new Padding(18, 8, 18, 8), BackColor = Ui.Surface,
            ColumnStyles =
            {
                new ColumnStyle(SizeType.AutoSize),
                new ColumnStyle(SizeType.Percent, 100F),
                new ColumnStyle(SizeType.AutoSize)
            }
        };
        selector.Controls.Add(new Label
        {
            Text = "Estudiante", AutoSize = true, Anchor = AnchorStyles.Left,
            ForeColor = Ui.MutedText, Margin = new Padding(0, 9, 12, 0)
        }, 0, 0);
        selector.Controls.Add(cboEstudiante, 1, 0);
        selector.Controls.Add(btnActualizar, 2, 0);

        var informacion = new TableLayoutPanel
        {
            Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 3,
            Padding = new Padding(18, 10, 18, 10), BackColor = Ui.Surface
        };
        informacion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        informacion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        for (var i = 0; i < 3; i++) informacion.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
        informacion.Controls.Add(lblNombre, 0, 0);
        informacion.SetColumnSpan(lblNombre, 2);
        informacion.Controls.Add(lblCodigo, 0, 1);
        informacion.Controls.Add(lblCarrera, 1, 1);
        informacion.Controls.Add(lblAsignatura, 0, 2);
        informacion.Controls.Add(lblDisponibilidad, 1, 2);

        var indicadores = new TableLayoutPanel
        {
            Dock = DockStyle.Fill, ColumnCount = 3, RowCount = 1,
            Padding = new Padding(18, 5, 18, 5), BackColor = Ui.Background
        };
        indicadores.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        indicadores.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        indicadores.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
        indicadores.Controls.Add(Ui.Tarjeta(lblGrupos, Ui.Blue), 0, 0);
        indicadores.Controls.Add(Ui.Tarjeta(lblReuniones, Ui.Turquoise), 1, 0);
        indicadores.Controls.Add(Ui.Tarjeta(lblHoras, Ui.Green), 2, 0);

        var semana = new TableLayoutPanel
        {
            Dock = DockStyle.Fill, ColumnCount = 7, RowCount = 1,
            Padding = new Padding(18, 4, 18, 4), BackColor = Ui.Background,
            CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
        };
        for (var indice = 0; indice < panelesDia.Length; indice++)
        {
            semana.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857F));
            panelesDia[indice] = new Panel
            {
                Dock = DockStyle.Fill, BackColor = Ui.Surface, Padding = new Padding(4),
                Margin = new Padding(3), AutoScroll = true
            };
            semana.Controls.Add(panelesDia[indice], indice, 0);
        }

        var detalle = new TableLayoutPanel
        {
            Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2,
            Padding = new Padding(18, 4, 18, 8), BackColor = Ui.Background
        };
        detalle.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
        detalle.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        detalle.Controls.Add(new Label
        {
            Text = "Detalle de horarios", AutoSize = true, Anchor = AnchorStyles.Left,
            ForeColor = Ui.Navy, Font = new Font("Segoe UI", 11F, FontStyle.Bold),
            Margin = new Padding(2, 4, 0, 0)
        }, 0, 0);
        detalle.Controls.Add(Ui.ContenedorTabla(grid, lblEstado), 0, 1);

        var acciones = Ui.BarraAcciones(btnParticipantes, btnRegresar);
        var contenido = new TableLayoutPanel
        {
            Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 7, BackColor = Ui.Background,
            RowStyles =
            {
                new RowStyle(SizeType.Absolute, 92),
                new RowStyle(SizeType.Absolute, 64),
                new RowStyle(SizeType.Absolute, 106),
                new RowStyle(SizeType.Absolute, 94),
                new RowStyle(SizeType.Absolute, 246),
                new RowStyle(SizeType.Percent, 100F),
                new RowStyle(SizeType.Absolute, 72)
            }
        };
        contenido.Controls.Add(Ui.Encabezado("Mi agenda", "Consulta las reuniones semanales de estudio de cada estudiante inscrito."), 0, 0);
        contenido.Controls.Add(selector, 0, 1);
        contenido.Controls.Add(Ui.Tarjeta(informacion), 0, 2);
        contenido.Controls.Add(indicadores, 0, 3);
        contenido.Controls.Add(semana, 0, 4);
        contenido.Controls.Add(detalle, 0, 5);
        contenido.Controls.Add(acciones, 0, 6);
        Controls.Add(contenido);

        Ui.ConfigurarTabla(grid);
        cboEstudiante.SelectedIndexChanged += (_, _) => ActualizarAgenda();
        btnActualizar.Click += (_, _) =>
        {
            var codigo = (cboEstudiante.SelectedItem as Estudiante)?.Codigo;
            CargarEstudiantes(codigo);
        };
        btnParticipantes.Click += (_, _) => VerParticipantes();
        btnRegresar.Click += (_, _) => Close();
        Ui.ConfigurarCampos(this);
    }

    private static Label Informacion(string texto, bool destacado = false) => new()
    {
        Text = texto, Dock = DockStyle.Fill, AutoSize = false, AutoEllipsis = false,
        ForeColor = destacado ? Ui.Navy : Ui.Text,
        Font = new Font("Segoe UI", destacado ? 13F : 9.5F, destacado ? FontStyle.Bold : FontStyle.Regular),
        TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(2, 0, 8, 0),
        Margin = new Padding(0)
    };

    private static Label Indicador(string valor, string descripcion) => new()
    {
        Text = $"{valor}\n{descripcion}", Dock = DockStyle.Fill, ForeColor = Color.White,
        Font = new Font("Segoe UI", 11F, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft,
        Padding = new Padding(4, 0, 0, 0)
    };
}
