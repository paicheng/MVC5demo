using MVC5Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Demo.Controllers
{
    public class ReportsController : Controller
    {
        ContosoUniversityEntities db = new ContosoUniversityEntities();
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CoursesReport1()
        {
            var data = from p in db.Course
                       select new CoursesReport1VM
                       {
                           CourseID = p.CourseID,
                           CourseName = p.Title,

                       };
            return View();
        }
    }
}