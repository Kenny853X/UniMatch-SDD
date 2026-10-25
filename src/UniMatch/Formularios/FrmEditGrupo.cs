using UniMatch.Controlador;
using UniMatch.Entidades;

namespace UniMatch.Formularios;

public partial class FrmEditGrupo : Form
{
    private readonly bool editar;
    public FrmEditGrupo(GrupoEstudio? grupo = null)
    {
        editar = grupo is not null; InitializeComponent(); Ui.Preparar(this, editar ? "Modificar grupo" : "Nuevo grupo"); ClientSize = new Size(720, 560); Ui.ConfigurarCampos(this);
        if (grupo is not null)
        {
            txtCodigo.Text = grupo.Codigo.ToString(); txtCodigo.Enabled = false; txtNombre.Text = grupo.Nombre; txtAsignatura.Text = grupo.Asignatura;
            cboDia.SelectedItem = grupo.Dia; cboModalidad.SelectedItem = grupo.Modalidad; nudCupo.Value = grupo.CupoMaximo;
            dtInicio.Value = DateTime.Today.Add(grupo.HoraInicio.ToTimeSpan()); dtFin.Value = DateTime.Today.Add(grupo.HoraFin.ToTimeSpan());
        }
    }
    private void GuardarClick(object? sender, EventArgs e)
    {
        if (!int.TryParse(txtCodigo.Text, out var codigo)) { Ui.Avisar("El código debe ser un número entero.", true); return; }
        var grupo = new GrupoEstudio { Codigo = codigo, Nombre = txtNombre.Text, Asignatura = txtAsignatura.Text, Dia = cboDia.Text, Modalidad = cboModalidad.Text, HoraInicio = TimeOnly.FromDateTime(dtInicio.Value), HoraFin = TimeOnly.FromDateTime(dtFin.Value), CupoMaximo = (int)nudCupo.Value };
        var resultado = editar ? TListaGrupoEstudio.Modificar(grupo) : TListaGrupoEstudio.Agregar(grupo);
        if (!resultado.Exito) { Ui.Avisar(resultado.Mensaje, true); return; } DialogResult = DialogResult.OK;
    }
}
