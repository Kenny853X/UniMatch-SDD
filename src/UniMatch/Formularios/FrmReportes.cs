
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
        if (lblEstado.Visible)
            lblEstado.BringToFront();

        Ui.Encabezados(grid,
            ("Codigo", "Código"),
            ("Nombre", "Nombre"),
            ("Asignatura", "Asignatura"),
            ("Modalidad", "Modalidad"),
            ("CupoMaximo", "Cupo máximo"),
            ("Inscritos", "Inscritos"),
            ("CuposDisponibles", "Cupos disponibles"),
            ("PorcentajeOcupacion", "% ocupación"),
            ("Estado", "Estado"));

        ConfigurarColumnas();
    }

    private void ConfigurarColumnas()
    {
        grid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        grid.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
        grid.ColumnHeadersHeight = 48;
        grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

        grid.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.Fill;

        var pesos = new Dictionary<string, float>(StringComparer.Ordinal)
        {
            ["Codigo"] = 8,
            ["Nombre"] = 25,
            ["Asignatura"] = 22,
            ["Modalidad"] = 14,
            ["CupoMaximo"] = 10,
            ["Inscritos"] = 10,
            ["CuposDisponibles"] = 12,
            ["PorcentajeOcupacion"] = 14,
            ["Estado"] = 13
        };

        foreach (DataGridViewColumn columna in grid.Columns)
        {
            if (pesos.TryGetValue(columna.Name, out var peso))
                columna.FillWeight = peso;

            columna.MinimumWidth = 72;

            columna.DefaultCellStyle.Alignment =
                columna.Name is "Codigo" or "CupoMaximo"
                    or "Inscritos" or "CuposDisponibles"
                    or "PorcentajeOcupacion" or "Estado"
                    ? DataGridViewContentAlignment.MiddleCenter
                    : DataGridViewContentAlignment.MiddleLeft;
        }
    }

    private sealed class GraficoBarras : Panel
    {
        private IReadOnlyList<(string Etiqueta, int Valor)> datos = [];
        private string titulo = string.Empty;
        private Color color = Ui.Blue;

        public GraficoBarras()
        {
            DoubleBuffered = true;
            Dock = DockStyle.Fill;
            BackColor = Ui.Surface;
            BorderStyle = BorderStyle.FixedSingle;
            Resize += (_, _) => Invalidate();
        }

        public void SetDatos(
            IReadOnlyList<(string Etiqueta, int Valor)> nuevosDatos,
            string nuevoTitulo,
            Color nuevoColor)
        {
            datos = nuevosDatos.Where(item => item.Valor >= 0).ToList();
            titulo = nuevoTitulo;
            color = nuevoColor;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using var tituloFuente =
                new Font("Segoe UI", 11F, FontStyle.Bold);
            using var textoFuente =
                new Font("Segoe UI", 8.5F);
            using var leyendaFuente =
                new Font("Segoe UI", 7.5F);

            using var tituloPincel = new SolidBrush(Ui.Navy);
            using var textoPincel = new SolidBrush(Ui.MutedText);
            using var valorPincel = new SolidBrush(Ui.Text);
            using var barraPincel = new SolidBrush(color);
            using var fondoPincel =
                new SolidBrush(Color.FromArgb(239, 244, 249));

            e.Graphics.DrawString(
                titulo, tituloFuente, tituloPincel,
                new RectangleF(16, 12, Math.Max(1, Width - 32), 30));

            if (datos.Count == 0)
            {
                const string vacio = "No hay datos para mostrar.";

                var tamano = e.Graphics.MeasureString(vacio, textoFuente);

                e.Graphics.DrawString(
                    vacio,
                    textoFuente,
                    textoPincel,
                    Math.Max(6, (Width - tamano.Width) / 2),
                    Math.Max(45, (Height - tamano.Height) / 2));

                return;
            }

            var margenIzquierdo = Math.Clamp(
                datos.Max(item =>
                    TextRenderer.MeasureText(
                        Abreviar(item.Etiqueta),
                        textoFuente).Width) + 22,
                86,
                Math.Max(86, Width / 2));

            const int margenDerecho = 30;
            const int margenSuperior = 56;

            var hayNombresLargos = datos.Any(
                item => Abreviar(item.Etiqueta) != item.Etiqueta);

            var margenInferior = hayNombresLargos ? 65 : 24;

            var areaAncho = Math.Max(
                1, Width - margenIzquierdo - margenDerecho);

            var areaAlto = Math.Max(
                1, Height - margenSuperior - margenInferior);

            var maximo = Math.Max(
                1, datos.Max(item => item.Valor));

            var altoFila = Math.Max(
                18, areaAlto / datos.Count);

            var altoBarra = Math.Clamp(
                altoFila - 6, 10, 22);

            for (var indice = 0; indice < datos.Count; indice++)
            {
                var item = datos[indice];

                var y = margenSuperior
                    + indice * altoFila
                    + Math.Max(0, (altoFila - altoBarra) / 2);

                var ancho = (int)(
                    areaAncho * (item.Valor / (double)maximo));

                var etiqueta = Abreviar(item.Etiqueta);

                var rectEtiqueta = new RectangleF(
                    8, y - 3,
                    Math.Max(1, margenIzquierdo - 18),
                    altoBarra + 10);

                using var formato = new StringFormat
                {
                    Alignment = StringAlignment.Far,
                    LineAlignment = StringAlignment.Center,
                    Trimming = StringTrimming.EllipsisCharacter,
                    FormatFlags = StringFormatFlags.NoWrap
                };

                e.Graphics.DrawString(
                    etiqueta,
                    textoFuente,
                    textoPincel,
                    rectEtiqueta,
                    formato);

                e.Graphics.FillRectangle(
                    fondoPincel,
                    margenIzquierdo,
                    y,
                    areaAncho,
                    altoBarra);

                if (item.Valor > 0)
                {
                    e.Graphics.FillRectangle(
                        barraPincel,
                        margenIzquierdo,
                        y,
                        Math.Max(2, ancho),
                        altoBarra);
                }

                var valor = item.Valor.ToString();
                var tamanoValor =
                    e.Graphics.MeasureString(valor, textoFuente);

                var valorX = Math.Min(
                    Width - margenDerecho - tamanoValor.Width,
                    Math.Max(
                        margenIzquierdo + 6,
                        margenIzquierdo + ancho + 6));

                e.Graphics.DrawString(
                    valor,
                    textoFuente,
                    valorPincel,
                    valorX,
                    y + (altoBarra - tamanoValor.Height) / 2);
            }

            if (hayNombresLargos)
            {
                var nombresCompletos = datos
                    .Where(item =>
                        Abreviar(item.Etiqueta) != item.Etiqueta)
                    .Select(item => item.Etiqueta);

                var leyenda = string.Join(" · ", nombresCompletos);

                var rectLeyenda = new RectangleF(
                    14,
                    Height - margenInferior + 10,
                    Math.Max(1, Width - 28),
                    margenInferior - 12);

                using var formatoLeyenda = new StringFormat
                {
                    Trimming = StringTrimming.EllipsisCharacter
                };

                e.Graphics.DrawString(
                    leyenda,
                    leyendaFuente,
                    textoPincel,
                    rectLeyenda,
                    formatoLeyenda);
            }
        }

        private static string Abreviar(string etiqueta)
        {
            const int maximoCaracteres = 18;

            var limpia = etiqueta.Trim();

            return limpia.Length <= maximoCaracteres
                ? limpia
                : limpia[..(maximoCaracteres - 1)] + "...";
        }
    }
}
