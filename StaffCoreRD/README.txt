========================================================
 STAFFCORE RD - Sistema de Gestion de Personal
 ISW-311 Tecnologias de Internet I
 Universidad Central del Este (UCE)
========================================================

DATOS DEL ESTUDIANTE
---------------------
Nombre completo : Ezequiel Peguero Bautista
Matricula        : 2023-0738


CREDENCIALES DEL USUARIO ADMINISTRADOR DE PRUEBA
--------------------------------------------------
Correo electronico : admin@staffcore.rd
Contrasena          : Admin123

(Este usuario se crea automaticamente con rol de Administrador
la primera vez que se ejecuta la aplicacion, mediante el metodo
DbInitializer.SeedAdminUserAsync. No es necesario registrarlo
manualmente.)

DESCRIPCION DEL PROYECTO
--------------------------
StaffCore RD es un sistema de gestion de personal desarrollado con
ASP.NET Core MVC, Entity Framework Core (Code First) y ASP.NET
Identity. Permite administrar el personal de una empresa dividida
en cuatro departamentos: Tecnologia, Recursos Humanos, Finanzas y
Operaciones.

El sistema cuenta con:
- Login y registro de usuarios con roles (Administrador, RRHH, Viewer)
- Proteccion de rutas segun el rol del usuario autenticado
- CRUD completo de empleados (Crear, Leer, Actualizar, Eliminar)
- Busqueda en tiempo real de empleados por nombre o cargo
- Pagina de detalle (perfil completo) de cada empleado
- Resumen estadistico de personal y nomina agrupado por departamento
- Gestion de roles de usuario desde el panel de Administrador

TECNOLOGIAS UTILIZADAS
------------------------
- ASP.NET Core MVC (.NET 8)
- Entity Framework Core (Code First) + SQL Server (LocalDB)
- ASP.NET Identity (autenticacion y roles)
- Bootstrap 5 + Font Awesome

COMO EJECUTAR EL PROYECTO
----------------------------
1. Clonar el repositorio y abrir la solucion en Visual Studio.
2. Verificar el connection string "StaffCore" en appsettings.json
   (apunta a (localdb)\mssqllocaldb, base de datos StaffCoreDB).
3. Ejecutar en la Consola del Administrador de Paquetes (Package
   Manager Console):
       Update-Database
4. Correr el proyecto con F5 en Visual Studio (o dotnet run).
5. Al iniciar por primera vez, el sistema crea automaticamente:
   - Los roles: Administrador, RRHH y Viewer.
   - El usuario Administrador de prueba (ver credenciales arriba).
6. Puede iniciar sesion directamente con esas credenciales, o
   registrar nuevos usuarios desde la pantalla de Registro (se
   les asignara el rol Viewer por defecto; el Administrador puede
   cambiarles el rol desde "Gestionar Roles").

========================================================
