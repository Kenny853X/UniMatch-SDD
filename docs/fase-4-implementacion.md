# Fase 4 - Implementación de UniMatch

## Alcance entregado

La solución se implementó con .NET 8 Windows Forms y almacenamiento en memoria:

- Entidades `Estudiante`, `GrupoEstudio` e `Inscripcion`.
- Controladores estáticos `TListaEstudiante`, `TListaGrupoEstudio` y `TListaInscripcion`.
- Administración, edición y eliminación de estudiantes y grupos.
- Búsqueda de grupos por asignatura, consulta de participantes y cupos.
- Registro y cancelación de inscripciones con validación de existencia, duplicados, asignatura, día, horario y cupos.
- Eliminación en cascada de inscripciones y rechazo atómico de modificaciones incompatibles.
- Formularios con archivos `.Designer.cs` editables en Visual Studio.
- Pruebas automatizadas para compatibilidad, duplicados, cupos y relaciones.
- GitHub Actions en Windows para restaurar, compilar y ejecutar pruebas.
- Datos de ejemplo cargados una sola vez al iniciar la aplicación.
- Panel principal con contadores reales y tablas con encabezados, filas y acciones diferenciadas.
- Detección centralizada de conflictos de horarios mediante `TCompatibilidadHorario`.

La información se pierde al cerrar la aplicación, conforme al alcance de la Fase 2.

## Datos de ejemplo

La carga está centralizada en `src/UniMatch/Controlador/DatosEjemplo.cs`, en el método `DatosEjemplo.Cargar()`. `Program.Main` lo invoca antes de abrir `FrmPrincipal`; el método usa una bandera interna para no duplicar datos aunque se abran nuevamente los formularios.

Incluye 10 estudiantes, 6 grupos y 9 inscripciones válidas. El conjunto contiene:

- El grupo `101` lleno.
- El grupo `105` sin participantes.
- Grupos presenciales y virtuales.
- Estudiantes con horarios incompatibles con algunos grupos para probar rechazos.

Para iniciar la aplicación sin datos de ejemplo, cambie `DatosEjemplo.Habilitados` a `false` antes de la llamada a `DatosEjemplo.Cargar()` en `Program.cs`. Las listas continuarán funcionando normalmente para registrar datos manualmente.

## MF04: conflictos de horarios

`TCompatibilidadHorario.SeSuperponen` aplica la regla `inicioA < finB && finA > inicioB` únicamente cuando los días coinciden. Por eso un grupo que termina a las 11:00 y otro que comienza a las 11:00 son consecutivos y válidos. `TListaInscripcion.Registrar` rechaza el conflicto antes de modificar la lista y muestra el grupo y horario implicados.

Al modificar un grupo, `TListaGrupoEstudio.Modificar` valida primero la compatibilidad de sus participantes y luego comprueba sus conflictos con las demás inscripciones del estudiante, excluyendo el propio grupo. Si falla, el objeto almacenado no se modifica. La carga de ejemplo usa horarios consecutivos para que las nueve inscripciones previstas se registren correctamente; si alguna fallara, `DatosEjemplo.Cargar()` lanza una excepción explícita en lugar de ocultarla.

## Spec Kit y Copilot

Se comprobó que el repositorio no tenía `.specify`. Se agregó la integración basada en los documentos existentes:

- `.specify/memory/constitution.md`
- `.specify/README.md`
- `.github/copilot-instructions.md`

No se instaló la CLI de Spec Kit porque la estructura documental requerida se pudo registrar directamente y no fue necesaria una generación adicional. Por tanto, no se ejecutó un comando de instalación Python; si se instala posteriormente, debe usarse Python 3.12.

## Comandos ejecutados y resultados

| Comando | Resultado |
|---|---|
| `git fetch origin refs/heads/main:refs/remotes/origin/main refs/heads/docs/fase-3-plan-tecnico:refs/remotes/origin/docs/fase-3-plan-tecnico` | Correcto; se recuperó el plan técnico. |
| `git merge --no-edit origin/main` | Correcto; integración fast-forward de `docs/plan-tecnico.md`, conservando historial. |
| `dotnet test UniMatch.sln` | Restauró y compiló los proyectos, pero el testhost no pudo ejecutarse en Linux porque el entorno solo tiene runtime .NET 10 y el proyecto requiere runtime .NET 8. |
| `dotnet build UniMatch.sln --configuration Release` | Debe ejecutarse en Windows o con el runtime Windows Desktop correspondiente. |
| `DOTNET_ROLL_FORWARD=Major dotnet test UniMatch.sln --no-restore` | No pudo ejecutar el testhost porque tampoco está instalado `Microsoft.WindowsDesktop.App` 8.0 en este entorno Linux. |

## Verificaciones pendientes

- Ejecutar `dotnet test UniMatch.sln` en Windows con .NET 8 instalado.
- Abrir la solución en Visual Studio y revisar visualmente los formularios y sus archivos Designer.
- Ejecutar los casos CV15 y CV29 de navegación/presentación en Windows.
- Confirmar en GitHub Actions la ejecución real del workflow sobre `windows-latest`.
- Revisar visualmente los formularios en Windows con escalas de 100 %, 125 % y 150 %.
