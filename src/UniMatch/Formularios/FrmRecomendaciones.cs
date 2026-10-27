using UniMatch.Controlador;
using UniMatch.Entidades;

namespace UniMatch.Formularios;

public partial class FrmRecomendaciones : Form
{
    public FrmRecomendaciones()
    {
        InitializeComponent();
        Ui.Preparar(this, "Recomendaciones");
        ClientSize = new Size(1400, 780);
        MinimumSize = new Size(1050, 650);
        Load += (_, _) => CargarEstudiantes();
    }

    private void CargarEstudiantes()
    {
        cboEstudiante.DataSource = TListaEstudiante.Lista
            .OrderBy(estudiante => estudiante.Apellidos, StringComparer.OrdinalIgnoreCase)
            .ThenBy(estudiante => estudiante.Nombres, StringComparer.OrdinalIgnoreCase)
            .ToList();
        cboEstudiante.DisplayMember = nameof(Estudiante.NombreCompleto);
        cboEstudiante.ValueMember = nameof(Estudiante.Codigo);
        lblEstado.Visible = TListaEstudiante.Lista.Count == 0;
        if (lblEstado.Visible) lblEstado.BringToFront();
        if (cboEstudiante.Items.Count > 0) cboEstudiante.SelectedIndex = 0;
        ActualizarSeleccion();
    }

    private void ActualizarSeleccion()
    {
        if (cboEstudiante.SelectedItem is not Estudiante estudiante)
        {
            LimpiarInformacion();
            return;
        }

        lblNombre.Text = estudiante.NombreCompleto;
        lblCarrera.Text = $"Carrera: {estudiante.Carrera}";
        lblAsignatura.Text = $"Asignatura: {estudiante.Asignatura}";
        lblDisponibilidad.Text =
            $"Disponibilidad: {estudiante.DiaDisponible} · {estudiante.HoraInicio:HH\\:mm} - {estudiante.HoraFin:HH\\:mm}";
        CargarRecomendaciones(estudiante);
    }

    private void CargarRecomendaciones(Estudiante estudiante)
    {
        var grupos = TRecomendacionGrupos.ParaEstudiante(estudiante).ToList();
        grid.DataSource = grupos.Select(grupo => new
        {
            grupo.Codigo,
            Grupo = grupo.Nombre,
            grupo.Asignatura,
            Dia = grupo.Dia,
            Horario = $"{grupo.HoraInicio:HH\\:mm} - {grupo.HoraFin:HH\\:mm}",
            grupo.Modalidad,
            CuposDisponibles = TListaGrupoEstudio.CuposDisponibles(grupo)
        }).ToList();
        lblConteo.Text = $"{grupos.Count} recomendación{(grupos.Count == 1 ? "" : "es")}";
        lblEstado.Text = grupos.Count == 0
            ? "No existen grupos compatibles para el estudiante seleccionado."
            : string.Empty;
        lblEstado.Visible = grupos.Count == 0;
        if (lblEstado.Visible) lblEstado.BringToFront();
        Ui.Encabezados(grid,
            ("Codigo", "Código"), ("Grupo", "Grupo"), ("Asignatura", "Asignatura"),
            ("Dia", "Día"), ("Horario", "Horario"), ("Modalidad", "Modalidad"),
            ("CuposDisponibles", "Cupos disponibles"));
    }

    private void LimpiarInformacion()
    {
        lblNombre.Text = "Seleccione un estudiante";
        lblCarrera.Text = "Carrera: -";
        lblAsignatura.Text = "Asignatura: -";
        lblDisponibilidad.Text = "Disponibilidad: -";
        grid.DataSource = null;
        lblConteo.Text = "0 recomendaciones";
    }
}
