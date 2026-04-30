# 🏦 Sistema de Transacciones Bancarias

Este proyecto es una solución técnica para la gestión de transacciones financieras (Abonos y Retiros), desarrollada bajo los estándares de **Clean Architecture** y **Domain-Driven Design (DDD)** utilizando **.NET 9**.

## 🚀 Inicio Rápido

### 1. Requisitos Previos
* [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
* Herramienta de línea de comandos (Bash, PowerShell o CMD).

### 2. Instalación y Ejecución
No es necesario configurar una base de datos externa. El sistema utiliza SQLite y se autogestiona al arrancar.

```bash
# Clonar el proyecto
git clone https://github.com/dban04/Transacciones-API.git
cd Transacciones-API

# Restaurar paquetes y ejecutar la aplicación
dotnet run --project Transacciones.API
```

### 3. Explorar la API
Una vez ejecutado, la API estará disponible en:
👉 Swagger UI: http://localhost:5051/swagger

Nota: Al iniciar por primera vez, el sistema poblará automáticamente la base de datos con cuentas de prueba (IDs: 1, 2 y 3).

## 🧪 Pruebas Unitarias e Integración
Se han implementado pruebas críticas para asegurar la integridad de los fondos:

Validación de Saldo: Verifica que no se permitan retiros que excedan el saldo disponible.

Integridad de Transacción: Asegura que el saldo se actualice correctamente tras un abono.

Pruebas de Controlador: Valida que los endpoints respondan con los códigos HTTP correctos.
Para ejecutar los tests:

```Bash
dotnet test
```

## 🏗️ Arquitectura de la Solución

La solución está dividida en cuatro capas para garantizar el desacoplamiento y la mantenibilidad:

* **Transacciones.Core:** El corazón del negocio. Contiene las entidades de dominio, interfaces de servicios y objetos de transferencia de datos (DTOs). La lógica de saldo y validaciones reside aquí.
* **Transacciones.Infrastructure:** Implementación de acceso a datos mediante **Entity Framework Core** y **SQLite**. Incluye el registro de transacciones y persistencia.
* **Transacciones.API:** Capa de presentación con Controladores REST, Middleware de manejo de excepciones y configuración de **Swagger/OpenAPI**.
* **Transacciones.Tests:** Suite de pruebas unitarias y de integración utilizando **xUnit**, **Moq** y base de datos en memoria.

### ✨ Características
* **Result Pattern:** Se evita el uso de excepciones para el flujo normal del negocio, devolviendo objetos de resultado que capturan el estado y posibles errores de forma semántica.

* **Middleware Global:** Captura de errores no controlados para evitar fugas de información técnica y estandarizar las respuestas de error.

* **Data Seeding:** Proceso automático de carga de datos iniciales para facilitar la revisión del evaluador.

* **Transaccionalidad:** Uso de **Optimistic Concurrency** con el comando BeginTransactionAsync para garantizar que la actualización del saldo y el registro del historial ocurran como una única operación atómica.

* **Eleccion SqlLite**: Para la capa de infrastrurture nos permitira hacer un el proceso de evaluacion mas rapido y el mismo puedo ser cambiado por otra base de datos facilmente.

* **.NET 9**: Para mayor compatibilidad de paguetes
