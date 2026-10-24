namespace UniMatch.Formularios;

public partial class FrmPrincipal : Form
{
    public FrmPrincipal()
    {
        InitializeComponent(); Ui.Preparar(this, "UniMatch");
        ClientSize = new Size(760, 480);
    }
}
