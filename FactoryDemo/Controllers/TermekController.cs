using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using FactoryDemo.Models;

namespace FactoryDemo.Controllers
{
    public class TermekController : ControllerWithEntities
    {
        //
        // GET: /Product/

        public ActionResult Index()
        {
            List<Termek> model = new List<Termek>();

            var data = from p in EntityContainer.Products
                       select new Termek
                       {
                           Azonosito = p.ProductID,
                           Nev = p.ProductName,
                           Ar = (int?)p.UnitPrice,
                           KategoriaAzonosito = p.CategoryID
                       };
            model = data.ToList();

            return View(model);
        }

        //
        // GET: /Product/Edit/5

        public ActionResult Edit(int id)
        {
            var data = from p in EntityContainer.Products
                       where p.ProductID == id
                       select new Termek
                       {
                           Azonosito = p.ProductID,
                           Nev = p.ProductName,
                           Ar = (int?)p.UnitPrice,
                           KategoriaAzonosito = p.CategoryID
                       };

            Termek model = data.SingleOrDefault();

            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        public ActionResult Edit(Termek model)
        {
            try
            {
                if (model.Nev.Length < 1)
                {
                    ModelState.AddModelError("", "A név nem lehet 3 karakternél rövidebb!");
                    return View(model);
                }
                else if (model.Nev.Length > 20)
                {
                    ModelState.AddModelError("", "A név nem lehet 10 karakternél hosszabb!");
                    return View(model);
                }
                else
                {
                    var data = (from p in EntityContainer.Products
                               where p.ProductID == model.Azonosito
                               select p).SingleOrDefault();

                    data.ProductName = model.Nev;
                    data.CategoryID = model.KategoriaAzonosito;
                    data.UnitPrice = model.Ar;

                    EntityContainer.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
