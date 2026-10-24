using UniMatch.Controlador;

namespace UniMatch.Formularios;

public partial class FrmEditInscripcion : Form
{
    public FrmEditInscripcion() { InitializeComponent(); Ui.Preparar(this, "Inscripción"); cboEstudiante.DataSource = TListaEstudiante.Lista.ToList(); cboEstudiante.DisplayMember = "NombreCompleto"; cboGrupo.DataSource = TListaGrupoEstudio.Lista.ToList(); cboGrupo.DisplayMember = "Nombre"; ActualizarDetalle(); }
    private void ActualizarDetalle() { if (cboGrupo.SelectedItem is not Entidades.GrupoEstudio g) { lblDetalle.Text = "Seleccione un grupo."; return; } lblDetalle.Text = $"{g.Asignatura} | {g.Dia} | {g.HoraInicio:HH\\:mm}-{g.HoraFin:HH\\:mm} | {g.Modalidad} | Cupos: {TListaGrupoEstudio.CuposDisponibles(g)}"; }
    private void Guardar(object? sender, EventArgs e) { if (cboEstudiante.SelectedItem is not Entidades.Estudiante e1 || cboGrupo.SelectedItem is not Entidades.GrupoEstudio g) { Ui.Avisar("Seleccione un estudiante y un grupo.", true); return; } var r = TListaInscripcion.Registrar(e1.Codigo, g.Codigo); if (!r.Exito) { Ui.Avisar(r.Mensaje, true); return; } DialogResult = DialogResult.OK; }
}
