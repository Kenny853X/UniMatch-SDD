namespace UniMatch.Formularios;

partial class FrmAdminInscripciones
{
    private DataGridView grid = null!; private Button btnInscribir = null!, btnCancelar = null!, btnParticipantes = null!, btnRegresar = null!;
    private void InitializeComponent()
    {
        grid = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AutoGenerateColumns = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, AllowUserToAddRows = false };
        btnInscribir = Ui.Boton("Nueva inscripción"); btnCancelar = Ui.Boton("Cancelar inscripción", peligro: true); btnParticipantes = Ui.Boton("Consultar participantes", secundario: true); btnRegresar = Ui.Boton("Regresar", secundario: true);
        var buttons = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 55, Padding = new Padding(10) }; buttons.Controls.AddRange([btnInscribir, btnCancelar, btnParticipantes, btnRegresar]);
        Controls.Add(grid); Controls.Add(buttons); Controls.Add(Ui.Titulo("Administración de inscripciones"));
        Ui.ConfigurarTabla(grid);
        btnInscribir.Click += (_, _) => { if (new FrmEditInscripcion().ShowDialog(this) == DialogResult.OK) Cargar(); }; btnCancelar.Click += CancelarClick; btnParticipantes.Click += (_, _) => new FrmParticipantes().ShowDialog(this); btnRegresar.Click += (_, _) => Close();
    }
}
