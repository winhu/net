using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinStudio.Framework.Web.Services;
using WinStudio.Track.IBusiService;
using WinStudio.Track.Models;

namespace WinStudio.Track.Web.Areas.Tracker.Controllers
{
    public class TimePointController : AbstractTrackController
    {
        //
        // GET: /Tracker/TimePoint/

        public ActionResult Index(string key, int pageIndex = 1, int pageSize = 10)
        {
            var ibsTimePoint = GetService<ITimePointBusiService>();
            var ret = ibsTimePoint.GetTimePoints(key, pageIndex, pageSize);
            return View(ret.Instance(0));
        }


        //
        // GET: /Tracker/TimePoint/Edit/5

        public ActionResult Edit(string id, string lid)
        {
            ViewBag.LineID = lid;
            var ibsTimePoint = GetService<ITimePointBusiService>();
            var point = ibsTimePoint.GetTimePoint(lid, id).Instance();
            return View(point);
        }

        //
        // POST: /Tracker/TimePoint/Edit/5

        [HttpPost]
        public ActionResult Edit(TimePoint point)
        {
            try
            {
                var ibsTimePoint = GetService<ITimePointBusiService>();
                var ret = ibsTimePoint.SaveTimePoint(point, MyAccount);
                ModelState.AddModelError("", ret.LastMsg);
                return View();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        //
        // GET: /Tracker/TimePoint/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Tracker/TimePoint/Delete/5

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
