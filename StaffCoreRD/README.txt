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

(Este fue el primer usuario registrado en el sistema, por lo que
el sistema le asigno automaticamente el rol de "Administrador".)

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
1. Abrir la solucion en Visual Studio.
2. Verificar el connection string "StaffCore" en appsettings.json
   (apunta a (localdb)\mssqllocaldb, base de datos StaffCoreDB).
3. Ejecutar en la Consola del Administrador de Paquetes:
       dotnet ef database update
4. Correr el proyecto con dotnet run o F5 en Visual Studio.
5. Al iniciar, el sistema crea automaticamente los roles:
   Administrador, RRHH y Viewer.
6. Registrar el primer usuario: quedara asignado automaticamente
   como Administrador. Los siguientes usuarios se registran con
   rol Viewer por defecto (el Administrador puede cambiarles el rol
   desde "Gestionar Roles").

========================================================
