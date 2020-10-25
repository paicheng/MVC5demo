using MVC5Demo.Models;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Razor;

namespace MVC5Demo.Controllers
{
    public class DepartmentController : Controller
    {
        private ContosoUniversityEntities db = new ContosoUniversityEntities();
        // GET: Department
        public ActionResult Index()
        {
            var dept = db.Department;
            return View(dept);
        }
        public ActionResult Create()
        {
            //ViewBag.InstructorID = new SelectList(db.Person, "ID", "FirstName");
            ViewBag.InstructorID = new SelectList(db.Person.OrderBy(p => p.FirstName), "ID", "FirstName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Department dept)
        {
            if (ModelState.IsValid)
            {
                db.Department.Add(dept);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            //ViewBag.InstructorID = new SelectList(db.Person, "ID", "FirstName");
            ViewBag.InstructorID = new SelectList(db.Person.OrderBy(p => p.FirstName), "ID", "FirstName");
            return View(dept);
        }
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            Department department = db.Department.Find(id);

            ViewBag.InstructorID = new SelectList(db.Person.OrderBy(p=>p.FirstName), "ID", "FirstName", department.InstructorID);
            return View(department);
        }
        [HttpPost]
        public ActionResult Edit(int id, DepartmentEdit department)
        {
            if (ModelState.IsValid)
            {
                var item = db.Department.Find(id);

                item.InjectFrom(department);
                //item.Name = department.Name;
                //item.Budget = department.Budget;
                //item.StartDate = department.StartDate;
                //item.InstructorID = department.InstructorID;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            //ViewBag.InstructorID = new SelectList(db.Person, "ID", "FirstName");

            var dept = db.Department.Find(id);

            ViewBag.InstructorID = new SelectList(db.Person.OrderBy(p=>p.FirstName), "ID", "FirstName", dept.InstructorID);

            return View(dept);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var dept = db.Department.Find(id);
            if (dept == null)
            {
                return HttpNotFound();
            }
            return View(dept);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dept = db.Department.Find(id);

            if (dept == null)
            {
                return HttpNotFound();
            }
            return View(dept);
        }

        [HttpPost]
        public ActionResult Delete(int id, Department department)
        {
            var dept = db.Department.Find(id);
            db.Department.Remove(dept);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}