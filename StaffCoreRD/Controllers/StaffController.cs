using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaffCoreRD.Data;
using StaffCoreRD.Models;

namespace StaffCoreRD.Controllers
{
    [Authorize]
    public class StaffController : Controller
    {
        private readonly StaffDbContext _context;

        public StaffController(StaffDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Administrador,RRHH,Viewer")]
        public async Task<IActionResult> Index(string searchString)
        {
            var query = _context.Personal.Where(s => s.Activo);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Nombre.Contains(searchString) || s.Cargo.Contains(searchString));
            }

            var staffList = await query.OrderBy(s => s.Nombre).ToListAsync();
            ViewData["CurrentFilter"] = searchString;
            return View(staffList);
        }

        [Authorize(Roles = "Administrador,RRHH,Viewer")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var staff = await _context.Personal.FirstOrDefaultAsync(m => m.Id == id && m.Activo);
            if (staff == null) return NotFound();

            return View(staff);
        }

        // GET: Staff/Resumen
        [Authorize(Roles = "Administrador,RRHH,Viewer")]
        public async Task<IActionResult> Resumen()
        {
            var resumen = await _context.Personal
                .Where(s => s.Activo)
                .GroupBy(s => s.Departamento)
                .Select(g => new DepartamentoResumenViewModel
                {
                    Departamento = g.Key,
                    TotalEmpleados = g.Count(),
                    TotalNomina = g.Sum(s => s.Salario)
                })
                .OrderByDescending(x => x.TotalNomina)
                .ToListAsync();

            return View(resumen);
        }

        [Authorize(Roles = "Administrador,RRHH")]
        public IActionResult Create()
        {
            return View(new Staff());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,RRHH")]
        public async Task<IActionResult> Create(Staff staff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staff);
                await _context.SaveChangesAsync();
                TempData["Exito"] = "¡Empleado registrado con éxito!";
                return RedirectToAction(nameof(Index));
            }
            return View(staff);
        }

        [Authorize(Roles = "Administrador,RRHH")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var staff = await _context.Personal.FindAsync(id);
            if (staff == null || !staff.Activo) return NotFound();

            return View(staff);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,RRHH")]
        public async Task<IActionResult> Edit(int id, Staff staff)
        {
            if (id != staff.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staff);
                    await _context.SaveChangesAsync();
                    TempData["Exito"] = "¡Empleado actualizado correctamente!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Personal.Any(e => e.Id == staff.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(staff);
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var staff = await _context.Personal.FirstOrDefaultAsync(m => m.Id == id);
            if (staff == null) return NotFound();

            return View(staff);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staff = await _context.Personal.FindAsync(id);
            if (staff != null)
            {
                _context.Personal.Remove(staff);
                await _context.SaveChangesAsync();
                TempData["Exito"] = "¡Empleado eliminado correctamente del sistema!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}