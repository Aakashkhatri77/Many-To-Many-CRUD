using Many_To_Many_CRUD.Context;
using Many_To_Many_CRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Many_To_Many_CRUD.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext db;
        public ProjectController(ApplicationDbContext _db)
        {
            this.db = _db;
        }
        // GET: ProjectController
        public ActionResult Index()
        {
            var projects = db.Projects.ToList();
            return View(projects);
        }

        // GET: ProjectController/Details/5
        public ActionResult Details(int id)
        {
            var projects = db.Projects.Find(id);
            return View(projects);
        }

        // GET: ProjectController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProjectController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Project project)
        {
            try
            {
                var _project = new Project()
                {
                    Name = project.Name,
                    ProjectDetails = project.ProjectDetails,
                };
                db.Projects.Add(_project);
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProjectController/Edit/5
        public ActionResult Edit(int id)
        {
            var projects = db.Projects.Find(id);
            return View(projects);
        }

        // POST: ProjectController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Project project)
        {
            try
            {
                var _project = db.Projects.Find(id);
                _project.Name = project.Name;
                _project.ProjectDetails = project.ProjectDetails;
                db.Projects.Update(_project);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProjectController/Delete/5
        public ActionResult Delete(int id)
        {
            var projects = db.Projects.Find(id);
            return View(projects);
        }

        // POST: ProjectController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var project = db.Projects.Find(id);
                db.Projects.Remove(project);
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
