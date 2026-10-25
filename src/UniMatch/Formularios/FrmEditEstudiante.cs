using UniMatch.Controlador;
using UniMatch.Entidades;

namespace UniMatch.Formularios;

public partial class FrmEditEstudiante : Form
{
    private readonly bool editar;
    public FrmEditEstudiante(Estudiante? estudiante = null)
    {
        editar = estudiante is not null; InitializeComponent(); Ui.Preparar(this, editar ? "Modificar estudiante" : "Nuevo estudiante"); ClientSize = new Size(720, 560); Ui.ConfigurarCampos(this);
        if (estudiante is not null)
        {
            txtCodigo.Text = estudiante.Codigo.ToString(); txtCodigo.Enabled = false; txtNombres.Text = estudiante.Nombres;
            txtApellidos.Text = estudiante.Apellidos; txtCarrera.Text = estudiante.Carrera; txtAsignatura.Text = estudiante.Asignatura;
            cboDia.SelectedItem = estudiante.DiaDisponible; dtInicio.Value = DateTime.Today.Add(estudiante.HoraInicio.ToTimeSpan()); dtFin.Value = DateTime.Today.Add(estudiante.HoraFin.ToTimeSpan());
        }
    }
    private void GuardarClick(object? sender, EventArgs e)
    {
        if (!int.TryParse(txtCodigo.Text, out var codigo)) { Ui.Avisar("El código debe ser un número entero.", true); return; }
        var estudiante = new Estudiante { Codigo = codigo, Nombres = txtNombres.Text, Apellidos = txtApellidos.Text, Carrera = txtCarrera.Text, Asignatura = txtAsignatura.Text, DiaDisponible = cboDia.Text, HoraInicio = TimeOnly.FromDateTime(dtInicio.Value), HoraFin = TimeOnly.FromDateTime(dtFin.Value) };
        var resultado = editar ? TListaEstudiante.Modificar(estudiante) : TListaEstudiante.Agregar(estudiante);
        if (!resultado.Exito) { Ui.Avisar(resultado.Mensaje, true); return; }
        DialogResult = DialogResult.OK;
    }
}
