# Constitución de UniMatch

## Principios

1. La implementación debe seguir los requerimientos y especificaciones funcionales aprobados.
2. La solución se organiza en `Entidades`, `Controlador` y `Formularios`.
3. El almacenamiento es en memoria durante una sesión; no se agrega persistencia fuera del alcance.
4. Las reglas de compatibilidad, duplicados, cupos e integridad referencial se validan en los controladores.
5. Los formularios deben conservar archivos de diseñador editables en Visual Studio.
6. Las pruebas automatizadas cubren reglas de negocio; la presentación visual se verifica posteriormente en Windows.
