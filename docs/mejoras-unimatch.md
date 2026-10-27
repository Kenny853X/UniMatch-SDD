# Evolución de UniMatch — Especificaciones de mejoras

## 1. Información del proyecto

**Proyecto:** UniMatch-SDD  
**Asignatura:** Programación Avanzada  
**Integrantes:** Kennedy Vinicio Delgado Narváez y Giana Esthefany Torres Armijos.  
**Estado:** Propuesta de mejoras pendiente de revisión del equipo.  
**Metodología:** Spec Driven Development (SDD).

## 2. Objetivo de las mejoras

Ampliar las funcionalidades y mejorar la presentación de UniMatch para ofrecer una aplicación de escritorio más completa, organizada y fácil de utilizar, conservando la estructura actual del proyecto y aplicando los conocimientos de Programación Avanzada.

## 3. Alcance y tecnologías

Se conservará el desarrollo en C# mediante Windows Forms, organizado en Entidades, Controlador y Formularios.

Los datos se administrarán mediante listas de C# durante la ejecución del programa. No se utilizarán SQLite ni otras bases de datos.

Se mantendrán las funcionalidades existentes de estudiantes, grupos de estudio e inscripciones, ampliando su comportamiento y mejorando su interfaz.

Las modificaciones se realizarán con apoyo de GitHub Copilot, siguiendo las especificaciones acordadas por el equipo y verificando el funcionamiento de cada cambio.

## 4. Mejoras funcionales

### MF01. Rediseño de la interfaz

La ventana principal tendrá un menú lateral con acceso a Inicio, Estudiantes, Grupos, Inscripciones, Buscar grupos, Recomendaciones, Mi agenda y Reportes.

El panel de inicio mostrará indicadores calculados a partir de los datos registrados, incluyendo estudiantes, grupos, inscripciones y cupos disponibles.

Las ventanas mantendrán un estilo visual coherente, con botones, tablas, títulos, colores y mensajes consistentes.

### MF02. Búsqueda avanzada de grupos

El sistema permitirá filtrar grupos por asignatura, día, modalidad y disponibilidad de cupos.

Se podrán consultar el nombre, horario, cantidad de inscritos y estado de cada grupo.

La búsqueda no modificará los registros y permitirá limpiar los filtros para recuperar el listado completo.

### MF03. Recomendación de grupos

El usuario podrá seleccionar un estudiante y solicitar una lista de grupos compatibles.

Las recomendaciones considerarán la asignatura, el día disponible, el horario y los cupos disponibles.

No se recomendarán como opciones de inscripción los grupos en los que el estudiante ya esté registrado o que presenten conflictos de horario con sus inscripciones existentes.

### MF04. Detección de conflictos y agenda personalizada

El sistema rechazará una inscripción cuando el nuevo grupo tenga una reunión que se superponga con otra del mismo estudiante en el mismo día.

La regla también deberá comprobarse cuando se modifiquen los horarios de estudiantes o grupos.

La agenda permitirá seleccionar un estudiante y visualizar sus reuniones inscritas, organizadas por día y hora.

### MF05. Estadísticas y reportes

El sistema mostrará estadísticas calculadas mediante las listas y consultas LINQ.

Los reportes incluirán cantidad de estudiantes, grupos, inscripciones, grupos completos, cupos disponibles y participantes por asignatura.

Los resultados deberán actualizarse cuando cambien los datos y podrán consultarse desde un formulario específico.

## 5. Organización propuesta de formularios

Se conservarán y mejorarán FrmPrincipal, FrmAdminEstudiantes, FrmAdminGrupos, FrmAdminInscripciones, FrmEditEstudiante, FrmEditGrupo, FrmEditInscripcion y FrmParticipantes.

Se propone agregar FrmBuscarGrupos, FrmRecomendaciones, FrmAgendaEstudiante y FrmReportes.

El panel principal podrá presentar las estadísticas generales sin requerir una ventana adicional.

## 6. Reglas y restricciones

- Mantener las validaciones y funcionalidades existentes.
- No permitir inscripciones duplicadas.
- No aceptar inscripciones en grupos sin cupos.
- Verificar la compatibilidad entre asignatura, día y disponibilidad.
- No permitir reuniones superpuestas para un mismo estudiante.
- Evitar que las modificaciones de horarios produzcan conflictos en inscripciones existentes.
- Utilizar listas de C# como almacenamiento temporal.
- No incorporar bases de datos.
- Mantener separada la lógica de los formularios y los controladores.
- No reemplazar todo el proyecto en una sola modificación.
- Revisar y probar cada mejora antes de integrarla a la rama principal.

## 7. Criterios de aceptación

- [ ] La aplicación conserva el funcionamiento de estudiantes, grupos e inscripciones.
- [ ] La interfaz principal dispone de navegación clara y diseño coherente.
- [ ] Los filtros de búsqueda funcionan correctamente.
- [ ] Las recomendaciones muestran grupos compatibles.
- [ ] Se rechazan inscripciones con conflictos de horario.
- [ ] La agenda presenta las reuniones del estudiante seleccionado.
- [ ] Los indicadores y reportes reflejan los registros actuales.
- [ ] El sistema compila y se ejecuta correctamente.
- [ ] Las pruebas automatizadas incluyen las nuevas reglas.
- [ ] Los cambios se revisan mediante GitHub y Pull Requests.

## 8. Proceso de implementación

Las mejoras se desarrollarán de forma progresiva, comenzando por la validación de horarios, seguida de la búsqueda y recomendaciones, el rediseño de la interfaz, la agenda y los reportes.

Antes de implementar cada funcionalidad se revisará su especificación y se utilizará GitHub Copilot para apoyar la generación del código.

Cada cambio deberá comprobarse mediante pruebas y revisión del equipo, manteniendo evidencia del trabajo realizado.