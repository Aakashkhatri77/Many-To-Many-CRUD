using Many_To_Many_CRUD.Context;
using Many_To_Many_CRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Many_To_Many_CRUD.Controllers
{
    public class EmployeeProjectController : Controller
    {
        private readonly ApplicationDbContext db;
        public EmployeeProjectController(ApplicationDbContext _db)
        {
            this.db = _db;
        }
        // GET: EmployeeProjectController
        public ActionResult Index()
        {
            var employeeProject = db.EmployeeProjects.Include(e => e.Employee).Include(e => e.Project).ToList();
            return View(employeeProject);
        }

        public ActionResult CreateProjectEmployee()
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            ViewBag.Employee = db.Employees;
            return View();
        }
        [HttpPost]
        public ActionResult CreateProjectEmployee(int ProjectId, int []EmployeeId)
        {
            foreach (int empId in EmployeeId)
            {
                EmployeeProject employeeProject = new EmployeeProject();
                employeeProject.ProjectId = ProjectId;
                employeeProject.EmployeeId = empId;
                db.EmployeeProjects.Add(employeeProject);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        // GET: EmployeeProjectController/Details/5
        public IActionResult Details(int id)
        {
            var employeeProjects = db.EmployeeProjects.Include(e => e.Employee).Include(e => e.Project).FirstOrDefault(m => m.Id == id);
            return View(employeeProjects);
        }

        // GET: EmployeeProjectController/Create
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            ViewBag.Employee = db.Employees;
            return View();
        }

        // POST: EmployeeProjectController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeProjectController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name");
            var employeeProjects = db.EmployeeProjects.Include(e => e.Employee).Include(e => e.Project).FirstOrDefault(m => m.Id == id);
            return View(employeeProjects);
        }

        // POST: EmployeeProjectController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EmployeeProject employeeProject)
        {
            try
            {
                    var _employeeProject = new EmployeeProject();
                    _employeeProject.ProjectId = employeeProject.ProjectId;
                    _employeeProject.EmployeeId = employeeProject.EmployeeId;
                    db.EmployeeProjects.Update(employeeProject);
                    db.SaveChanges();
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeProjectController/Delete/5
        public ActionResult Delete(int id)
        {
            var employeeProjects = db.EmployeeProjects.Include(e => e.Employee).Include(e => e.Project).FirstOrDefault(m => m.Id == id);

            return View(employeeProjects);
        }

        // POST: EmployeeProjectController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var employeeProject = db.EmployeeProjects.Include(e => e.Employee).Include(e => e.Project).FirstOrDefault(m => m.Id == id);
                db.EmployeeProjects.Remove(employeeProject);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
