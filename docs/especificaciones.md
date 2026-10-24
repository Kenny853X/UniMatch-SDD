# Fase 2 - Especificaciones funcionales de UniMatch

## 1. Información general

**Proyecto:** UniMatch  
**Asignatura:** Programación Avanzada  
**Integrantes:**
- Kennedy Vinicio Delgado Narváez.
- Giana Esthefany Torres Armijos.

**Fase:** Especificaciones funcionales de la metodología SDD.

## 2. Objetivo de la especificación

Definir de manera detallada cómo funcionarán los módulos de estudiantes, grupos de estudio e inscripciones de UniMatch, incluyendo sus formularios, operaciones y validaciones. Estas especificaciones servirán como base para el plan técnico y la generación posterior del código en C# con apoyo de GitHub Copilot.

## 3. Organización de la interfaz

UniMatch será una aplicación de escritorio desarrollada en Windows Forms. Contará con una ventana principal para acceder a la administración de estudiantes, grupos de estudio e inscripciones.

Cada módulo presentará la información mediante tablas y botones para realizar las operaciones correspondientes. Se utilizarán formularios de registro y edición con campos identificados, mensajes de validación y una navegación sencilla.
## 4. Especificación del módulo de estudiantes

### 4.1. Propósito

Este módulo permitirá registrar, consultar, modificar y eliminar la información de los estudiantes que utilizarán UniMatch. También permitirá conocer las asignaturas y los horarios en los que cada estudiante está disponible para participar en grupos de estudio.

### 4.2. Ventanas del módulo

El módulo contará con dos formularios de Windows Forms:

**FrmAdminEstudiantes:** será la ventana principal del módulo. Mostrará los estudiantes registrados mediante un DataGridView y contará con los botones Insertar, Modificar, Eliminar y Regresar.

**FrmEditEstudiante:** será el formulario utilizado para ingresar o modificar los datos de un estudiante. Se utilizará la misma ventana para ambas operaciones.

### 4.3. Campos del formulario

El formulario FrmEditEstudiante incluirá los siguientes campos:

| Campo | Control de Windows Forms | Descripción |
|---|---|---|
| Código | TextBox | Identificador único del estudiante. |
| Nombres | TextBox | Nombres del estudiante. |
| Apellidos | TextBox | Apellidos del estudiante. |
| Carrera | TextBox | Carrera que estudia. |
| Asignatura | TextBox | Asignatura en la que desea estudiar. |
| Día disponible | ComboBox | Día en el que puede participar. |
| Hora de inicio | DateTimePicker | Inicio de su disponibilidad. |
| Hora de finalización | DateTimePicker | Finalización de su disponibilidad. |

El formulario también contará con los botones Guardar y Cancelar.

### 4.4. Funcionamiento de los botones

**Insertar:** abrirá FrmEditEstudiante con los campos vacíos. Después de ingresar y validar los datos, permitirá guardar un nuevo estudiante.

**Modificar:** permitirá seleccionar un estudiante del DataGridView y abrir FrmEditEstudiante con sus datos actuales. El código no podrá modificarse porque identifica al estudiante.

**Eliminar:** permitirá eliminar el estudiante seleccionado, previa confirmación del usuario. También se eliminarán las inscripciones relacionadas con ese estudiante.

**Guardar:** verificará que los datos ingresados sean correctos antes de registrar o actualizar la información.

**Cancelar:** cerrará el formulario de edición sin guardar los cambios.

**Regresar:** permitirá volver a la ventana principal de UniMatch.

### 4.5. Validaciones

El módulo deberá cumplir las siguientes condiciones:

1. El código del estudiante será un número entero mayor que cero.
2. No se permitirán códigos repetidos al registrar estudiantes.
3. Los campos obligatorios deberán estar completos.
4. Se deberá seleccionar un día disponible.
5. La hora de inicio deberá ser anterior a la hora de finalización.
6. El código no podrá modificarse durante la edición.
7. Antes de eliminar un estudiante se solicitará confirmación.
8. Las operaciones mostrarán mensajes de confirmación o error según corresponda.

### 4.6. Resultado esperado

Al finalizar este módulo, el usuario podrá administrar los estudiantes registrados mediante formularios sencillos y visualizar su información en una tabla. Los datos quedarán disponibles durante la ejecución del programa para utilizarlos en las operaciones de inscripción a grupos de estudio.
## 5. Especificación del módulo de grupos de estudio

### 5.1. Propósito

Este módulo permitirá registrar, consultar, modificar y eliminar grupos de estudio para que los estudiantes de la Universidad Técnica de Machala puedan encontrar compañeros con quienes estudiar una misma asignatura en horarios compatibles.

### 5.2. Ventanas del módulo

El módulo contará con dos formularios de Windows Forms:

**FrmAdminGrupos:** será la ventana principal del módulo. Mostrará los grupos registrados mediante un DataGridView y contará con los botones Insertar, Modificar, Eliminar, Ver participantes y Regresar.

**FrmEditGrupo:** permitirá ingresar o modificar los datos de un grupo de estudio. Se utilizará el mismo formulario para ambas operaciones.

### 5.3. Campos del formulario

El formulario FrmEditGrupo incluirá los siguientes campos:

| Campo | Control de Windows Forms | Descripción |
|---|---|---|
| Código | TextBox | Identificador único del grupo. |
| Nombre del grupo | TextBox | Nombre que identificará al grupo. |
| Asignatura | TextBox | Materia que estudiarán los participantes. |
| Día de reunión | ComboBox | Día en que se reunirá el grupo. |
| Hora de inicio | DateTimePicker | Hora en que comenzará la reunión. |
| Hora de finalización | DateTimePicker | Hora en que terminará la reunión. |
| Modalidad | ComboBox | Presencial o virtual. |
| Cupo máximo | NumericUpDown | Cantidad máxima de participantes. |

El formulario también tendrá los botones Guardar y Cancelar.

### 5.4. Funcionamiento de los botones

**Insertar:** abrirá FrmEditGrupo con los campos vacíos para registrar un nuevo grupo.

**Modificar:** permitirá seleccionar un grupo del DataGridView y abrir FrmEditGrupo con sus datos actuales. El código del grupo no podrá modificarse.

**Eliminar:** permitirá eliminar el grupo seleccionado, previa confirmación del usuario. También se eliminarán las inscripciones relacionadas con ese grupo.

**Ver participantes:** mostrará los estudiantes inscritos en el grupo seleccionado, la cantidad de participantes y los cupos disponibles.

**Guardar:** verificará los datos ingresados antes de registrar o actualizar un grupo.

**Cancelar:** cerrará el formulario de edición sin guardar cambios.

**Regresar:** permitirá volver a la ventana principal de UniMatch.

### 5.5. Validaciones

El módulo deberá cumplir las siguientes condiciones:

1. El código del grupo será un número entero mayor que cero.
2. No se permitirán códigos repetidos.
3. Los campos obligatorios deberán estar completos.
4. Se deberá seleccionar un día de reunión y una modalidad.
5. La hora de inicio deberá ser anterior a la hora de finalización.
6. El cupo máximo deberá ser mayor que cero.
7. No se permitirá reducir el cupo máximo por debajo de la cantidad de estudiantes inscritos.
8. El código no podrá modificarse durante la edición.
9. Antes de eliminar un grupo se solicitará confirmación.
10. Si se modifica la asignatura, el día o el horario de un grupo con participantes inscritos, el sistema verificará que las inscripciones existentes continúen siendo compatibles. Si alguna deja de cumplir las condiciones, se rechazará la modificación y se mostrará un mensaje.

### 5.6. Resultado esperado

Al finalizar este módulo, el usuario podrá administrar los grupos de estudio mediante formularios sencillos, consultar sus características y visualizar los participantes inscritos. La información de los grupos estará disponible durante la ejecución del programa para realizar búsquedas e inscripciones.
## 6. Especificación del módulo de inscripciones

### 6.1. Propósito

Este módulo permitirá registrar y administrar las inscripciones de los estudiantes en los grupos de estudio de UniMatch. Su función principal será comprobar que cada estudiante cumpla las condiciones necesarias para participar en un grupo, evitando inscripciones duplicadas, incompatibilidades de horarios y exceso de participantes.

### 6.2. Ventanas del módulo

El módulo contará con dos formularios de Windows Forms:

**FrmAdminInscripciones:** será la ventana principal del módulo. Mostrará las inscripciones realizadas mediante un DataGridView y contará con los botones Inscribir, Cancelar inscripción, Consultar participantes y Regresar.

**FrmEditInscripcion:** permitirá seleccionar un estudiante y un grupo de estudio para realizar una inscripción. Mostrará la información necesaria para comprobar si el estudiante puede participar.

### 6.3. Campos del formulario

El formulario FrmEditInscripcion incluirá los siguientes campos:

| Campo | Control de Windows Forms | Descripción |
|---|---|---|
| Estudiante | ComboBox | Permitirá seleccionar un estudiante registrado. |
| Grupo de estudio | ComboBox | Permitirá seleccionar un grupo existente. |
| Asignatura | Label | Mostrará la asignatura del grupo seleccionado. |
| Día de reunión | Label | Mostrará el día en que se reúne el grupo. |
| Horario | Label | Mostrará la hora de inicio y finalización del grupo. |
| Cupos disponibles | Label | Mostrará la cantidad de cupos que quedan. |

El formulario también contará con los botones Inscribir y Cancelar.

Cada inscripción relacionará internamente el código del estudiante con el código del grupo de estudio.

### 6.4. Funcionamiento de los botones

**Inscribir:** permitirá seleccionar un estudiante y un grupo. Antes de registrar la inscripción, verificará que se cumplan las condiciones establecidas.

**Cancelar inscripción:** permitirá seleccionar una inscripción registrada y eliminarla, previa confirmación del usuario. Al eliminarla, el grupo recuperará un cupo disponible.

**Consultar participantes:** permitirá seleccionar un grupo y visualizar los estudiantes inscritos, la cantidad de participantes y los cupos disponibles.

**Cancelar:** cerrará el formulario de inscripción sin guardar cambios.

**Regresar:** permitirá volver a la ventana principal de UniMatch.

### 6.5. Validaciones

El módulo deberá cumplir las siguientes condiciones:

1. El estudiante y el grupo seleccionados deberán existir.
2. No se permitirá inscribir dos veces al mismo estudiante en un mismo grupo.
3. El grupo deberá tener cupos disponibles.
4. La asignatura del estudiante deberá coincidir con la asignatura del grupo, sin distinguir entre letras mayúsculas y minúsculas.
5. El día disponible del estudiante deberá coincidir con el día de reunión del grupo.
6. La hora de inicio de disponibilidad del estudiante deberá ser menor o igual que la hora de inicio del grupo.
7. La hora de finalización de disponibilidad del estudiante deberá ser mayor o igual que la hora de finalización
8. ## 7. Reglas generales del sistema

Las siguientes reglas deberán cumplirse durante el funcionamiento de UniMatch para garantizar que los registros y las inscripciones se administren correctamente.

### 7.1. Registro y modificación de datos

1. Los estudiantes y los grupos de estudio deberán tener códigos únicos.
2. No se permitirá guardar información cuando existan campos obligatorios vacíos.
3. Los códigos deberán ser números enteros mayores que cero.
4. Las horas de inicio deberán ser anteriores a las horas de finalización.
5. Durante la modificación de estudiantes y grupos, sus códigos permanecerán sin cambios.
6. Las operaciones realizadas deberán mostrar mensajes de confirmación o error.

### 7.2. Administración de inscripciones

1. Cada inscripción relacionará un estudiante existente con un grupo de estudio existente.
2. No se permitirán inscripciones duplicadas.
3. No se permitirá superar el cupo máximo de un grupo.
4. La asignatura y el día disponible del estudiante deberán coincidir con los del grupo.
5. El horario disponible del estudiante deberá cubrir completamente el horario del grupo.
6. Al cancelar una inscripción, el cupo disponible se actualizará automáticamente.

### 7.3. Modificación y eliminación de registros relacionados

1. Al eliminar un estudiante, también se eliminarán sus inscripciones.
2. Al eliminar un grupo, también se eliminarán las inscripciones relacionadas.
3. No se permitirá reducir el cupo máximo de un grupo por debajo de la cantidad de participantes inscritos.
4. Si se modifica la asignatura, el día o el horario de un estudiante o grupo, el sistema verificará que las inscripciones existentes continúen siendo compatibles.
5. Cuando una modificación afecte la compatibilidad de alguna inscripción, el sistema rechazará el cambio y mostrará un mensaje explicativo.
6. Antes de eliminar estudiantes, grupos o inscripciones, se solicitará confirmación al usuario.

### 7.4. Almacenamiento de información

La información se administrará mediante listas de C# durante la ejecución de la aplicación. Los registros estarán disponibles mientras el programa permanezca abierto.

Esta versión demostrativa no incluirá una base de datos ni almacenamiento permanente. Por lo tanto, los registros se perderán al cerrar la aplicación.

## 8. Criterios de verificación

Para comprobar el funcionamiento de UniMatch se realizarán las siguientes verificaciones:

| Código | Verificación | Resultado esperado |
|---|---|---|
| CV01 | Registrar un estudiante con datos válidos. | El estudiante aparece en la tabla. |
| CV02 | Registrar un estudiante con código repetido. | El sistema rechaza el registro. |
| CV03 | Modificar los datos de un estudiante. | Los cambios se muestran correctamente. |
| CV04 | Crear un grupo de estudio válido. | El grupo aparece en la tabla. |
| CV05 | Registrar un grupo con horario incorrecto. | El sistema muestra un mensaje de error. |
| CV06 | Buscar grupos por asignatura. | Se muestran los grupos correspondientes. |
| CV07 | Inscribir un estudiante compatible. | La inscripción se registra correctamente. |
| CV08 | Inscribir dos veces al mismo estudiante en un grupo. | Se rechaza la segunda inscripción. |
| CV09 | Inscribir un estudiante sin disponibilidad compatible. | El sistema rechaza la inscripción. |
| CV10 | Inscribir un estudiante en un grupo lleno. | Se muestra un mensaje indicando que no hay cupos. |
| CV11 | Cancelar una inscripción. | Se elimina la inscripción y se actualizan los cupos. |
| CV12 | Consultar los participantes de un grupo. | Se muestran los estudiantes inscritos y los cupos disponibles. |
| CV13 | Eliminar un estudiante o grupo con inscripciones. | Se eliminan también sus inscripciones relacionadas. |
| CV14 | Modificar un horario que afectaría una inscripción existente. | El sistema rechaza la modificación. |
| CV15 | Abrir y utilizar las ventanas de UniMatch. | Los formularios funcionan y permiten navegar correctamente. |

Estas verificaciones se realizarán después de implementar las funcionalidades. Los resultados obtenidos se documentarán como evidencias del funcionamiento de la aplicación.

## 9. Resultado esperado de la Fase 2

Al finalizar esta fase, UniMatch contará con especificaciones que describen sus formularios, campos, operaciones, validaciones y criterios de verificación.

Esta documentación servirá como base para elaborar el plan técnico y organizar las tareas de implementación mediante GitHub Spec Kit. Posteriormente, GitHub Copilot se utilizará como apoyo para generar el código en C# Windows Forms, el cual será revisado y probado por ambos integrantes.
