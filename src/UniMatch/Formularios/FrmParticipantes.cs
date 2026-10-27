using UniMatch.Controlador;
using UniMatch.Entidades;

namespace UniMatch.Formularios;

public partial class FrmParticipantes : Form
{
    public FrmParticipantes(int? codigoGrupo = null)
    {
        InitializeComponent(); Ui.Preparar(this, "Participantes");
        ClientSize = new Size(1080, 680); MinimumSize = new Size(900, 580);
        cboGrupo.DataSource = TListaGrupoEstudio.Lista.ToList(); cboGrupo.DisplayMember = "Nombre";
        if (codigoGrupo is not null) cboGrupo.SelectedItem = TListaGrupoEstudio.Buscar(codigoGrupo.Value);
        Cargar();
    }
    private void Cargar()
    {
        if (cboGrupo.SelectedItem is not GrupoEstudio grupo) { grid.DataSource = null; lblEstado.Text = "Seleccione un grupo para consultar sus participantes."; lblEstado.Visible = true; lblEstado.BringToFront(); lblResumen.Text = "No hay grupos registrados."; return; }
        grid.DataSource = TListaInscripcion.PorGrupo(grupo.Codigo).Select(x => TListaEstudiante.Buscar(x.CodigoEstudiante)).Where(x => x is not null).Select(x => new { x!.Codigo, x.NombreCompleto, x.Carrera, x.Asignatura }).ToList();
        lblEstado.Text = "Este grupo todavía no tiene participantes.";
        lblEstado.Visible = TListaGrupoEstudio.Participantes(grupo.Codigo) == 0;
        if (lblEstado.Visible) lblEstado.BringToFront();
        Ui.Encabezados(grid, ("Codigo", "Código"), ("NombreCompleto", "Estudiante"), ("Carrera", "Carrera"), ("Asignatura", "Asignatura"));
        lblResumen.Text = $"  {grupo.Nombre}   ·   Participantes: {TListaGrupoEstudio.Participantes(grupo.Codigo)}   ·   Cupos disponibles: {TListaGrupoEstudio.CuposDisponibles(grupo)}";
    }
}
