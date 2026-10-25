using UniMatch.Controlador;

namespace UniMatch.Formularios;

public partial class FrmAdminEstudiantes : Form
{
    public FrmAdminEstudiantes() { InitializeComponent(); Ui.Preparar(this, "Estudiantes"); ClientSize = new Size(1180, 650); Load += (_, _) => Cargar(); }
    private void Cargar()
    {
        grid.DataSource = TListaEstudiante.Lista.Select(x => new { x.Codigo, x.Nombres, x.Apellidos, x.Carrera, x.Asignatura, x.DiaDisponible, x.HoraInicio, x.HoraFin }).ToList();
        Ui.Encabezados(grid, ("Codigo", "Código"), ("Nombres", "Nombres"), ("Apellidos", "Apellidos"), ("Carrera", "Carrera"), ("Asignatura", "Asignatura"), ("DiaDisponible", "Día disponible"), ("HoraInicio", "Inicio"), ("HoraFin", "Fin"));
    }
    private int? Seleccionado() => grid.CurrentRow?.Cells["Codigo"].Value is int codigo ? codigo : null;
    private void Editar()
    {
        var codigo = Seleccionado(); if (codigo is null) { Ui.Avisar("Seleccione un estudiante.", true); return; }
        if (new FrmEditEstudiante(TListaEstudiante.Buscar(codigo.Value)).ShowDialog(this) == DialogResult.OK) Cargar();
    }
    private void Eliminar()
    {
        var codigo = Seleccionado(); if (codigo is null) { Ui.Avisar("Seleccione un estudiante.", true); return; }
        if (MessageBox.Show("También se eliminarán sus inscripciones relacionadas. ¿Continuar?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
        var r = TListaEstudiante.Eliminar(codigo.Value); Ui.Avisar(r.Mensaje, !r.Exito); Cargar();
    }
}
