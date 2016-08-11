using System;
using System.Web.Mvc;
using FactoryDemo.Controllers;
using DataAccess;

namespace FactoryDemo.ActionFilters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class NeedsEntityContainerAttribute : ActionFilterAttribute
    {

        // ---- Operations

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (!(filterContext.Controller is ControllerWithEntities))
                return;

            var controller = (ControllerWithEntities)filterContext.Controller;
            controller.EntityContainer = DependencyContainer.Resolve<INorthwindEntities>();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            if (!(filterContext.Controller is ControllerWithEntities))
                return;

            var controller = (ControllerWithEntities)filterContext.Controller;

            if (controller.EntityContainer != null)
            {
                controller.EntityContainer.Dispose();
                controller.EntityContainer = null;
            }
        }

    }
}