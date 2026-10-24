# UniMatch-SDD

Sistema de grupos de estudio para estudiantes de la UTMACH desarrollado en C# Windows Forms mediante SDD.

## Requisitos

- Windows 10/11.
- Visual Studio 2022/2026 con la carga **Desarrollo de escritorio .NET**.
- .NET 8 SDK.

## Abrir, compilar y ejecutar

1. Clona el repositorio y abre `UniMatch.sln` en Visual Studio.
2. Selecciona `UniMatch` como proyecto de inicio.
3. Compila con **Compilar > Compilar solución** (`Ctrl+Shift+B`).
4. Ejecuta con `F5`.

Los datos se mantienen únicamente durante la sesión y se pierden al cerrar la aplicación.

## Probar

Desde una terminal ubicada en la raíz:

```bash
dotnet restore UniMatch.sln
dotnet build UniMatch.sln --configuration Release
dotnet test UniMatch.sln --configuration Release
```

Las pruebas automatizadas cubren compatibilidad de asignatura/horario, duplicados, cupos, cancelaciones y eliminación de relaciones. La revisión visual de Windows Forms debe realizarse en Windows.

## Documentación

- [Requerimientos](docs/requerimientos.md)
- [Especificaciones funcionales](docs/especificaciones.md)
- [Plan técnico](docs/plan-tecnico.md)
- [Implementación de Fase 4](docs/fase-4-implementacion.md)
