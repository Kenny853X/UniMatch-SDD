namespace UniMatch.Formularios;

public partial class FrmPrincipal : Form
{
    public FrmPrincipal()
    {
        InitializeComponent(); Ui.Preparar(this, "UniMatch");
        ClientSize = new Size(1280, 760);
        MinimumSize = new Size(1050, 650);
        ActualizarResumen();
    }

    private void ActualizarResumen()
    {
        var grupos = Controlador.TListaGrupoEstudio.Lista;
        var disponibles = grupos.Sum(g => Controlador.TListaGrupoEstudio.CuposDisponibles(g));
        var llenos = grupos.Count(g => Controlador.TListaGrupoEstudio.CuposDisponibles(g) == 0);
        lblEstudiantes.Text = $"{Controlador.TListaEstudiante.Lista.Count}\nEstudiantes";
        lblGrupos.Text = $"{grupos.Count}\nGrupos de estudio";
        lblInscripciones.Text = $"{Controlador.TListaInscripcion.Lista.Count}\nInscripciones";
        lblDisponibles.Text = $"{disponibles}\nCupos disponibles";
        lblLlenos.Text = $"{llenos}\nGrupos llenos";
        lblGruposResumen.Text = grupos.Count == 0
            ? "No hay grupos registrados.\n\nCrea el primer grupo desde el módulo Grupos."
            : string.Join(Environment.NewLine + Environment.NewLine, grupos.Take(6).Select(g =>
                $"{g.Nombre}  ·  {g.Asignatura}\n{g.Dia}  |  {g.HoraInicio:HH\\:mm} - {g.HoraFin:HH\\:mm}  |  {g.Modalidad}"));
        lblActividad.Text = Controlador.TListaInscripcion.Lista.Count == 0
            ? "Aún no hay actividad registrada.\n\nLas nuevas inscripciones aparecerán aquí."
            : $"Últimas inscripciones: {Controlador.TListaInscripcion.Lista.Count}\n\nLos datos se mantienen en memoria durante esta sesión.";
    }
}
