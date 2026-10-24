# Fase 3 - Plan técnico de UniMatch

## 1. Información general

**Proyecto:** UniMatch  
**Asignatura:** Programación Avanzada  
**Integrantes:**
- Kennedy Vinicio Delgado Narváez.
- Giana Esthefany Torres Armijos.

**Fase:** Plan técnico de la metodología SDD.

## 2. Objetivo del plan técnico

Establecer la organización técnica que se utilizará para desarrollar UniMatch, definiendo las herramientas, las clases, los controladores, los formularios y las actividades necesarias para implementar las funcionalidades descritas en los requerimientos y las especificaciones aprobadas.

Este plan servirá como guía para desarrollar la aplicación en C# Windows Forms con apoyo de GitHub Copilot, manteniendo una estructura de código clara y organizada.

## 3. Herramientas y tecnologías

Para el desarrollo de UniMatch se utilizarán las siguientes herramientas:

| Herramienta | Utilización en el proyecto |
|---|---|
| C# | Lenguaje de programación de la aplicación. |
| Windows Forms | Creación de las ventanas e interfaz gráfica. |
| Visual Studio 2026 | Entorno para desarrollar, ejecutar y revisar el programa. |
| .NET 8 (Windows Desktop) | Plataforma LTS utilizada para ejecutar la aplicación y Windows Forms. |
| Git | Registro y control de los cambios realizados. |
| GitHub | Almacenamiento del proyecto y trabajo colaborativo. |
| GitHub Spec Kit | Organización del desarrollo basado en especificaciones. |
| GitHub Copilot | Apoyo para generar código a partir de las especificaciones. |
| GitHub Actions | Automatización de la compilación y las pruebas configuradas. |

La aplicación será una versión demostrativa de escritorio para Windows. Inicialmente, los registros se administrarán mediante listas de C# durante la ejecución del programa, sin utilizar una base de datos.

El desarrollo seguirá las especificaciones aprobadas en la Fase 2. El código generado con apoyo de GitHub Copilot será revisado por los integrantes antes de incorporarlo a la rama principal.
## 4. Estructura del proyecto

UniMatch se organizará en tres carpetas principales: Entidades, Controlador y Formularios. Esta distribución permitirá separar los datos del sistema, las operaciones necesarias para administrarlos y las ventanas que utilizará el usuario.

### 4.1. Carpeta Entidades

Esta carpeta contendrá las clases que representan la información principal de UniMatch.

**Clase Estudiante**

Representará a los estudiantes registrados en el sistema. Contendrá los siguientes atributos:

- Codigo: identificador único del estudiante.
- Nombres: nombres del estudiante.
- Apellidos: apellidos del estudiante.
- Carrera: carrera universitaria.
- Asignatura: materia que desea estudiar.
- DiaDisponible: día disponible para estudiar.
- HoraInicio: inicio de su disponibilidad.
- HoraFin: finalización de su disponibilidad.

**Clase GrupoEstudio**

Representará los grupos de estudio creados en la aplicación. Contendrá los siguientes atributos:

- Codigo: identificador único del grupo.
- Nombre: nombre del grupo de estudio.
- Asignatura: materia que estudiarán.
- Dia: día de reunión.
- HoraInicio: hora de inicio de la reunión.
- HoraFin: hora de finalización.
- Modalidad: presencial o virtual.
- CupoMaximo: número máximo de participantes.

**Clase Inscripcion**

Representará la relación entre un estudiante y un grupo de estudio. Contendrá los siguientes atributos:

- CodigoEstudiante: código del estudiante inscrito.
- CodigoGrupo: código del grupo al que pertenece.

Esta clase permitirá identificar las inscripciones y comprobar que un estudiante no se registre dos veces en el mismo grupo.

### 4.2. Carpeta Controlador

Esta carpeta contendrá las clases encargadas de administrar las listas de información y realizar las operaciones del sistema.

**Clase TListaEstudiante**

Administrará los estudiantes registrados. Incluirá operaciones para:

- Agregar estudiantes.
- Buscar estudiantes por código.
- Modificar la información de estudiantes.
- Eliminar estudiantes.
- Consultar la lista de estudiantes.

**Clase TListaGrupoEstudio**

Administrará los grupos de estudio. Incluirá operaciones para:

- Agregar grupos.
- Buscar grupos por código o asignatura.
- Modificar grupos.
- Eliminar grupos.
- Consultar los grupos registrados.

**Clase TListaInscripcion**

Administrará las inscripciones de los estudiantes. Incluirá operaciones para:

- Registrar inscripciones.
- Comprobar inscripciones duplicadas.
- Verificar la compatibilidad de asignaturas y horarios.
- Comprobar los cupos disponibles.
- Cancelar inscripciones.
- Consultar los participantes de cada grupo.
- Eliminar inscripciones relacionadas cuando se elimine un estudiante o grupo.

Estas clases trabajarán con listas de C# y se encargarán de aplicar las reglas definidas en las especificaciones funcionales.

### 4.3. Carpeta Formularios

Esta carpeta contendrá las ventanas de Windows Forms que permitirán al usuario utilizar UniMatch.

| Formulario | Función |
|---|---|
| FrmPrincipal | Presentar el menú principal de UniMatch. |
| FrmAdminEstudiantes | Mostrar y administrar estudiantes. |
| FrmEditEstudiante | Insertar o modificar estudiantes. |
| FrmAdminGrupos | Mostrar, buscar y administrar grupos de estudio. |
| FrmEditGrupo | Insertar o modificar grupos. |
| FrmAdminInscripciones | Mostrar y administrar las inscripciones. |
| FrmEditInscripcion | Registrar estudiantes en grupos de estudio. |

Los formularios administrativos utilizarán controles DataGridView para presentar los registros. Los formularios de edición incluirán los campos y botones definidos en la Fase 2.

### 4.4. Relación entre los componentes

Las entidades almacenarán la información de los estudiantes, grupos e inscripciones.

Los controladores administrarán estos registros y comprobarán las condiciones necesarias para realizar cada operación.

Los formularios permitirán que el usuario ingrese y consulte información, comunicándose con los controladores para guardar, modificar o eliminar los registros.

De esta manera, UniMatch mantendrá una estructura organizada, evitando concentrar todas las operaciones en una sola ventana.
## 5. Plan de implementación y distribución de tareas

El desarrollo de UniMatch se realizará de manera organizada, siguiendo los requerimientos y las especificaciones aprobadas en las fases anteriores. Antes de generar el código, se establecerán las tareas necesarias para construir los módulos de estudiantes, grupos de estudio e inscripciones.

Se utilizará GitHub Spec Kit como apoyo para organizar las especificaciones, el plan y las tareas de implementación. Posteriormente, GitHub Copilot ayudará a generar el código en C# Windows Forms, el cual será revisado y probado por los integrantes.

### 5.1. Orden de implementación

El desarrollo de la aplicación se organizará en las siguientes actividades:

**Actividad 1. Preparación del proyecto**

Se creó la solución `UniMatch.sln` con un proyecto Windows Forms `net8.0-windows` y un proyecto de pruebas compatible. Se establecieron las carpetas Entidades, Controlador y Formularios y se comprobó la compilación.

**Actividad 2. Desarrollo del módulo de estudiantes**

Se implementarán las clases Estudiante y TListaEstudiante, junto con los formularios correspondientes. Se realizarán las operaciones de insertar, consultar, modificar y eliminar estudiantes, aplicando las validaciones establecidas.

**Actividad 3. Desarrollo del módulo de grupos de estudio**

Se implementarán las clases GrupoEstudio y TListaGrupoEstudio, junto con sus formularios. El módulo permitirá administrar los grupos, consultar sus características y buscar grupos por asignatura.

**Actividad 4. Desarrollo del módulo de inscripciones**

Se implementarán las clases Inscripcion y TListaInscripcion, junto con los formularios correspondientes. Se comprobarán las reglas de compatibilidad de asignatura, día, horario y cupos disponibles.

**Actividad 5. Integración de los módulos**

Se conectarán los formularios con sus controladores y se verificará que las operaciones de estudiantes, grupos e inscripciones funcionen conjuntamente. También se preparará el menú principal de UniMatch.

**Actividad 6. Pruebas y correcciones**

Se realizarán pruebas para comprobar el cumplimiento de los criterios de aceptación. Los errores encontrados se corregirán y se verificará nuevamente el funcionamiento de las operaciones.

**Actividad 7. Automatización y documentación**

Se configurará GitHub Actions para comprobar la compilación del proyecto y ejecutar las pruebas automatizadas disponibles. Finalmente, se reunirán las evidencias del desarrollo y las instrucciones para ejecutar la aplicación.

### 5.2. Distribución propuesta de tareas

Para facilitar el trabajo colaborativo, se propone la siguiente distribución inicial:

| Integrante | Responsabilidades |
|---|---|
| Kennedy Vinicio Delgado Narváez | Preparación del proyecto, desarrollo del módulo de grupos de estudio y colaboración en la integración de los módulos. |
| Giana Esthefany Torres Armijos | Desarrollo del módulo de estudiantes, apoyo en el módulo de inscripciones y documentación de las pruebas realizadas. |
| Ambos integrantes | Revisión de especificaciones, desarrollo e integración de inscripciones, revisión del código generado, Pull Requests, pruebas y correcciones finales. |

Esta distribución podrá ajustarse de común acuerdo durante el desarrollo del proyecto, según las necesidades que se presenten.

### 5.3. Organización del trabajo mediante GitHub

Las actividades se organizarán mediante GitHub Issues, donde se describirán las tareas pendientes y los criterios que deberán cumplirse.

Cada integrante utilizará una rama de trabajo para desarrollar las funcionalidades asignadas, evitando modificar directamente la rama principal.

Una vez completada una tarea, se registrarán los cambios mediante commits y se creará un Pull Request. El otro integrante revisará el código y, cuando corresponda, aprobará su incorporación a la rama principal.

Este procedimiento permitirá conservar un historial de los cambios y evidenciar la participación de ambos integrantes.

### 5.4. Uso de GitHub Copilot

GitHub Copilot se utilizará como asistente durante la implementación de UniMatch. Las instrucciones proporcionadas estarán basadas en las especificaciones y tareas previamente definidas.

La generación del código se realizará por componentes, comenzando con las entidades y los controladores, para continuar posteriormente con los formularios.

El código generado no se incorporará automáticamente al proyecto sin revisión. Ambos integrantes comprobarán que las clases, los métodos y las validaciones correspondan con las especificaciones establecidas.

### 5.5. Resultado esperado

Al finalizar las actividades planificadas, se espera contar con una versión demostrativa y funcional de UniMatch, que permita administrar estudiantes, grupos de estudio e inscripciones mediante una interfaz desarrollada en Windows Forms.

Asimismo, se espera disponer de un historial de trabajo colaborativo en GitHub, pruebas de funcionamiento, una configuración de GitHub Actions y la documentación necesaria para presentar el proyecto.
## 6. Estrategia de pruebas y verificación

Para comprobar el funcionamiento de UniMatch, se realizarán pruebas que permitan verificar que las operaciones del sistema cumplan los requerimientos y las especificaciones aprobadas.

Las pruebas se efectuarán durante y después del desarrollo de los módulos de estudiantes, grupos de estudio e inscripciones. Cuando se identifiquen errores, se realizarán las correcciones correspondientes y se repetirá la verificación.

### 6.1. Pruebas funcionales

Se comprobará que cada módulo permita realizar correctamente sus operaciones principales.

**Módulo de estudiantes:** se verificará el registro, la consulta, la modificación y la eliminación de estudiantes, así como las validaciones de códigos y horarios.

**Módulo de grupos de estudio:** se comprobará la creación, búsqueda, modificación y eliminación de grupos, incluyendo la validación de cupos y horarios.

**Módulo de inscripciones:** se verificará que los estudiantes puedan inscribirse en grupos compatibles y que el sistema rechace las inscripciones duplicadas, los horarios incompatibles y los grupos sin cupos disponibles.

### 6.2. Casos de prueba

Se utilizarán los siguientes casos para verificar las funcionalidades principales de UniMatch:

| Prueba | Acción | Resultado esperado |
|---|---|---|
| P01 | Registrar un estudiante con datos correctos. | El estudiante se guarda y aparece en la tabla. |
| P02 | Registrar un estudiante con código repetido. | El sistema rechaza el registro. |
| P03 | Crear un grupo de estudio con datos válidos. | El grupo aparece en la tabla. |
| P04 | Buscar grupos por asignatura. | Se muestran los grupos correspondientes. |
| P05 | Inscribir un estudiante con asignatura y horario compatibles. | La inscripción se realiza correctamente. |
| P06 | Inscribir dos veces al mismo estudiante en un grupo. | Se rechaza la segunda inscripción. |
| P07 | Inscribir un estudiante en un grupo lleno. | Se muestra un mensaje indicando que no hay cupos. |
| P08 | Inscribir un estudiante con horario incompatible. | El sistema rechaza la inscripción. |
| P09 | Cancelar una inscripción. | Se elimina la inscripción y se actualizan los cupos disponibles. |
| P10 | Eliminar un estudiante con inscripciones. | Se eliminan también las inscripciones relacionadas. |
| P11 | Reducir el cupo máximo por debajo de los inscritos. | El sistema rechaza la modificación. |
| P12 | Modificar datos que vuelvan incompatible una inscripción. | El sistema rechaza el cambio y conserva los datos anteriores. |

### 6.3. Pruebas automatizadas

Se prepararán pruebas automatizadas para comprobar las reglas principales de UniMatch, especialmente las validaciones de registros, horarios, cupos e inscripciones.

Estas pruebas permitirán verificar el comportamiento de los controladores sin depender de la interacción manual con los formularios.

También se configurará GitHub Actions para ejecutar un proceso de verificación cuando se envíen cambios al repositorio.

Debido a que UniMatch se desarrollará con Windows Forms, se utilizará un entorno de Windows para la compilación automatizada. La configuración específica se establecerá una vez creada la solución en Visual Studio.

### 6.4. Verificación de la interfaz

Se comprobará que los formularios de UniMatch presenten correctamente la información y permitan utilizar sus funciones.

Se revisará que los campos sean visibles, los botones realicen las acciones correspondientes, las tablas muestren los registros y los mensajes permitan comprender los errores.

También se verificará que el usuario pueda navegar entre las ventanas sin dificultades.

### 6.5. Registro de resultados y evidencias

Durante las pruebas se registrarán los resultados obtenidos y los errores encontrados.

Se recopilarán capturas de pantalla que permitan evidenciar:

- El funcionamiento de los formularios de UniMatch.
- Las operaciones de registro, modificación, eliminación y búsqueda.
- Las validaciones de inscripciones, horarios y cupos.
- La compilación y ejecución de la solución.
- La ejecución de GitHub Actions.
- Los commits, Pull Requests y revisiones realizados por ambos integrantes.

Estas evidencias se incorporarán posteriormente al informe final del proyecto.

### 6.6. Resultado esperado

Al finalizar la verificación, se espera comprobar que la versión demostrativa de UniMatch cumple las funciones establecidas en sus requerimientos y especificaciones.

Los resultados de las pruebas permitirán identificar las correcciones necesarias antes de presentar la aplicación, mientras que las evidencias servirán para demostrar el proceso de desarrollo y el trabajo colaborativo realizado.
