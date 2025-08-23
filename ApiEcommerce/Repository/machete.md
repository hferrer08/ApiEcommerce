# 📝 Machete para nuevos Controllers (ApiEcommerce)

Guía rápida para crear endpoints con buenas prácticas (Repository Pattern + DTOs + AutoMapper + EF Core).

---

## Pasos a seguir

1. **Instalar paquetes (si no están)**
   - EF Core + Provider de BD (SQL Server o PostgreSQL).
   - Herramientas EF (`dotnet ef`).
   - AutoMapper (`AutoMapper.Extensions.Microsoft.DependencyInjection`).

2. **Modelo**
   - Crear clase en `Models/` con propiedades y data annotations.

3. **DbContext**
   - Agregar `DbSet<Entity>` en `AppDbContext`.
   - Confirmar registro de `DbContext` en `Program.cs` con la cadena de conexión.

4. **DTOs**
   - Crear DTOs en `DTOs/` para Create, Read y Update.

5. **AutoMapper**
   - Configurar `MappingProfile` con `CreateMap` entre modelo y DTOs.
   - Registrar AutoMapper en `Program.cs`.

6. **Repository Pattern**
   - Crear contrato en `Repositories/IRepository<T>` o `I<Entity>Repository`.
   - Implementar repositorio (`EntityRepository`) con métodos CRUD.
   - Registrar repositorio en `Program.cs` con `AddScoped`.

7. **Controller**
   - Crear `EntitiesController` con `[ApiController]` y `[Route]`.
   - Implementar endpoints CRUD (`GET`, `POST`, `PUT`, `DELETE`).
   - Usar repo + AutoMapper.
   - Responder con `Ok`, `CreatedAtAction`, `NoContent`, `NotFound`.

8. **Migraciones**
   - `dotnet ef migrations add <NombreMigracion>`
   - `dotnet ef database update`

9. **Pruebas**
   - Probar endpoints en Swagger:
     - `GET /api/entities`
     - `GET /api/entities/{id}`
     - `POST /api/entities`
     - `PUT /api/entities/{id}`
     - `DELETE /api/entities/{id}`

---

## Flujo mental (checklist rápido)

Modelo → DTOs → AutoMapper → Repository → Registrar en `Program.cs` → Controller → Migración → Probar en Swagger

---

## Notas
- Usar `AsNoTracking()` en lecturas.
- Usar `UtcNow` para fechas de creación.
- Separar bien carpetas: `Models`, `DTOs`, `Repositories`, `Controllers`, `Mapping`, `Data`.
