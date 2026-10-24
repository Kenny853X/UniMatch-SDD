#nullable enable
namespace UniMatch.Formularios;

partial class FrmPrincipal
{
    private System.ComponentModel.IContainer? components;
    private Button btnEstudiantes = null!, btnGrupos = null!, btnInscripciones = null!, btnSalir = null!;
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        btnEstudiantes = Ui.Boton("Administrar estudiantes");
        btnGrupos = Ui.Boton("Administrar grupos");
        btnInscripciones = Ui.Boton("Administrar inscripciones");
        btnSalir = Ui.Boton("Salir");
        var panel = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(30), FlowDirection = FlowDirection.TopDown, AutoScroll = true };
        panel.Controls.AddRange([btnEstudiantes, btnGrupos, btnInscripciones, btnSalir]);
        Controls.Add(panel); Controls.Add(Ui.Titulo("UniMatch | Grupos de estudio"));
        btnEstudiantes.Click += (_, _) => new FrmAdminEstudiantes().ShowDialog(this);
        btnGrupos.Click += (_, _) => new FrmAdminGrupos().ShowDialog(this);
        btnInscripciones.Click += (_, _) => new FrmAdminInscripciones().ShowDialog(this);
        btnSalir.Click += (_, _) => Close();
    }
}
