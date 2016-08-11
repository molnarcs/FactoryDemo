using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using FactoryDemo.Controllers;
using DataAccess;
using Test.Extensions;
using System.Web.Routing;
using FactoryDemo;

namespace Test.Controllers
{
    public class ControllerTestBase
    {

        // ---- Operations

        public ActionResult ExecuteControllerAction<T>(T controller, Func<T, ActionResult> controllerAction, string url = "http://localhost")
            where T : ControllerWithEntities
        {
            SetupController(controller, url);

            try
            {
                var result = controllerAction(controller);
                return result;
            }
            finally
            {
                TeardownController(controller);
            }
        }

        private void SetupController(ControllerWithEntities controller, string url)
        {
            var httpContext = MvcMockHelpers.FakeHttpContext(url);
            var controllerContext = new ControllerContext(httpContext, new RouteData(), controller);

            controller.ControllerContext = controllerContext;
            controller.Url = new UrlHelper(controllerContext.RequestContext, RouteTable.Routes);

            controller.SetupBeforeRequest();

            SetupEntityContainer(controller as ControllerWithEntities);
        }

        private void TeardownController(ControllerWithEntities controller)
        {
            controller.TeardownAfterRequest();
        }

        private void SetupEntityContainer(ControllerWithEntities controller)
        {
            if (controller == null)
                return;

            var northwindEntities = DependencyContainer.Resolve<INorthwindEntities>();
            controller.EntityContainer = northwindEntities;
        }

    }
}
