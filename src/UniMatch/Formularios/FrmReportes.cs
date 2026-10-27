using System.Drawing.Drawing2D;
using UniMatch.Controlador;

namespace UniMatch.Formularios;

public partial class FrmReportes : Form
{
    public FrmReportes()
    {
        InitializeComponent();
        Ui.Preparar(this, "Estadísticas y reportes");
        ClientSize = new Size(1440, 900);
        MinimumSize = new Size(1120, 760);
        Load += (_, _) => CargarDatos();
    }

    private void CargarDatos()
    {
        var resumen = TEstadisticas.Resumen();
        lblEstudiantes.Text = $"{resumen.TotalEstudiantes}\nEstudiantes";
        lblGrupos.Text = $"{resumen.TotalGrupos}\nGrupos";
        lblInscripciones.Text = $"{resumen.TotalInscripciones}\nInscripciones";
        lblDisponibles.Text = $"{resumen.CuposDisponibles}\nCupos disponibles";
        lblOcupacion.Text = $"{resumen.PorcentajeOcupacion:0.0}%\nOcupación general";
        lblLlenos.Text = $"{resumen.GruposLlenos}\nGrupos llenos";

        graficoAsignaturas.SetDatos(
            TEstadisticas.InscripcionesPorAsignatura()
                .Select(item => (item.Asignatura, item.Inscripciones))
                .ToList(),
            "Inscripciones por asignatura", Ui.Blue);
        graficoModalidades.SetDatos(
            TEstadisticas.GruposPorModalidad()
                .Select(item => (item.Modalidad, item.Grupos))
                .ToList(),
            "Grupos por modalidad", Ui.Turquoise);
        graficoOcupacion.SetDatos(
            TEstadisticas.GruposMasSolicitados()
                .Select(item => (item.Nombre, item.Inscritos))
                .ToList(),
            "Ocupación de cupos por grupo", Ui.Green);

        var ocupaciones = TEstadisticas.OcupacionPorGrupo();
        grid.DataSource = ocupaciones.Select(item => new
        {
            item.Codigo,
            item.Nombre,
            item.Asignatura,
            item.Modalidad,
            CupoMaximo = item.CupoMaximo,
            Inscritos = item.Inscritos,
            CuposDisponibles = item.CuposDisponibles,
            PorcentajeOcupacion = $"{item.PorcentajeOcupacion:0.0}%",
            item.Estado
        }).ToList();
        lblEstado.Visible = ocupaciones.Count == 0;
        if (lblEstado.Visible) lblEstado.BringToFront();
        Ui.Encabezados(grid,
            ("Codigo", "Código"), ("Nombre", "Nombre"), ("Asignatura", "Asignatura"),
            ("Modalidad", "Modalidad"), ("CupoMaximo", "Cupo máximo"),
            ("Inscritos", "Inscritos"), ("CuposDisponibles", "Cupos disponibles"),
            ("PorcentajeOcupacion", "% ocupación"), ("Estado", "Estado"));
    }

    private sealed class GraficoBarras : Panel
    {
        private IReadOnlyList<(string Etiqueta, int Valor)> datos = [];
        private string titulo = string.Empty;
        private Color color = Ui.Blue;

        public GraficoBarras()
        {
            DoubleBuffered = true;
            BackColor = Ui.Surface;
            BorderStyle = BorderStyle.FixedSingle;
            Resize += (_, _) => Invalidate();
        }

        public void SetDatos(IReadOnlyList<(string Etiqueta, int Valor)> nuevosDatos, string nuevoTitulo, Color nuevoColor)
        {
            datos = nuevosDatos;
            titulo = nuevoTitulo;
            color = nuevoColor;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using var tituloFuente = new Font("Segoe UI", 10F, FontStyle.Bold);
            using var textoFuente = new Font("Segoe UI", 8.5F);
            using var tituloPincel = new SolidBrush(Ui.Navy);
            using var textoPincel = new SolidBrush(Ui.MutedText);
            e.Graphics.DrawString(titulo, tituloFuente, tituloPincel, new PointF(16, 14));

            if (datos.Count == 0)
            {
                var vacio = "No hay datos para mostrar.";
                var tamano = e.Graphics.MeasureString(vacio, textoFuente);
                e.Graphics.DrawString(vacio, textoFuente, textoPincel,
                    (Width - tamano.Width) / 2, (Height - tamano.Height) / 2);
                return;
            }

            var margenIzquierdo = 28;
            var margenDerecho = 16;
            var margenSuperior = 48;
            var margenInferior = 50;
            var areaAncho = Math.Max(1, Width - margenIzquierdo - margenDerecho);
            var areaAlto = Math.Max(1, Height - margenSuperior - margenInferior);
            var maximo = Math.Max(1, datos.Max(item => item.Valor));
            var anchoBarra = Math.Max(10, areaAncho / Math.Max(1, datos.Count) - 12);

            using var linea = new Pen(Ui.Border);
            e.Graphics.DrawLine(linea, margenIzquierdo, margenSuperior + areaAlto,
                Width - margenDerecho, margenSuperior + areaAlto);
            for (var indice = 0; indice < datos.Count; indice++)
            {
                var item = datos[indice];
                var alto = (int)(areaAlto * (item.Valor / (double)maximo));
                var x = margenIzquierdo + indice * (areaAncho / datos.Count) +
                    Math.Max(0, (areaAncho / datos.Count - anchoBarra) / 2);
                var y = margenSuperior + areaAlto - alto;
                using var pincel = new SolidBrush(color);
                e.Graphics.FillRectangle(pincel, x, y, anchoBarra, Math.Max(2, alto));
                var valor = item.Valor.ToString();
                var valorTamano = e.Graphics.MeasureString(valor, textoFuente);
                e.Graphics.DrawString(valor, textoFuente, tituloPincel, x + (anchoBarra - valorTamano.Width) / 2, y - 18);
                var etiqueta = item.Etiqueta.Length > 16 ? item.Etiqueta[..16] + "..." : item.Etiqueta;
                var etiquetaTamano = e.Graphics.MeasureString(etiqueta, textoFuente);
                e.Graphics.DrawString(etiqueta, textoFuente, textoPincel,
                    x + (anchoBarra - etiquetaTamano.Width) / 2, margenSuperior + areaAlto + 8);
            }
        }
    }
}
