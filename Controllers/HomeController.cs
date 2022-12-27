using Portfolio_Main.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portfolio_Main.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult Contact()
        {
            return View();
        }


        PortfolioEntities db = new PortfolioEntities();

        public ActionResult Tasks()
        {
            var model = db.ToDoLists.ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(ToDoList newTask)
        {
            if (newTask.ID == 0) db.ToDoLists.Add(newTask);

            else
            {
                var updateTask = db.ToDoLists.Find(newTask.ID);

                if (updateTask == null) return HttpNotFound();

                updateTask.Task = newTask.Task;
            }
            db.SaveChanges();
            return RedirectToAction("Tasks", "Home");
        }

        public ActionResult Update(int id)
        {
            var model = db.ToDoLists.Find(id);

            if (model == null) return HttpNotFound();

            return View("New", model);
        }

        public ActionResult Delete(int id)
        {
            var deleteName = db.ToDoLists.Find(id);

            if (deleteName == null) return HttpNotFound();

            db.ToDoLists.Remove(deleteName);
            db.SaveChanges();
            return RedirectToAction("Tasks", "Home");
        }
    }
}