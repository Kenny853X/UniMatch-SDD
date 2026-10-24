using UniMatch.Controlador;

namespace UniMatch.Formularios;

public partial class FrmAdminGrupos : Form
{
    public FrmAdminGrupos() { InitializeComponent(); Ui.Preparar(this, "Grupos"); Load += (_, _) => Cargar(); }
    private void Cargar(string texto = "") => grid.DataSource = TListaGrupoEstudio.BuscarPorAsignatura(texto).Select(x => new { x.Codigo, x.Nombre, x.Asignatura, x.Dia, Horario = x.HoraInicio.ToString("HH:mm") + " - " + x.HoraFin.ToString("HH:mm"), x.Modalidad, x.CupoMaximo, Participantes = TListaGrupoEstudio.Participantes(x.Codigo), Disponibles = TListaGrupoEstudio.CuposDisponibles(x) }).ToList();
    private int? Seleccionado() => grid.CurrentRow?.Cells["Codigo"].Value is int codigo ? codigo : null;
    private void Editar() { var c = Seleccionado(); if (c is null) { Ui.Avisar("Seleccione un grupo.", true); return; } if (new FrmEditGrupo(TListaGrupoEstudio.Buscar(c.Value)).ShowDialog(this) == DialogResult.OK) Cargar(txtBuscar.Text); }
    private void Eliminar() { var c = Seleccionado(); if (c is null) { Ui.Avisar("Seleccione un grupo.", true); return; } if (MessageBox.Show("También se eliminarán sus inscripciones relacionadas. ¿Continuar?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return; var r = TListaGrupoEstudio.Eliminar(c.Value); Ui.Avisar(r.Mensaje, !r.Exito); Cargar(txtBuscar.Text); }
    private void VerParticipantes() { var c = Seleccionado(); if (c is null) { Ui.Avisar("Seleccione un grupo.", true); return; } new FrmParticipantes(c.Value).ShowDialog(this); }
}
