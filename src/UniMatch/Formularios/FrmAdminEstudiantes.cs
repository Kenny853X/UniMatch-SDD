using UniMatch.Controlador;

namespace UniMatch.Formularios;

public partial class FrmAdminEstudiantes : Form
{
    public FrmAdminEstudiantes() { InitializeComponent(); Ui.Preparar(this, "Estudiantes"); ClientSize = new Size(1240, 720); MinimumSize = new Size(980, 600); Load += (_, _) => Cargar(); }
    private void Cargar(string texto = "")
    {
        var filtro = texto.Trim();
        var estudiantes = TListaEstudiante.Lista.Where(x => string.IsNullOrWhiteSpace(filtro)
            || $"{x.Nombres} {x.Apellidos} {x.Asignatura}".Contains(filtro, StringComparison.OrdinalIgnoreCase)).ToList();
        grid.DataSource = estudiantes.Select(x => new { x.Codigo, x.Nombres, x.Apellidos, x.Carrera, x.Asignatura, x.DiaDisponible, x.HoraInicio, x.HoraFin }).ToList();
        lblConteo.Text = $"{estudiantes.Count} estudiante{(estudiantes.Count == 1 ? "" : "s")}";
        lblEstado.Text = TListaEstudiante.Lista.Count == 0 ? "No hay estudiantes registrados." : "No hay coincidencias para esta búsqueda.";
        lblEstado.Visible = estudiantes.Count == 0;
        if (lblEstado.Visible) lblEstado.BringToFront();
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
