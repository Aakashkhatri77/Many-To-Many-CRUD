using Many_To_Many_CRUD.Context;
using Many_To_Many_CRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
/*using Microsoft.AspNetCore.Mvc;
*/
/*using System.Web.Mvc;
*/
namespace Many_To_Many_CRUD.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext db;

        public EmployeeController(ApplicationDbContext _db)
        {
            this.db = _db;
        }
        // GET: EmployeeController
        public  ActionResult Index()
        {
            var data = db.Employees.ToList();
            return View(data);
        }

        // GET: EmployeeController/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            var employee = await db.Employees.FindAsync(id);
            return View(employee);
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            try
            {
                var _employee = new Employee()
                {
                    Name = employee.Name,
                    Email = employee.Email,
                    MobileNo = employee.MobileNo,
                    Salary = employee.Salary,
                };
                db.Employees.Add(_employee);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            var employee = db.Employees.Find(id);
            return View(employee);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Employee employee)
        {
            try
            {
                var _employee = db.Employees.Find(id);
                _employee.Name = employee.Name;
                _employee.Email = employee.Email;
                _employee.MobileNo = employee.MobileNo;
                _employee.Salary = employee.Salary;
                db.Employees.Update(_employee);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            var employee = db.Employees.Find(id);
            return View(employee);
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var employee = db.Employees.Find(id);
                db.Employees.Remove(employee);
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
