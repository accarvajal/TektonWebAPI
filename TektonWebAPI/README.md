# Product API

## Descripción

Este proyecto es una API RESTful para la gestión de productos. Implementa la arquitectura Clean Architecture, principios SOLID, Clean Code, y patrones de diseño como Repository, CQRS, Result. La API está documentada con Swagger.

Los proyectos que hacen parte de la solución estan desarrollados en .NET 8 C#.

## Estructura del Proyecto

### `TektonWebAPI`

Este es el proyecto principal de la API. Contiene los controladores, modelos y servicios necesarios para ejecutar la aplicación.

- **Products.db**: Base de Datos SQLite.
- **Controllers**: Contiene los controladores de la API.
- **Models**: Contiene los modelos de datos utilizados por la API.
- **Services**: Contiene los servicios de la aplicación.
- **Program.cs**: Punto de entrada de la aplicación.
- **Startup.cs**: Configuración de la aplicación.
- **appsettings.json**: Configuración de la aplicación.

### `TektonWebAPI.Application`

Este proyecto contiene la lógica de aplicación, incluyendo comandos, consultas, interfaces y DTOs.

- **Commands**: Contiene los comandos de la aplicación.
- **Queries**: Contiene las consultas de la aplicación.
- **Interfaces**: Contiene las interfaces de la aplicación.
- **DTOs**: Contiene los objetos de transferencia de datos (DTOs).

### `TektonWebAPI.Core`

Este proyecto contiene las entidades, interfaces y objetos de valor.

- **Entities**: Contiene las entidades.
- **Interfaces**: Contiene las interfaces.
- **ValueObjects**: Contiene los objetos de valor.

### `TektonWebAPI.Infrastructure`

Este proyecto contiene la implementación de la infraestructura, incluyendo acceso a datos y servicios externos.

- **Data**: Contiene la configuración de acceso a datos.
- **Repositories**: Contiene las implementaciones de los repositorios.
- **Services**: Contiene las implementaciones de los servicios de infraestructura.

### `TektonWebAPI.Tests`

Este proyecto contiene las pruebas unitarias e integrales para el proyecto `TektonWebAPI`.

- **Data**: Contiene las pruebas unitarias para el repositorio.
- **Features**: Contiene las pruebas integrales de CQRS.
- **Services**: Contiene las pruebas integrales de los servicios.

## Ejecución del Proyecto con Linea de Comandos

1. Clonar el repositorio.
2. Navegar a la carpeta del proyecto.
3. Ejecutar `dotnet restore` para restaurar las dependencias.
4. Ejecutar `dotnet ef database update` para crear la base de datos SQLite.
5. Ejecutar `dotnet run` para iniciar la API.


## Ejecución del Proyecto con Docker

Para ejecutar el proyecto utilizando Docker, sigue estos pasos:

1. Navega al directorio del proyecto:

    ```sh
    cd path/to/TektonWebAPI
    ```

2. Construye la imagen Docker:

    ```sh
    docker build -t tektonwebapi .
    ```

3. Ejecuta la imagen Docker:

    ```sh
    docker run -d -p 8080:80 --name tektonwebapi-container tektonwebapi
    ```

4. Verifica que el contenedor esté corriendo:

    ```sh
    docker ps
    ```

5. Accede a la aplicación en `http://localhost:8080`.

6. Accede a Swagger en `http://localhost:8080/swagger`.

## Notas Adicionales

- **Dockerfile**: Asegúrate de que tu `Dockerfile` esté correctamente configurado para construir y ejecutar tu aplicación .NET.
- **Puertos**: Ajusta los puertos según sea necesario para tu aplicación.
- **Variables de Entorno**: Si tu aplicación requiere variables de entorno, puedes pasarlas al contenedor usando la opción `-e` en el comando `docker run`.

    ```sh
    docker run -d -p 8080:80 --name tektonwebapi-container -e "ASPNETCORE_ENVIRONMENT=Development" tektonwebapi
    ```

Con estos pasos, deberías poder construir y ejecutar tu proyecto .NET con Docker desde la línea de comandos.


## Ejecución de Pruebas

Ejecutar `dotnet test` para correr las pruebas unitarias.


## Requisitos

- .NET Core (última versión)
- AutoMapper
- FluentValidation
- Swagger
- MemoryCache
- Entity Framework Core
- SQLite
- xUnit
- Moq


## Endpoints

- `/swagger/index.html`: Documentación de Endpoints en Swagger.
- `POST /api/auth/login`: Iniciar sesión. Para poder acceder a los demás endpoints, se debe iniciar .
- `GET /api/product/{id}`: Obtener producto por ID.
- `POST /api/product`: Crear un nuevo producto.
- `PUT /api/product/{id}`: Actualizar un producto existente.

## Servicios Externos

### `DiscountService`

El `DiscountService` se conecta a un servicio externo para obtener el porcentaje de descuento de un producto. La URL del servicio es:

`https://66cc98efa4dd3c8a71b82d4d.mockapi.io/api/v1/discounts/{productId}`

Actualmente, solo hay dos productos creados con los siguientes IDs:

- **Producto 1**: ID = 1
- **Producto 2**: ID = 2

Para obtener el descuento de un producto, el `DiscountService` realiza una solicitud GET a la URL anterior, reemplazando `{productId}` con el ID del producto deseado.

## Patrones y Principios

- **Repository Pattern**: Para el acceso a datos.
- **CQRS**: Separación de comandos y consultas.
- **SOLID**: Principios de diseño de software.
- **RESULT**: Patrón para gestión de errores y resultados.
- **TDD**: Desarrollo guiado por pruebas.
