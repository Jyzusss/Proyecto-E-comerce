# Proyecto E-commerce API 🛒

Este es un proyecto de Backend desarrollado con **ASP.NET Core** que simula el funcionamiento de una tienda en línea. La API permite gestionar categorías, productos y procesar órdenes de compra con validación de inventario en tiempo real.

## 🚀 Tecnologías Utilizadas

* **Framework:** .NET 10 (C#)
* **ORM:** Entity Framework Core
* **Base de Datos:** SQL Server
* **Documentación:** Swagger / OpenAPI
* **Patrones:** Data Transfer Objects (DTO)

## 🛠️ Características Principales

* **Gestión de Catálogo (CRUD):** Control total sobre productos y categorías.
* **Sistema de Ordenes:** Procesamiento de compras que vincula múltiples productos en una sola transacción.
* **Lógica de Inventario:** El sistema valida la existencia de stock antes de confirmar cualquier venta y actualiza las existencias automáticamente.
* **Integridad de Datos:** Uso de `Include` y `ThenInclude` para consultas eficientes de datos relacionados.
* **Seguridad en Capas:** Implementación de DTOs para proteger la estructura interna de la base de datos.

## 📋 Estructura del Proyecto

* `/Controllers`: Manejo de las peticiones HTTP y lógica de los Endpoints.
* `/Models`: Entidades de la base de datos (mapeadas con EF Core).
* `/Dto`: Objetos de transferencia de datos para entrada y salida de la API.

## ⚙️ Configuración e Instalación

1. **Clonar el repositorio:**
   ```bash
   git clone [https://github.com/Jyzusss/Proyecto-E-comerce.git](https://github.com/Jyzusss/Proyecto-E-comerce.git)
