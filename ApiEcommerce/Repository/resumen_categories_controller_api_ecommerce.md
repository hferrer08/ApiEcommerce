# Machete — CategoriesController (ApiEcommerce)

Guía en pasos breves para repetir lo hecho con `CategoriesController`.

---

## Pasos

1. **Instalación de paquetes**  
   - Instalé **Entity Framework Core**, el proveedor de base de datos (SQL Server o PostgreSQL), las herramientas de EF y **AutoMapper**.

2. **Modelo**  
   - Creé la clase `Category` en `Models` con propiedades `Id`, `Name`, `CreationDate` y sus data annotations.

3. **DbContext**  
   - Configuré `AppDbContext` y agregué el `DbSet<Category>`.
   - Lo registré en `Program.cs` con la cadena de conexión.

4. **DTOs**  
   - Creé DTOs para separar entrada/salida: `CategoryCreateDto`, `CategoryReadDto`, `CategoryUpdateDto`.

5. **AutoMapper**  
   - Configuré un `MappingProfile` para mapear entre `Category` y los DTOs.
   - Registré AutoMapper en `Program.cs`.

6. **Repository Pattern**  
   - Definí un contrato `IRepository<T>` (métodos CRUD básicos).
   - Implementé `CategoryRepository` que usa `AppDbContext`.
   - Registré el repositorio en `Program.cs` (`AddScoped`).

7. **Controller**  
   - Creé `CategoriesController` con endpoints CRUD (`GET`, `POST`, `PUT`, `DELETE`).
   - Usé el repositorio y AutoMapper dentro de los endpoints.
   - Respondí con `Ok`, `CreatedAtAction`, `NoContent`, `NotFound` según el caso.

8. **Migraciones**  
   - Ejecuté `dotnet ef migrations add InitialCreate`.
   - Apliqué con `dotnet ef database update`.

9. **Pruebas**  
   - Probé los endpoints en Swagger: listar, obtener por id, crear, actualizar y eliminar categorías.

---

## Estructura mental a repetir
1. Crear modelo.  
2. Crear DTOs.  
3. Configurar AutoMapper.  
4. Definir contrato e implementar repositorio.  
5. Registrar en `Program.cs`.  
6. Crear controlador CRUD.  
7. Hacer migración y actualizar DB.  
8. Probar en Swagger.

---

Así tienes un machete pequeño tipo checklist, como tu antiguo flujo en VB.NET, pero adaptado a Repository Pattern con buenas prácticas modernas.

