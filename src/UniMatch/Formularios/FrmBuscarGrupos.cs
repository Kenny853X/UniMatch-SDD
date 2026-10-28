using UniMatch.Controlador;

namespace UniMatch.Formularios;

public partial class FrmBuscarGrupos : Form
{
    public FrmBuscarGrupos()
    {
        InitializeComponent();
        Ui.Preparar(this, "Buscar grupos");

        ClientSize = new Size(1400, 760);
        MinimumSize = new Size(1050, 650);

        Load += (_, _) =>
        {
            CargarAsignaturas();
            Cargar();
        };
    }

    private void CargarAsignaturas()
    {
        var asignaturas = new List<string> { "Todas" };

        asignaturas.AddRange(
            TListaGrupoEstudio.Lista
                .Select(x => x.Asignatura.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(x => x, StringComparer.OrdinalIgnoreCase)
                .ToList());

        cboAsignatura.DataSource = asignaturas;
        cboAsignatura.SelectedIndex = 0;
    }

    private void Cargar()
    {
        TimeOnly? horaInicio = chkHorario.Checked
            ? TimeOnly.FromDateTime(dtpDesde.Value)
            : null;

        TimeOnly? horaFin = chkHorario.Checked
            ? TimeOnly.FromDateTime(dtpHasta.Value)
            : null;

        if (horaInicio.HasValue && horaFin.HasValue &&
            horaInicio > horaFin)
        {
            grid.DataSource = null;
            lblConteo.Text = "0 grupos";
            lblEstado.Text = "El intervalo horario seleccionado no es válido.";
            lblEstado.Visible = true;
            lblEstado.BringToFront();
            return;
        }

        var asignatura = ValorSeleccionado(cboAsignatura);

        var grupos = TListaGrupoEstudio.BuscarAvanzado(
            asignatura,
            ValorSeleccionado(cboDia),
            ValorSeleccionado(cboModalidad),
            horaInicio,
            horaFin,
            (FiltroDisponibilidad)cboDisponibilidad.SelectedIndex)
            .Where(x => asignatura is null ||
                string.Equals(
                    x.Asignatura.Trim(),
                    asignatura.Trim(),
                    StringComparison.OrdinalIgnoreCase))
            .ToList();

        grid.DataSource = grupos.Select(x =>
        {
            var disponibles = TListaGrupoEstudio.CuposDisponibles(x);

            return new
            {
                x.Codigo,
                x.Nombre,
                x.Asignatura,
                x.Dia,
                Horario = $"{x.HoraInicio:HH\\:mm} - {x.HoraFin:HH\\:mm}",
                x.Modalidad,
                x.CupoMaximo,
                Participantes = TListaGrupoEstudio.Participantes(x.Codigo),
                Disponibles = disponibles,
                Estado = disponibles > 0 ? "Disponible" : "Lleno"
            };
        }).ToList();

        lblConteo.Text =
            $"{grupos.Count} grupo{(grupos.Count == 1 ? "" : "s")}";

        lblEstado.Text =
            "No existen grupos que coincidan con los filtros seleccionados.";

        lblEstado.Visible = grupos.Count == 0;

        if (lblEstado.Visible)
            lblEstado.BringToFront();

        Ui.Encabezados(grid,
            ("Codigo", "Código"),
            ("Nombre", "Grupo"),
            ("Asignatura", "Asignatura"),
            ("Dia", "Día"),
            ("Horario", "Horario"),
            ("Modalidad", "Modalidad"),
            ("CupoMaximo", "Cupo máximo"),
            ("Participantes", "Inscritos"),
            ("Disponibles", "Cupos disponibles"),
            ("Estado", "Estado"));
    }

    private static string? ValorSeleccionado(ComboBox combo) =>
        combo.SelectedIndex <= 0
            ? null
            : combo.SelectedItem?.ToString();

    private int? Seleccionado() =>
        grid.CurrentRow?.Cells["Codigo"].Value is int codigo
            ? codigo
            : null;

    private void VerParticipantes()
    {
        var codigo = Seleccionado();

        if (codigo is null)
        {
            Ui.Avisar("Seleccione un grupo.", true);
            return;
        }

        using var form = new FrmParticipantes(codigo.Value);
        form.ShowDialog(this);
    }

    private void Limpiar()
    {
        cboAsignatura.SelectedIndex = 0;
        cboDia.SelectedIndex = 0;
        cboModalidad.SelectedIndex = 0;
        cboDisponibilidad.SelectedIndex = 0;
        chkHorario.Checked = false;

        Cargar();
    }
}