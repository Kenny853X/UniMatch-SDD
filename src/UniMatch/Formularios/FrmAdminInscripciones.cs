using UniMatch.Controlador;

namespace UniMatch.Formularios;

public partial class FrmAdminInscripciones : Form
{
    public FrmAdminInscripciones() { InitializeComponent(); Ui.Preparar(this, "Inscripciones"); Load += (_, _) => Cargar(); }
    private void Cargar() => grid.DataSource = TListaInscripcion.Lista.Select(i => { var e = TListaEstudiante.Buscar(i.CodigoEstudiante)!; var g = TListaGrupoEstudio.Buscar(i.CodigoGrupo)!; return new { CodigoEstudiante = e.Codigo, Estudiante = e.NombreCompleto, CodigoGrupo = g.Codigo, Grupo = g.Nombre, g.Asignatura, Dia = g.Dia, Horario = g.HoraInicio.ToString("HH:mm") + " - " + g.HoraFin.ToString("HH:mm") }; }).ToList();
    private void CancelarClick(object? sender, EventArgs e)
    {
        if (grid.CurrentRow?.Cells["CodigoEstudiante"].Value is not int estudiante || grid.CurrentRow.Cells["CodigoGrupo"].Value is not int grupo) { Ui.Avisar("Seleccione una inscripción.", true); return; }
        if (MessageBox.Show("¿Cancelar la inscripción seleccionada?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        var r = TListaInscripcion.Cancelar(estudiante, grupo); Ui.Avisar(r.Mensaje, !r.Exito); Cargar();
    }
}
