namespace UniMatch.Formularios;

public partial class FrmPrincipal : Form
{
    public FrmPrincipal()
    {
        InitializeComponent(); Ui.Preparar(this, "UniMatch");
        ClientSize = new Size(960, 650);
        ActualizarResumen();
    }

    private void ActualizarResumen()
    {
        lblEstudiantes.Text = $"{Controlador.TListaEstudiante.Lista.Count}\nEstudiantes registrados";
        lblGrupos.Text = $"{Controlador.TListaGrupoEstudio.Lista.Count}\nGrupos de estudio";
        lblInscripciones.Text = $"{Controlador.TListaInscripcion.Lista.Count}\nInscripciones activas";
    }
}
