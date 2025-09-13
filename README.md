# 🛒 ApiEcommerce (.NET 8)

Proyecto didáctico desarrollado siguiendo el curso **Devtalles**.  
El objetivo no es solo crear una API funcional, sino también **aprender paso a paso buenas prácticas modernas en ASP.NET Core** y dejar esta API como **plantilla base** para futuros proyectos.

---

## 🚀 Tecnologías utilizadas
- .NET Web API  
- Entity Framework Core (SQL Server)  
- AutoMapper
- Mapster
- Swagger / Swashbuckle  
- BCrypt.Net (hash de contraseñas)  
- Microsoft.AspNetCore.Authentication.JwtBearer  
- Microsoft.AspNetCore.Identity.EntityFrameworkCore  
- Caché  
- Postman (colecciones organizadas en workspace)  
- Azure App Service (deploy final)

---

## 📚 Línea de aprendizaje

1. **Estructura inicial**  
   - Controllers, DTOs, Repository Pattern, AutoMapper.

2. **Entity Framework Core**  
   - DbContext, migraciones, LINQ, seeding de datos.

3. **Swagger**  
   - Documentación automática con `[ProducesResponseType]`.

4. **Autenticación con JWT (manual)**  
   - Login, Claims, generación de tokens con `JwtSecurityTokenHandler`.

5. **CORS**  
   - Configuración de políticas centralizadas en `Constants/PolicyNames`.

6. **Autorización con `[Authorize]` / `[AllowAnonymous]`**  
   - Endpoints públicos y privados.  
   - Restricción de **Products** y **Category** solo a rol *admin*.

7. **Postman organizado**  
   - Workspace + carpetas por dominio (Auth, Products, Categories).  
   - Variables de entorno (`baseUrl`, `token`) y captura automática de JWT.


8. **Versionamiento de API**  
   - Rutas `v1`, `v2`.  
   - Swagger múltiple con versiones visibles.  
   - Endpoints neutrales y ejemplo con `[Obsolete]`.

9. **Evolución de la autenticación**  
    - Paso 1: JWT manual.  
    - Paso 2: Migración a **.NET Identity** con `ApplicationUser` y `IdentityRole`.  
    - Roles y claims integrados a los endpoints.

10. **Subida de imágenes**  
    - Implementada en creación y modificación de productos (`multipart/form-data`).  

11. **Seed, paginación y uso de agente**  
    - Datos iniciales (seeding).  
    - Paginación con `Skip/Take` y metadatos en headers.  
    - Agente HTTP (`HttpClient` tipado).


---

## 🔑 Autenticación y Roles

Ejemplo de respuesta de login:

```json
{
  "user": {
    "id": "id",
    "username": "username",
    "name": "Administrador"
  },
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6ImFkbWluLTAwMSIsInVzZXJuYW1lIjoiYWRtaW5AYWRtaW4uY29tIiwicm9sZSI6IkFkbWluIiwibmJmIjoxNzU3Nzc0OTc0LCJleHAiOjE3NTc3ODIxNzQsImlhdCI6MTc1Nzc3NDk3NH0.TmXS1fetnuYk936QK369hUDfDmcCtfuPH8lneCy3D8c",
  "message": "Usuario logueado correctamente."
}
```

---


## 👨‍💻 Autor
Desarrollado por **Hubert Ferrer** como parte del curso *.NET Backend: .NET Core, SQL Server y seguridad JWT* de **Devtalles**, con el objetivo de **aprender, practicar y dejar una plantilla reutilizable para futuros proyectos**.
