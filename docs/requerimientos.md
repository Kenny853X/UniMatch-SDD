# Fase 1 Requerimientos de UniMatch

## 1. Información del proyecto

**Proyecto:** UniMatch  
**Asignatura:** Programación Avanzada  
**Integrantes:**
- Kennedy Vinicio Delgado Narváez.
- Giana Esthefany Torres Armijos.

**Estado:** Propuesta inicial para revisión de ambos integrantes.  
**Fase:** Requerimientos de la metodología SDD.

## 2. Propósito

UniMatch permitirá organizar grupos de estudio entre estudiantes de la Universidad Técnica de Machala. Facilitará el registro de estudiantes, la búsqueda de grupos por asignatura y la inscripción de participantes con disponibilidad compatible.

## 3. Alcance

Se desarrollará una aplicación de escritorio completa en C# con Windows Forms, organizada en Entidades, Controlador y Formularios.

La aplicación incluirá la administración de estudiantes, grupos de estudio e inscripciones. El código conservará una estructura clara y comprensible, mientras que las ventanas tendrán una presentación cuidada y una navegación coherente.

## 4. Requerimientos funcionales

### RF01 Registro de estudiantes

El sistema permitirá registrar estudiantes con los siguientes datos:

- Código único.
- Nombres.
- Apellidos.
- Carrera.
- Asignatura.
- Día disponible.
- Hora de inicio y hora de finalización de su disponibilidad.

### RF02 Administración de estudiantes

El sistema permitirá consultar, modificar y eliminar estudiantes registrados.

Las modificaciones y eliminaciones deberán mantener correctamente la información relacionada con sus inscripciones.

### RF03 Creación de grupos de estudio

El sistema permitirá crear grupos con los siguientes datos:

- Código único.
- Nombre del grupo.
- Asignatura.
- Día de reunión.
- Hora de inicio y hora de finalización.
- Modalidad.
- Cupo máximo.

### RF04 Administración de grupos

El sistema permitirá consultar, modificar y eliminar grupos de estudio.

Los cambios deberán respetar las inscripciones existentes y mantener la información relacionada correctamente.

### RF05 Búsqueda de grupos

El sistema permitirá buscar grupos por asignatura y consultar su día, horario, modalidad y cupos disponibles.

### RF06 Inscripción de estudiantes

El sistema permitirá inscribir estudiantes en grupos existentes.

Antes de aceptar una inscripción deberá comprobar:

- La existencia del estudiante y del grupo.
- La disponibilidad de cupos.
- La ausencia de una inscripción duplicada.
- La compatibilidad de asignatura, día y horario.

Cuando una condición no se cumpla, se mostrará un mensaje que explique el motivo.

### RF07 Consulta de participantes

El sistema permitirá seleccionar un grupo y visualizar sus estudiantes inscritos, la cantidad de participantes y los cupos disponibles.

## 5. Reglas generales

- Los códigos de estudiantes y grupos serán únicos.
- Los campos obligatorios deberán estar completos.
- La hora de inicio será anterior a la hora de finalización.
- El cupo máximo de un grupo será mayor que cero.
- Un estudiante no podrá inscribirse dos veces en el mismo grupo.
- Las inscripciones no podrán superar el cupo máximo.
- La inscripción requerirá compatibilidad de asignatura, día y horario.
- Las modificaciones y eliminaciones conservarán la integridad de los datos relacionados.

Las condiciones detalladas de compatibilidad, modificación y eliminación se establecerán en la especificación funcional.

## 6. Requerimientos no funcionales

### RNF01 Presentación y facilidad de uso

Las ventanas tendrán controles alineados, textos legibles, botones consistentes, tablas organizadas y navegación clara entre las funciones.

### RNF02 Entorno de desarrollo

La aplicación se desarrollará en C# con Windows Forms utilizando Visual Studio. La versión de .NET se definirá y documentará en el plan técnico.

### RNF03 Validación de datos

Se verificarán los datos obligatorios, los valores numéricos y las reglas del sistema. Los mensajes permitirán identificar y corregir los errores.

### RNF04 Organización del código

El código se separará en Entidades, Controlador y Formularios.

Se utilizarán listas de C#, controladores Tlista y métodos comprensibles. La información almacenada únicamente en memoria se conservará durante la sesión de ejecución.

### RNF05 Trabajo colaborativo

Ambos integrantes participarán mediante sus propias cuentas de GitHub. Se utilizarán ramas, Issues y Pull Requests para organizar las tareas y revisar los cambios.

### RNF06 Verificación automatizada

GitHub Actions comprobará la compilación de la solución y ejecutará las pruebas automatizadas configuradas.

### RNF07 Desarrollo mediante especificaciones e IA

Se aplicará SDD con apoyo de GitHub Spec Kit. GitHub Copilot generará el código a partir de las especificaciones, el plan técnico y las tareas revisadas por el equipo.

Los integrantes revisarán el resultado y solicitarán las correcciones necesarias a la IA.

### RNF08 Documentación y evidencias

Se documentarán los requerimientos, las especificaciones, el plan, las tareas y las decisiones de implementación.

La entrega incluirá instrucciones para abrir, compilar, ejecutar y probar la solución, junto con evidencias reales de participación de ambos integrantes.

## 7. Criterios de aceptación

- [ ] La solución abre y compila en Visual Studio.
- [ ] Se pueden registrar, consultar, modificar y eliminar estudiantes.
- [ ] Se pueden crear, consultar, modificar y eliminar grupos.
- [ ] La búsqueda por asignatura funciona correctamente.
- [ ] Se aceptan inscripciones que cumplen las condiciones.
- [ ] Se rechazan inscripciones duplicadas, incompatibles o sin cupo.
- [ ] Se pueden consultar los participantes de cada grupo.
- [ ] Las modificaciones y eliminaciones mantienen las relaciones correctamente.
- [ ] Las ventanas presentan la información de forma clara y organizada.
- [ ] Las validaciones muestran mensajes comprensibles.
- [ ] Las pruebas y GitHub Actions se ejecutan correctamente.
- [ ] La documentación y las evidencias permiten comprobar el trabajo de ambos integrantes.
