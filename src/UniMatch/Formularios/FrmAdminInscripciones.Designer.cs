namespace UniMatch.Formularios;

partial class FrmAdminInscripciones
{
    private DataGridView grid = null!;
    private Label lblEstado = null!, lblConteo = null!;
    private Button btnInscribir = null!, btnCancelar = null!, btnParticipantes = null!, btnRegresar = null!;

    private void InitializeComponent()
    {
        grid = new DataGridView { Dock = DockStyle.Fill, AutoGenerateColumns = true };
        lblEstado = Ui.EstadoVacio("No hay inscripciones activas.");
        lblConteo = Ui.EtiquetaEstado("0 inscripciones", Ui.Navy);
        btnInscribir = Ui.BotonPrimario("Nueva inscripción"); btnCancelar = Ui.BotonEliminar("Cancelar inscripción");
        btnParticipantes = Ui.BotonSecundario("Consultar participantes"); btnRegresar = Ui.BotonCancelar("Regresar");
        var resumen = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 30, WrapContents = false };
        resumen.Controls.Add(lblConteo);
        var encabezado = Ui.Encabezado("Inscripciones", "Consulta las relaciones activas entre estudiantes y grupos.", resumen);
        var acciones = Ui.BarraAcciones(btnInscribir, btnCancelar, btnParticipantes, btnRegresar);
        Controls.Add(Ui.ContenedorTabla(grid, lblEstado)); Controls.Add(acciones); Controls.Add(encabezado);
        Ui.ConfigurarTabla(grid);
        btnInscribir.Click += (_, _) => { if (new FrmEditInscripcion().ShowDialog(this) == DialogResult.OK) Cargar(); };
        btnCancelar.Click += CancelarClick; btnParticipantes.Click += (_, _) => new FrmParticipantes().ShowDialog(this); btnRegresar.Click += (_, _) => Close();
    }
}
