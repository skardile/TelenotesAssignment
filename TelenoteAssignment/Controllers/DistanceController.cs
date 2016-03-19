using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelenoteAssignment.Models;

namespace TelenoteAssignment.Controllers
{
    public class DistanceController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }


        // Initialize AngularJS model with ViewCoordinate
        public JsonResult getCoordinate()
        {
            ViewCoordinate vd = new ViewCoordinate();
            List<Coordinate> cList = new List<Coordinate>();
            vd.cList = cList;
            return Json(vd, JsonRequestBehavior.AllowGet);
        }

        // add location

        [HttpPost]
        public ActionResult Add(Coordinate model)
        {
            var coordinate = new Coordinate();
            coordinate.x = model.x;
            coordinate.y = model.y;

            return Json(coordinate, JsonRequestBehavior.AllowGet);

        }

        // Calculate distance of each location from the starting point and sort the list

        [HttpPost]
        public ActionResult Calculate(List<Coordinate> list)
        {
            
            float startX = list.ElementAt(0).x;
            float startY = list.ElementAt(0).y;

            for (int i = 1; i < list.Count; i++)
            {
                float temp = list.ElementAt(i).x - startX;
                temp = temp * temp;

                float temp1 = list.ElementAt(i).y - startY;
                temp1 = temp1 * temp1;

                list.ElementAt(i).distance = Math.Sqrt(temp + temp1);

            
            }
            List<Coordinate> SortedList = list.OrderBy(o => o.distance).ToList();
            return Json(SortedList, JsonRequestBehavior.AllowGet);

        }

        // Delete location from given location list

        [HttpPost]
        public ActionResult Delete(List<Coordinate> list)
        {
            Coordinate c = list.ElementAt(list.Count - 1);
            list.RemoveAt(list.Count - 1);

            for (int i = 0; i < list.Count; i++)
            {
                if (list.ElementAt(i).x == c.x && list.ElementAt(i).y == c.y)
                {
                    list.RemoveAt(i);
                    break;
                }
            }


            return Json(list, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Technologies()
        {
            return View();
        }

        public ActionResult Documentation()
        {
            return View();

        }
        public FileResult Resume()
        {
            return File("~/Content/SaurabhKardileResume.pdf", "application/pdf");
        }

    }
}