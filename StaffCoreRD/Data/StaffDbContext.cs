using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StaffCoreRD.Models;

namespace StaffCoreRD.Data
{
    public class StaffDbContext : IdentityDbContext<IdentityUser>
    {
        public StaffDbContext(DbContextOptions<StaffDbContext> options) : base(options) { }

        public DbSet<Staff> Personal { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb); // Obligatorio para Identity

            mb.Entity<Staff>().HasData(
                new Staff
                {
                    Id = 1,
                    Nombre = "Juan Carlos Rosario",
                    Cedula = "001-1234567-8",
                    Cargo = "Desarrollador Senior",
                    Departamento = "Tecnología",
                    Salario = 85000.00m,
                    FechaIngreso = new DateTime(2023, 01, 15),
                    Activo = true
                },
                new Staff
                {
                    Id = 2,
                    Nombre = "Maria Altagracia Garcia",
                    Cedula = "002-8765432-1",
                    Cargo = "Analista de Reclutamiento",
                    Departamento = "Recursos Humanos",
                    Salario = 45000.00m,
                    FechaIngreso = new DateTime(2023, 05, 20),
                    Activo = true
                }
            );
        }
    }
}