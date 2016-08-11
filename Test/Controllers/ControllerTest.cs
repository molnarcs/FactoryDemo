using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess;
using FactoryDemo.Models;
using FactoryDemo.Controllers;
using System.Web.Mvc;

namespace Test.Controllers
{
    [TestClass]
    [DeploymentItem("unity.config")]
    public class ControllerTest : ControllerTestBase
    {
        [TestMethod]
        public void EditTest()
        {
            var northwindEntities = (NorthwindEntitiesMock)DependencyContainer.Resolve<INorthwindEntities>();

            northwindEntities.Reset();

            var product = new Product
            {
                ProductID = 1,
                UnitPrice = 10,
                CategoryID = 1,
                ProductName = "Test"
            };

            northwindEntities.Products.AddObject(product);

            var model = new Termek
            {
                Azonosito = 1,
                Ar = 10,
                KategoriaAzonosito = 1,
                Nev = "Valami"
            };

            var controller = new TermekController();

            var result = ExecuteControllerAction(controller, current => current.Edit(model), url: "http://localhost:1838/Termek/Edit");

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

            var redirectResult = (RedirectToRouteResult)result;

            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);

            Assert.AreEqual("Valami", northwindEntities.Products.First().ProductName);
        }

        [TestMethod]
        public void Edit_Failed_TooShortName()
        {
            var northwindEntities = (NorthwindEntitiesMock)DependencyContainer.Resolve<INorthwindEntities>();

            northwindEntities.Reset();

            var product = new Product
            {
                ProductID = 1,
                UnitPrice = 10,
                CategoryID = 1,
                ProductName = "Test"
            };

            northwindEntities.Products.AddObject(product);

            var model = new Termek
            {
                Azonosito = 1,
                Ar = 10,
                KategoriaAzonosito = 1,
                Nev = "ab"
            };

            var controller = new TermekController();

            var result = ExecuteControllerAction(controller, current => current.Edit(model), url: "http://localhost:1838/Termek/Edit");

            Assert.IsInstanceOfType(result, typeof(ViewResult));

            var viewResult = (ViewResult)result;

            Assert.IsInstanceOfType(viewResult.Model, typeof(Termek));

            Assert.AreEqual("Test", northwindEntities.Products.First().ProductName);
        }

        [TestMethod]
        public void Edit_Failed_TooLongtName()
        {
            var northwindEntities = (NorthwindEntitiesMock)DependencyContainer.Resolve<INorthwindEntities>();

            northwindEntities.Reset();

            var product = new Product
            {
                ProductID = 1,
                UnitPrice = 10,
                CategoryID = 1,
                ProductName = "Test"
            };

            northwindEntities.Products.AddObject(product);

            var model = new Termek
            {
                Azonosito = 1,
                Ar = 10,
                KategoriaAzonosito = 1,
                Nev = "abcdefghijk"
            };

            var controller = new TermekController();

            var result = ExecuteControllerAction(controller, current => current.Edit(model), url: "http://localhost:1838/Termek/Edit");

            Assert.IsInstanceOfType(result, typeof(ViewResult));

            var viewResult = (ViewResult)result;

            Assert.IsInstanceOfType(viewResult.Model, typeof(Termek));

            Assert.AreEqual("Test", northwindEntities.Products.First().ProductName);
        }

    }
}
