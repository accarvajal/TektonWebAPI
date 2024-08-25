# Product API

## Descripci�n

Este proyecto es una API RESTful para la gesti�n de productos. Implementa principios SOLID, Clean Code, y patrones de dise�o como Repository y CQRS. La API est� documentada con Swagger y utiliza TDD para su desarrollo.

## Estructura del Proyecto

- **Endpoints**: Definici�n de los endpoints de la API.
- **Models**: Modelos de datos.
- **Services**: L�gica de negocio.
- **Repositories**: Acceso a datos.
- **Validators**: Validaciones de request.
- **Middleware**: Middleware para logging.
- **DTOs**: Data Transfer Objects.
- **Mappings**: Configuraci�n de AutoMapper.
- **Cache**: Implementaci�n de cach�.
- **Logs**: Archivos de logs.
- **Data**: Contexto de la base de datos.
- **Extensions**: M�todos de extensi�n para la configuraci�n de servicios.
- **Tests**: Proyecto de pruebas unitarias.

## Requisitos

- .NET Core (�ltima versi�n)
- AutoMapper
- Swagger
- MemoryCache
- Entity Framework Core
- SQLite
- xUnit
- Moq

## Configuraci�n

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
- **CQRS**: Separaci�n de comandos y consultas.
- **SOLID**: Principios de dise�o de software.
- **TDD**: Desarrollo guiado por pruebas.

## Ejecuci�n de Pruebas

Ejecutar `dotnet test` para correr las pruebas unitarias.