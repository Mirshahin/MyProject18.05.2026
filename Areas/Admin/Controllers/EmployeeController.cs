namespace MyProject18._05._2026.Areas.Admin.Controllers
{
    using global::MyProject18._05._2026.Areas.Admin.ViewModels;
    using global::MyProject18._05._2026.DAL;
    using global::MyProject18._05._2026.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    namespace MyProject18._05._2026.Areas.Admin.Controllers
    {
        [Area("Admin")]
        public class EmployeeController : Controller
        {
            private readonly AppDbContext _context;

            public EmployeeController(AppDbContext context)
            {
                _context = context;
            }

          
            public async Task<IActionResult> Index()
            { 
                var employees = await _context.Employees    
                    .Include(e => e.Range)
                    .Where(e => !e.isDeleted)
                    .ToListAsync();

                return View(employees);
            }

          
            public async Task<IActionResult> Create()
            {
              
                ViewBag.Ranges = new SelectList(await _context.Ranges.Where(r => !r.isDeleted).ToListAsync(), "Id", "name");
                return View();
            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(EmployeeCreate model)
            {
                if (!ModelState.IsValid)
                {

                    ViewBag.Ranges = new SelectList(await _context.Ranges.Where(r => !r.isDeleted).ToListAsync(), "Id", "name");
                    return View(model);
                }

                var range = await _context.Ranges.FindAsync(model.RangeId);
                if (range == null)
                {
                    ModelState.AddModelError("RangeId", "Selected Range is invalid.");
                    ViewBag.Ranges = new SelectList(await _context.Ranges.Where(r => !r.isDeleted).ToListAsync(), "Id", "name");
                    return View(model);
                }

                Employee employee = new Employee
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Age = model.Age,
                    Position = model.Position,
                    Range = range
                };

                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            public async Task<IActionResult> Edit(int id)
            {
                var employee = await _context.Employees
                    .Include(e => e.Range)
                    .FirstOrDefaultAsync(e => e.Id == id && !e.isDeleted);

                if (employee == null) return NotFound();

                var updateModel = new EmployeeUpdate
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Surname = employee.Surname,
                    Age = employee.Age,
                    Position = employee.Position,
                    RangeId = employee.Range?.Id ?? 0
                };

                ViewBag.Ranges = new SelectList(await _context.Ranges.Where(r => !r.isDeleted).ToListAsync(), "Id", "name", updateModel.RangeId);
                return View(updateModel);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, EmployeeUpdate model)
            {
                if (id != model.Id) return BadRequest();

                if (!ModelState.IsValid)
                {
                    ViewBag.Ranges = new SelectList(await _context.Ranges.Where(r => !r.isDeleted).ToListAsync(), "Id", "name", model.RangeId);
                    return View(model);
                }

                var employee = await _context.Employees.FindAsync(id);
                if (employee == null) return NotFound();

                var range = await _context.Ranges.FindAsync(model.RangeId);
                if (range == null)
                {
                    ModelState.AddModelError("RangeId", "Selected Range is invalid.");
                    ViewBag.Ranges = new SelectList(await _context.Ranges.Where(r => !r.isDeleted).ToListAsync(), "Id", "name", model.RangeId);
                    return View(model);
                }

                employee.Name = model.Name;
                employee.Surname = model.Surname;
                employee.Age = model.Age;
                employee.Position = model.Position;
                employee.Range = range;

                _context.Employees.Update(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Delete(int id)
            {
                var employee = await _context.Employees.FindAsync(id);
                if (employee == null) return NotFound();

                employee.isDeleted = true;

                _context.Employees.Update(employee);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
        }
    }
}
