using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WinStudio.Track.Web.Areas.Tracking.Controllers
{
    public class MainController : Controller
    {
        //
        // GET: /Tracking/Main/

        public ActionResult Index()
        {
            return View();
        }
        

        //
        // GET: /Tracking/Main/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Tracking/Main/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Tracking/Main/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Tracking/Main/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Tracking/Main/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Tracking/Main/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Tracking/Main/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
