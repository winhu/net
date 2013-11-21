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
    public class TimeLineController : AbstractTrackController
    {
        //
        // GET: /Tracker/TimeLine/

        public ActionResult Index(string key, int pageIndex = 1, int pageSize = 10)
        {
            var ibsTimeLine = GetService<ITimeLineBusiService>();
            var ret = ibsTimeLine.GetTimeLines(key, pageIndex, pageSize);
            return View(ret.Instance(0));
        }
        //
        // GET: /Tracker/TimeLine/Edit/5

        public ActionResult Edit(string id)
        {
            var ibsTimeLine = GetService<ITimeLineBusiService>();
            var line = ibsTimeLine.GetTimeLine(id).Instance<TimeLine>() ?? new TimeLine();
            //if (line != null)
            //{
            //    var ret = line.Points;
            //}
            return View(line);
        }

        //
        // POST: /Tracker/TimeLine/Edit/5

        [HttpPost]
        public ActionResult Edit(TimeLine line)
        {
            try
            {
                var ibsTimeLine = GetService<ITimeLineBusiService>();
                var ret = ibsTimeLine.SaveTimeLine(line, MyAccount);
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
        // GET: /Tracker/TimeLine/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Tracker/TimeLine/Delete/5

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
