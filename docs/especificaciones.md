# Fase 2 - Especificaciones funcionales de UniMatch

## 1. Información general

**Proyecto:** UniMatch  
**Asignatura:** Programación Avanzada  
**Integrantes:**
- Kennedy Vinicio Delgado Narváez.
- Giana Esthefany Torres Armijos.

**Fase:** Especificación funcional de la metodología SDD.  
**Estado:** Propuesta corregida para revisión de ambos integrantes.  
**Documento de referencia:** docs/requerimientos.md.

## 2. Objetivo de la especificación

Definir cómo funcionarán los módulos de estudiantes, grupos de estudio e inscripciones de UniMatch, incluyendo sus ventanas, campos, operaciones, reglas y criterios de verificación.

Estas especificaciones servirán como base para elaborar el plan técnico y generar posteriormente el código en C# con apoyo de GitHub Copilot.

La aplicación se desarrollará con Windows Forms y conservará la organización en Entidades, Controlador y Formularios. La información se administrará mediante listas de C# durante la ejecución del programa.

Para el alcance de esta entrega, cada estudiante registrará una asignatura, un día disponible y una franja horaria.

## 3. Organización y presentación de la interfaz

### 3.1. Ventana principal

La ventana principal, denominada **FrmPrincipal**, permitirá acceder a:

- Administración de estudiantes.
- Administración de grupos de estudio.
- Administración de inscripciones.
- Salida de la aplicación.

Los accesos estarán identificados con nombres claros y mantendrán una distribución ordenada.

### 3.2. Presentación de las ventanas

Las ventanas deberán cumplir los siguientes criterios:

1. Mantener una tipografía y una paleta de colores consistentes.
2. Presentar un encabezado con el nombre del módulo.
3. Distribuir los campos con etiquetas visibles, alineación y espaciado uniforme.
4. Agrupar los datos relacionados, como información personal y disponibilidad.
5. Mantener tamaños y nombres coherentes en los botones.
6. Presentar tablas con encabezados comprensibles y columnas legibles.
7. Mostrar horarios en formato de 24 horas, con horas y minutos.
8. Mantener visibles los controles al redimensionar las ventanas de administración.
9. Mostrar mensajes que expliquen el resultado de las operaciones y los motivos de los errores.

### 3.3. Comportamiento de las tablas

Los DataGridView se utilizarán para consultar y seleccionar registros. La edición se realizará mediante los formularios correspondientes.

Las operaciones que requieran un registro seleccionado deberán comprobar que exista una selección válida. Si no existe, se mostrará un mensaje indicando que debe seleccionarse un registro.

Cuando una tabla esté vacía, la ventana deberá permanecer operativa y mostrar que no existen registros para consultar.

## 4. Especificación del módulo de estudiantes

### 4.1. Propósito

Permitir el registro, consulta, modificación y eliminación de estudiantes, incluyendo su asignatura de interés y disponibilidad para participar en grupos de estudio.

Este módulo corresponde a los requerimientos RF01 y RF02.

### 4.2. Ventanas del módulo

**FrmAdminEstudiantes:** mostrará los estudiantes registrados mediante un DataGridView. Contará con los botones Insertar, Modificar, Eliminar y Regresar.

La tabla mostrará código, nombres, apellidos, carrera, asignatura, día disponible, hora de inicio y hora de finalización.

**FrmEditEstudiante:** permitirá registrar o modificar un estudiante utilizando el mismo formulario.

### 4.3. Campos del formulario

| Campo | Control | Condición |
|---|---|---|
| Código | TextBox | Número entero mayor que cero y único entre los estudiantes. |
| Nombres | TextBox | Obligatorio. |
| Apellidos | TextBox | Obligatorio. |
| Carrera | TextBox | Obligatorio. |
| Asignatura | TextBox | Obligatorio. |
| Día disponible | ComboBox | Selección de un día de lunes a domingo. |
| Hora de inicio | DateTimePicker | Inicio de la disponibilidad. |
| Hora de finalización | DateTimePicker | Finalización de la disponibilidad. |

El formulario contará con los botones Guardar y Cancelar.

### 4.4. Funcionamiento de los botones

**Insertar:** abrirá FrmEditEstudiante en modo de registro, con los campos de texto vacíos y el código habilitado.

**Modificar:** abrirá FrmEditEstudiante con los datos del estudiante seleccionado. Su código permanecerá bloqueado.

**Eliminar:** solicitará confirmación antes de eliminar al estudiante. La confirmación informará que también se eliminarán sus inscripciones relacionadas.

**Guardar:** validará los datos y registrará o actualizará al estudiante. Si la operación se realiza correctamente, cerrará el formulario de edición y actualizará la tabla.

**Cancelar:** cerrará el formulario de edición conservando los datos anteriores.

**Regresar:** permitirá volver a la ventana principal.

### 4.5. Validaciones

1. El código deberá ser un número entero mayor que cero.
2. El código deberá ser único dentro de la lista de estudiantes.
3. Los campos obligatorios deberán contener información.
4. Un texto compuesto únicamente por espacios se considerará vacío.
5. Se deberá seleccionar un día disponible.
6. La hora de inicio deberá ser anterior a la hora de finalización.
7. El código permanecerá sin cambios durante la edición.
8. Al modificar la asignatura, el día o la disponibilidad, se comprobarán todas las inscripciones del estudiante.
9. Si alguna inscripción deja de ser compatible, se rechazará la modificación y se explicará el motivo.
10. Cuando una validación falle, se conservarán los datos anteriores del estudiante y sus inscripciones.

### 4.6. Resultado esperado

El usuario podrá administrar estudiantes mediante ventanas organizadas y consultar sus datos actualizados en la tabla.

La información registrada estará disponible para realizar inscripciones durante la sesión de trabajo.

## 5. Especificación del módulo de grupos de estudio

### 5.1. Propósito

Permitir la creación, consulta, modificación, eliminación y búsqueda de grupos de estudio, así como la consulta de sus participantes.

Este módulo corresponde a los requerimientos RF03, RF04, RF05 y RF07.

### 5.2. Ventanas del módulo

**FrmAdminGrupos:** mostrará los grupos registrados mediante un DataGridView. Incluirá un campo para buscar por asignatura y los botones Buscar, Limpiar, Insertar, Modificar, Eliminar, Ver participantes y Regresar.

La tabla mostrará código, nombre del grupo, asignatura, día, horario, modalidad, cupo máximo, participantes inscritos y cupos disponibles.

**FrmEditGrupo:** permitirá registrar o modificar un grupo utilizando el mismo formulario.

**FrmParticipantes:** mostrará los estudiantes inscritos en un grupo. Será una ventana compartida con el módulo de inscripciones.

### 5.3. Campos del formulario de edición

| Campo | Control | Condición |
|---|---|---|
| Código | TextBox | Número entero mayor que cero y único entre los grupos. |
| Nombre del grupo | TextBox | Obligatorio. |
| Asignatura | TextBox | Obligatorio. |
| Día de reunión | ComboBox | Selección de un día de lunes a domingo. |
| Hora de inicio | DateTimePicker | Inicio de la reunión. |
| Hora de finalización | DateTimePicker | Finalización de la reunión. |
| Modalidad | ComboBox | Presencial o virtual. |
| Cupo máximo | NumericUpDown | Número entero mayor que cero. |

El formulario contará con los botones Guardar y Cancelar.

### 5.4. Funcionamiento de los botones

**Insertar:** abrirá FrmEditGrupo en modo de registro.

**Modificar:** abrirá FrmEditGrupo con los datos del grupo seleccionado. Su código permanecerá bloqueado.

**Eliminar:** solicitará confirmación antes de eliminar el grupo. La confirmación informará que también se eliminarán sus inscripciones relacionadas.

**Ver participantes:** abrirá FrmParticipantes con el grupo seleccionado. Mostrará sus estudiantes inscritos, la cantidad de participantes y los cupos disponibles.

**Guardar:** validará los datos antes de registrar o actualizar el grupo. Si la operación se realiza correctamente, cerrará el formulario de edición y actualizará la tabla.

**Cancelar:** cerrará el formulario de edición conservando los datos anteriores.

**Regresar:** permitirá volver a la ventana principal.

### 5.5. Validaciones

1. El código deberá ser un número entero mayor que cero.
2. El código deberá ser único dentro de la lista de grupos.
3. Los campos obligatorios deberán contener información.
4. Se deberá seleccionar un día de reunión y una modalidad.
5. La hora de inicio deberá ser anterior a la hora de finalización.
6. El cupo máximo deberá ser un número entero mayor que cero.
7. El cupo máximo deberá ser mayor o igual que la cantidad de participantes inscritos.
8. El código permanecerá sin cambios durante la edición.
9. Al modificar la asignatura, el día o el horario, se comprobarán todas las inscripciones del grupo.
10. Si alguna inscripción deja de ser compatible, se rechazará la modificación y se explicará el motivo.
11. Cuando una validación falle, se conservarán los datos anteriores del grupo y sus inscripciones.

### 5.6. Búsqueda por asignatura

La búsqueda se realizará desde FrmAdminGrupos.

1. El usuario escribirá una asignatura o parte de su nombre.
2. Al pulsar Buscar, se mostrarán los grupos cuya asignatura contenga el texto ingresado.
3. La búsqueda ignorará las diferencias entre mayúsculas y minúsculas.
4. Se eliminarán los espacios al inicio y al final del texto de búsqueda.
5. Si el campo está vacío, se mostrarán todos los grupos.
6. Si no existen coincidencias, la tabla quedará vacía y se mostrará un mensaje informativo.
7. Al pulsar Limpiar, se vaciará el campo de búsqueda y se mostrarán todos los grupos.

Los resultados permitirán consultar el día, horario, modalidad y cupos disponibles de cada grupo.

### 5.7. Resultado esperado

El usuario podrá administrar y buscar grupos de estudio, consultar sus características y visualizar sus participantes mediante ventanas con una presentación consistente.

## 6. Especificación del módulo de inscripciones

### 6.1. Propósito

Permitir la inscripción de estudiantes en grupos de estudio y la cancelación de inscripciones, verificando disponibilidad, compatibilidad y cupos.

Este módulo corresponde a los requerimientos RF06 y RF07.

### 6.2. Ventanas del módulo

**FrmAdminInscripciones:** mostrará las inscripciones mediante un DataGridView. Contará con los botones Inscribir, Cancelar inscripción, Consultar participantes y Regresar.

La tabla mostrará el código y nombre del estudiante, el código y nombre del grupo, la asignatura, el día y el horario.

**FrmEditInscripcion:** permitirá seleccionar un estudiante y un grupo para registrar una inscripción.

La consulta de participantes utilizará FrmParticipantes. Desde esta ventana se podrá seleccionar un grupo, incluyendo grupos que todavía tengan cero participantes.

### 6.3. Campos del formulario de inscripción

| Campo | Control | Descripción |
|---|---|---|
| Estudiante | ComboBox | Mostrará el código y nombre completo del estudiante. |
| Grupo de estudio | ComboBox | Mostrará el código y nombre del grupo. |
| Asignatura | Label | Asignatura del grupo seleccionado. |
| Día de reunión | Label | Día del grupo seleccionado. |
| Horario | Label | Inicio y finalización de la reunión. |
| Modalidad | Label | Modalidad del grupo seleccionado. |
| Cupos disponibles | Label | Cupos restantes del grupo. |

El formulario contará con los botones Inscribir y Cancelar.

Cada inscripción relacionará internamente el código del estudiante con el código del grupo. Esta combinación identificará la inscripción y permitirá comprobar que no esté repetida.

### 6.4. Funcionamiento de los botones

**Inscribir, en la ventana de administración:** abrirá FrmEditInscripcion.

**Inscribir, en el formulario de registro:** comprobará las condiciones y registrará la inscripción cuando todas se cumplan. Después actualizará las tablas y los cupos mostrados.

**Cancelar inscripción:** solicitará confirmación para eliminar la inscripción seleccionada. Al eliminarla, se actualizarán la cantidad de participantes y los cupos disponibles.

**Consultar participantes:** abrirá FrmParticipantes para seleccionar un grupo y consultar sus estudiantes inscritos.

**Cancelar:** cerrará el formulario de inscripción conservando los registros existentes.

**Regresar:** permitirá volver a la ventana principal.

### 6.5. Validaciones de inscripción

1. Se deberá seleccionar un estudiante y un grupo.
2. Ambos registros deberán existir.
3. La combinación del estudiante y el grupo deberá ser única entre las inscripciones.
4. El grupo deberá tener al menos un cupo disponible.
5. La asignatura del estudiante deberá coincidir con la del grupo, ignorando diferencias entre mayúsculas y minúsculas y espacios al inicio o al final.
6. El día disponible del estudiante deberá coincidir con el día de reunión del grupo.
7. La hora de inicio de disponibilidad del estudiante deberá ser menor o igual que la hora de inicio del grupo.
8. La hora de finalización de disponibilidad del estudiante deberá ser mayor o igual que la hora de finalización del grupo.
9. La disponibilidad deberá cubrir completamente el horario del grupo. Una coincidencia parcial será rechazada.
10. Se aceptará una disponibilidad que coincida exactamente con el inicio y la finalización del grupo.
11. Cuando una inscripción sea rechazada, se mostrará el motivo y se conservarán los registros y cupos anteriores.

Un estudiante podrá pertenecer a varios grupos siempre que cumpla las condiciones de cada inscripción.

### 6.6. Resultado esperado

El usuario podrá registrar inscripciones válidas, cancelar inscripciones y consultar participantes.

La cantidad de inscritos y los cupos disponibles deberán reflejar las operaciones realizadas.

## 7. Reglas generales del sistema

### 7.1. Registro y modificación de datos

1. Los códigos serán números enteros mayores que cero.
2. La unicidad del código se comprobará dentro de cada módulo: estudiantes o grupos.
3. Los códigos permanecerán sin cambios durante la edición.
4. Los campos obligatorios rechazarán valores vacíos o compuestos únicamente por espacios.
5. Los textos se guardarán eliminando espacios al inicio y al final.
6. Los días y las modalidades se seleccionarán de las opciones disponibles.
7. Los horarios corresponderán a un mismo día y se compararán utilizando horas y minutos.
8. La hora de inicio deberá ser anterior a la hora de finalización.
9. Los datos se actualizarán después de superar todas las validaciones de la operación.
10. Los mensajes de error indicarán el campo o la condición que debe corregirse.

### 7.2. Administración de inscripciones y cupos

1. Cada inscripción relacionará un estudiante existente con un grupo existente.
2. Cada combinación de estudiante y grupo deberá ser única.
3. La asignatura y el día deberán ser compatibles.
4. La disponibilidad del estudiante deberá cubrir completamente el horario del grupo.
5. La cantidad de inscritos deberá permanecer dentro del cupo máximo.
6. Los cupos disponibles se obtendrán de la siguiente relación:

   **Cupos disponibles = Cupo máximo − Cantidad de inscritos.**

7. Al registrar, cancelar o eliminar inscripciones, se actualizará la información de participantes y cupos.
8. Las operaciones rechazadas conservarán las cantidades anteriores.

### 7.3. Registros relacionados

1. Al eliminar un estudiante, se eliminarán sus inscripciones.
2. Al eliminar un grupo, se eliminarán sus inscripciones.
3. Las eliminaciones conservarán los registros ajenos a la operación.
4. Antes de eliminar estudiantes, grupos o inscripciones, se solicitará confirmación.
5. Si el usuario cancela la confirmación, se conservará toda la información.
6. El cupo máximo deberá ser mayor o igual que la cantidad de participantes inscritos.
7. Los cambios de asignatura, día o disponibilidad de estudiantes y grupos deberán mantener compatibles todas sus inscripciones.
8. Si alguna inscripción queda incompatible, se rechazará la modificación completa.
9. Las consultas de inscripciones y participantes mostrarán los datos vigentes de estudiantes y grupos.

### 7.4. Almacenamiento de información

UniMatch administrará la información mediante listas de C# durante la ejecución de la aplicación.

Los registros estarán disponibles mientras el programa permanezca abierto. Al cerrar la aplicación, finalizará la sesión y se perderán los datos registrados durante ella.

Este comportamiento corresponde al alcance definido en la Fase 1 y deberá indicarse en las instrucciones de uso.

## 8. Criterios de verificación

Las siguientes verificaciones se realizarán después de implementar las funcionalidades.

**Estado de las verificaciones: pendientes de ejecución.**

| Código | Verificación | Resultado esperado |
|---|---|---|
| CV01 | Registrar un estudiante con datos válidos. | El estudiante aparece en la tabla con sus datos. |
| CV02 | Registrar un estudiante con un código existente. | Se rechaza el registro y se conserva el estudiante anterior. |
| CV03 | Modificar datos válidos de un estudiante. | Los cambios aparecen en las consultas correspondientes. |
| CV04 | Crear un grupo con datos válidos. | El grupo aparece en la tabla con sus cupos disponibles. |
| CV05 | Guardar un grupo con hora de inicio igual o posterior a la finalización. | Se rechaza la operación con un mensaje explicativo. |
| CV06 | Buscar grupos utilizando parte de una asignatura y diferentes mayúsculas. | Se muestran los grupos que coinciden con la búsqueda. |
| CV07 | Inscribir un estudiante compatible en un grupo con cupo. | Se registra la inscripción y se actualizan participantes y cupos. |
| CV08 | Repetir la inscripción del mismo estudiante en el mismo grupo. | Se rechaza la segunda inscripción y se conservan las cantidades anteriores. |
| CV09 | Inscribir un estudiante con asignatura, día o disponibilidad incompatibles. | Se rechaza la inscripción indicando el motivo. |
| CV10 | Inscribir un estudiante en un grupo lleno. | Se informa que el grupo carece de cupos disponibles. |
| CV11 | Cancelar una inscripción y confirmar la operación. | Se elimina la inscripción y se recupera un cupo. |
| CV12 | Consultar los participantes de un grupo. | Se muestran los estudiantes inscritos, el total y los cupos disponibles. |
| CV13 | Eliminar un estudiante o grupo con inscripciones, confirmando la operación. | Se eliminan el registro y sus inscripciones relacionadas, conservando las demás. |
| CV14 | Modificar un horario que volvería incompatible una inscripción existente. | Se rechaza el cambio y se conservan todos los datos anteriores. |
| CV15 | Abrir los módulos y utilizar Regresar, Guardar y Cancelar. | La navegación y los botones realizan las operaciones especificadas. |
| CV16 | Reducir el cupo máximo por debajo de la cantidad de inscritos. | Se rechaza la modificación y se conserva el cupo anterior. |
| CV17 | Inscribir usando asignaturas equivalentes con diferencias de mayúsculas o espacios externos. | La comparación reconoce que las asignaturas coinciden. |
| CV18 | Inscribir con disponibilidad exactamente igual al horario del grupo. | La inscripción se acepta si cumple las demás condiciones. |
| CV19 | Inscribir con disponibilidad que cubra solamente una parte del horario del grupo. | Se rechaza la inscripción. |
| CV20 | Buscar con el campo vacío o pulsar Limpiar. | Se muestran todos los grupos registrados. |
| CV21 | Buscar una asignatura sin coincidencias. | Se muestra una tabla vacía y un mensaje informativo. |
| CV22 | Guardar códigos inválidos o campos obligatorios vacíos. | Se rechaza la operación y se indica qué debe corregirse. |
| CV23 | Intentar modificar, eliminar o cancelar una inscripción sin seleccionar un registro. | Se solicita una selección válida y se conserva la información. |
| CV24 | Editar datos y pulsar Cancelar. | Se cierra el formulario y se conservan los valores anteriores. |
| CV25 | Cancelar la confirmación de una eliminación. | Se conservan el registro y sus inscripciones. |
| CV26 | Consultar un grupo con cero participantes. | Se muestra la tabla vacía, cero inscritos y la totalidad de sus cupos. |
| CV27 | Modificar una asignatura o un día que volvería incompatible una inscripción. | Se rechaza la modificación completa. |
| CV28 | Intentar inscribir sin seleccionar estudiante o grupo. | Se solicita completar la selección y se conservan los registros existentes. |
| CV29 | Revisar la presentación de las ventanas. | Se cumplen los criterios de alineación, legibilidad, distribución y consistencia visual. |
| CV30 | Cerrar y volver a abrir la aplicación. | Se inicia una nueva sesión y los datos ingresados en la sesión anterior dejan de estar disponibles. |

El plan técnico determinará cuáles verificaciones se automatizarán y cuáles se realizarán manualmente en la interfaz.

Los resultados reales se documentarán después de ejecutar las verificaciones. Las pruebas automatizadas configuradas se integrarán con el proceso de GitHub Actions.

## 9. Resultado esperado de la Fase 2

Al aprobarse esta fase, ambos integrantes contarán con una descripción acordada de las ventanas, campos, operaciones, reglas y verificaciones de UniMatch.

El documento servirá como entrada para elaborar el plan técnico y organizar las tareas mediante GitHub Spec Kit.

Posteriormente, GitHub Copilot se utilizará para generar el código en C# Windows Forms a partir de las especificaciones y del plan revisado. Ambos integrantes revisarán el código, comprobarán su funcionamiento y registrarán las correcciones y evidencias correspondientes.
