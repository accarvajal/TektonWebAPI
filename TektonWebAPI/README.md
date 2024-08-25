# Product API

## Descripción

Este proyecto es una API RESTful para la gestión de productos. Implementa principios SOLID, Clean Code, y patrones de diseño como Repository y CQRS. La API está documentada con Swagger y utiliza TDD para su desarrollo.

## Estructura del Proyecto

- **Endpoints**: Definición de los endpoints de la API.
- **Models**: Modelos de datos.
- **Services**: Lógica de negocio.
- **Repositories**: Acceso a datos.
- **Validators**: Validaciones de request.
- **Middleware**: Middleware para logging.
- **DTOs**: Data Transfer Objects.
- **Mappings**: Configuración de AutoMapper.
- **Cache**: Implementación de caché.
- **Logs**: Archivos de logs.
- **Data**: Contexto de la base de datos.
- **Extensions**: Métodos de extensión para la configuración de servicios.
- **Tests**: Proyecto de pruebas unitarias.

## Requisitos

- .NET Core (última versión)
- AutoMapper
- Swagger
- MemoryCache
- Entity Framework Core
- SQLite
- xUnit
- Moq

## Configuración

1. Clonar el repositorio.
2. Navegar a la carpeta del proyecto.
3. Ejecutar `dotnet restore` para restaurar las dependencias.
4. Ejecutar `dotnet ef database update` para crear la base de datos SQLite.
5. Ejecutar `dotnet run` para iniciar la API.

## Endpoints

- `GET /api/product/{id}`: Obtener producto por ID.
- `POST /api/product`: Crear un nuevo producto.
- `PUT /api/product/{id}`: Actualizar un producto existente.

## Patrones y Principios

- **Repository Pattern**: Para el acceso a datos.
- **CQRS**: Separación de comandos y consultas.
- **SOLID**: Principios de diseño de software.
- **TDD**: Desarrollo guiado por pruebas.

## Ejecución de Pruebas

Ejecutar `dotnet test` para correr las pruebas unitarias.