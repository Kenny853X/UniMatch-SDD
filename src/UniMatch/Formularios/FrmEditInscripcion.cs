using UniMatch.Controlador;

namespace UniMatch.Formularios;

public partial class FrmEditInscripcion : Form
{
    public FrmEditInscripcion() { InitializeComponent(); Ui.Preparar(this, "Inscripción"); ClientSize = new Size(820, 560); MinimumSize = new Size(760, 520); cboEstudiante.DataSource = TListaEstudiante.Lista.ToList(); cboEstudiante.DisplayMember = "NombreCompleto"; cboGrupo.DataSource = TListaGrupoEstudio.Lista.ToList(); cboGrupo.DisplayMember = "Nombre"; ActualizarDetalle(); }
    private void ActualizarDetalle()
    {
        if (cboGrupo.SelectedItem is not Entidades.GrupoEstudio g) { lblDetalle.Text = "Selecciona un grupo para consultar sus condiciones."; return; }
        var disponibles = TListaGrupoEstudio.CuposDisponibles(g);
        lblDetalle.Text = $"Asignatura: {g.Asignatura}\nDía y horario: {g.Dia}  |  {g.HoraInicio:HH\\:mm} - {g.HoraFin:HH\\:mm}\nModalidad: {g.Modalidad}  |  Cupos disponibles: {disponibles}";
    }
    private void Guardar(object? sender, EventArgs e) { if (cboEstudiante.SelectedItem is not Entidades.Estudiante e1 || cboGrupo.SelectedItem is not Entidades.GrupoEstudio g) { Ui.Avisar("Seleccione un estudiante y un grupo.", true); return; } var r = TListaInscripcion.Registrar(e1.Codigo, g.Codigo); if (!r.Exito) { Ui.Avisar(r.Mensaje, true); return; } DialogResult = DialogResult.OK; }
}
