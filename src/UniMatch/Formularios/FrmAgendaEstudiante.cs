using UniMatch.Controlador;
using UniMatch.Entidades;

namespace UniMatch.Formularios;

public partial class FrmAgendaEstudiante : Form
{
    private static readonly string[] DiasSemana =
        ["Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo"];

    public FrmAgendaEstudiante()
    {
        InitializeComponent();
        Ui.Preparar(this, "Mi agenda");
        ClientSize = new Size(1440, 900);
        MinimumSize = new Size(1120, 720);
        Load += (_, _) => CargarEstudiantes();
    }

    private void CargarEstudiantes(int? codigoSeleccionado = null)
    {
        var estudiantes = TListaEstudiante.Lista
            .OrderBy(estudiante => estudiante.Apellidos, StringComparer.OrdinalIgnoreCase)
            .ThenBy(estudiante => estudiante.Nombres, StringComparer.OrdinalIgnoreCase)
            .ToList();

        cboEstudiante.DataSource = estudiantes;
        cboEstudiante.DisplayMember = nameof(Estudiante.NombreCompleto);
        cboEstudiante.ValueMember = nameof(Estudiante.Codigo);

        if (codigoSeleccionado.HasValue)
        {
            var indice = estudiantes.FindIndex(estudiante => estudiante.Codigo == codigoSeleccionado.Value);
            cboEstudiante.SelectedIndex = indice;
        }
        else if (estudiantes.Count > 0)
        {
            cboEstudiante.SelectedIndex = 0;
        }

        ActualizarAgenda();
    }

    private void ActualizarAgenda()
    {
        if (cboEstudiante.SelectedItem is not Estudiante estudiante)
        {
            LimpiarAgenda();
            return;
        }

        var reuniones = TListaInscripcion.AgendaDeEstudiante(estudiante.Codigo).ToList();
        lblNombre.Text = estudiante.NombreCompleto;
        lblCodigo.Text = $"Código: {estudiante.Codigo}";
        lblCarrera.Text = $"Carrera: {estudiante.Carrera}";
        lblAsignatura.Text = $"Asignatura: {estudiante.Asignatura}";
        lblDisponibilidad.Text =
            $"Disponibilidad: {estudiante.DiaDisponible} · {estudiante.HoraInicio:HH\\:mm} - {estudiante.HoraFin:HH\\:mm}";
        lblGrupos.Text = $"{reuniones.Select(reunion => reunion.CodigoGrupo).Distinct().Count()}\nGrupos inscritos";
        lblReuniones.Text = $"{reuniones.Count}\nReuniones programadas";
        lblHoras.Text = $"{TListaInscripcion.HorasSemanales(estudiante.Codigo):0.##} h\nHoras semanales";
        lblEstado.Text = reuniones.Count == 0
            ? "Este estudiante no tiene inscripciones. Registra una inscripción compatible para visualizar su agenda."
            : string.Empty;
        lblEstado.Visible = reuniones.Count == 0;
        if (lblEstado.Visible) lblEstado.BringToFront();

        CargarSemana(reuniones);
        grid.DataSource = reuniones.Select(reunion => new
        {
            reunion.CodigoGrupo,
            Grupo = reunion.NombreGrupo,
            reunion.Asignatura,
            Dia = reunion.Dia,
            Horario = $"{reunion.HoraInicio:HH\\:mm} - {reunion.HoraFin:HH\\:mm}",
            reunion.Modalidad
        }).ToList();
        Ui.Encabezados(grid,
            ("CodigoGrupo", "Código del grupo"), ("Grupo", "Grupo"),
            ("Asignatura", "Asignatura"), ("Dia", "Día"),
            ("Horario", "Horario"), ("Modalidad", "Modalidad"));
    }


    private void CargarSemana(
        IReadOnlyCollection<TListaInscripcion.ReunionAgenda> reuniones)
    {
        for (var indice = 0; indice < panelesDia.Length; indice++)
        {
            var panel = panelesDia[indice];
            panel.Controls.Clear();

            var dia = DiasSemana[indice];

            var delDia = reuniones
                .Where(reunion =>
                    string.Compare(
                        reunion.Dia.Trim(),
                        dia,
                        System.Globalization.CultureInfo.GetCultureInfo("es-ES"),
                        System.Globalization.CompareOptions.IgnoreCase |
                        System.Globalization.CompareOptions.IgnoreNonSpace
                    ) == 0)
                .ToList();

            var encabezado = new Label
            {
                Text = dia.ToUpperInvariant(),
                Dock = DockStyle.Top,
                Height = 34,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Ui.Navy,
                BackColor = Ui.LightBlue,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Margin = new Padding(0)
            };

            var reunionesPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Padding = new Padding(2),
                BackColor = Ui.Surface
            };

            // Agregar primero el contenido y después el encabezado.
            panel.Controls.Add(reunionesPanel);
            panel.Controls.Add(encabezado);

            if (delDia.Count == 0)
            {
                reunionesPanel.Controls.Add(new Label
                {
                    Text = "Sin reuniones",
                    Width = 140,
                    Height = 58,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Ui.MutedText,
                    Font = new Font(
                        "Segoe UI", 9F, FontStyle.Italic),
                    Margin = new Padding(2)
                });

                continue;
            }

            foreach (var reunion in delDia)
            {
                var bloque = new Panel
                {
                    Width = 160,
                    Height = 112,
                    BackColor = Ui.LightBlue,
                    BorderStyle = BorderStyle.FixedSingle,
                    Padding = new Padding(8),
                    Margin = new Padding(3, 6, 3, 6)
                };

                var contenido = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 1,
                    RowCount = 4,
                    BackColor = Ui.LightBlue,
                    Margin = Padding.Empty
                };

                contenido.ColumnStyles.Add(
                    new ColumnStyle(SizeType.Percent, 100F));

                contenido.RowStyles.Add(
                    new RowStyle(SizeType.Percent, 35F));

                contenido.RowStyles.Add(
                    new RowStyle(SizeType.Percent, 25F));

                contenido.RowStyles.Add(
                    new RowStyle(SizeType.Percent, 20F));

                contenido.RowStyles.Add(
                    new RowStyle(SizeType.Percent, 20F));

                contenido.Controls.Add(
                    Celda(reunion.NombreGrupo, Ui.Navy, true), 0, 0);

                contenido.Controls.Add(
                    Celda(reunion.Asignatura, Ui.Text), 0, 1);

                contenido.Controls.Add(
                    Celda(
                        $"{reunion.HoraInicio:HH\\:mm} - {reunion.HoraFin:HH\\:mm}",
                        Ui.Blue, true), 0, 2);

                contenido.Controls.Add(
                    Celda(reunion.Modalidad, Ui.MutedText), 0, 3);

                bloque.Controls.Add(contenido);
                reunionesPanel.Controls.Add(bloque);
            }
        }
    }


    private void LimpiarAgenda()
    {
        lblNombre.Text = "No hay estudiantes registrados";
        lblCodigo.Text = "Código: -";
        lblCarrera.Text = "Carrera: -";
        lblAsignatura.Text = "Asignatura: -";
        lblDisponibilidad.Text = "Disponibilidad: -";
        lblGrupos.Text = "0\nGrupos inscritos";
        lblReuniones.Text = "0\nReuniones programadas";
        lblHoras.Text = "0 h\nHoras semanales";
        lblEstado.Text = "Registra un estudiante para comenzar a consultar agendas.";
        lblEstado.Visible = true;
        lblEstado.BringToFront();
        CargarSemana([]);
        grid.DataSource = null;
    }

    private void VerParticipantes()
    {
        if (grid.CurrentRow?.Cells["CodigoGrupo"].Value is not int codigoGrupo)
        {
            Ui.Avisar("Seleccione una reunión para consultar sus participantes.", true);
            return;
        }

        using var form = new FrmParticipantes(codigoGrupo);
        form.ShowDialog(this);
    }

    private static Label Celda(string texto, Color color, bool destacado = false) => new()
    {
        Text = texto,
        Dock = DockStyle.Fill,
        AutoSize = false,
        AutoEllipsis = false,
        ForeColor = color,
        Font = new Font("Segoe UI", destacado ? 8.5F : 8F, destacado ? FontStyle.Bold : FontStyle.Regular),
        TextAlign = ContentAlignment.MiddleLeft,
        Margin = new Padding(0)
    };
}
