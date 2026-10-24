using UniMatch.Controlador;
using UniMatch.Entidades;

namespace UniMatch.Formularios;

public partial class FrmParticipantes : Form
{
    public FrmParticipantes(int? codigoGrupo = null)
    {
        InitializeComponent(); Ui.Preparar(this, "Participantes");
        cboGrupo.DataSource = TListaGrupoEstudio.Lista.ToList(); cboGrupo.DisplayMember = "Nombre";
        if (codigoGrupo is not null) cboGrupo.SelectedItem = TListaGrupoEstudio.Buscar(codigoGrupo.Value);
        Cargar();
    }
    private void Cargar()
    {
        if (cboGrupo.SelectedItem is not GrupoEstudio grupo) { grid.DataSource = null; lblResumen.Text = "No hay grupos registrados."; return; }
        grid.DataSource = TListaInscripcion.PorGrupo(grupo.Codigo).Select(x => TListaEstudiante.Buscar(x.CodigoEstudiante)).Where(x => x is not null).Select(x => new { x!.Codigo, x.NombreCompleto, x.Carrera, x.Asignatura }).ToList();
        lblResumen.Text = $"Participantes: {TListaGrupoEstudio.Participantes(grupo.Codigo)} | Cupos disponibles: {TListaGrupoEstudio.CuposDisponibles(grupo)}";
    }
}
