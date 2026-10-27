# Instrucciones de UniMatch

Consulta `docs/requerimientos.md`, `docs/especificaciones.md` y `docs/plan-tecnico.md` antes de modificar el proyecto.

- Mantén la separación `Entidades`, `Controlador` y `Formularios`.
- Usa listas en memoria y controladores estáticos `TLista` para este alcance.
- Toda operación debe validar primero y modificar las listas solo cuando sea válida.
- Las relaciones estudiante-grupo se almacenan en `TListaInscripcion`; las eliminaciones deben ser en cascada.
- Los formularios Windows Forms deben conservar sus archivos `.Designer.cs` y ser compatibles con Visual Studio.
- Ejecuta `dotnet test UniMatch.sln` antes de entregar cambios.
