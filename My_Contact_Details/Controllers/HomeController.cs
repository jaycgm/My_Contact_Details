using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My_Contact_Details.Models;

namespace My_Contact_Details.Controllers
{
    public class HomeController : Controller
    {
        private BASEIEntities _db = new BASEIEntities(); 

        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View(_db.Tmp_Contact_Details.ToList());
        }

        //
        // GET: /Home/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Home/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Home/Create

        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Exclude="Id")]Tmp_Contact_Details contactDetail)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                // TODO: Add insert logic here
                _db.AddToTmp_Contact_Details(contactDetail);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Home/Edit/5
 
        public ActionResult Edit(int id)
        {
            var res = (from r in _db.Tmp_Contact_Details where r.Id == id select r).FirstOrDefault();
            return View();
        }

        //
        // POST: /Home/Edit/5

        [HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        public ActionResult Edit(Tmp_Contact_Details contactDetail)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                // TODO: Add update logic here
                var res = (from r in _db.Tmp_Contact_Details where r.Id == contactDetail.Id select r).FirstOrDefault();
                _db.ApplyCurrentValues(res.EntityKey.EntitySetName, contactDetail);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Home/Delete/5
 
        public ActionResult Delete(int id)
        {
            var res = (from r in _db.Tmp_Contact_Details where r.Id == id select r).FirstOrDefault();
            return View(res);
        }

        //
        // POST: /Home/Delete/5

        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(Tmp_Contact_Details contactDetail)
        {
            try
            {
                // TODO: Add delete logic here
                var res = (from r in _db.Tmp_Contact_Details where r.Id == contactDetail.Id select r).FirstOrDefault();
                _db.DeleteObject(res);
                _db.SaveChanges();                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
