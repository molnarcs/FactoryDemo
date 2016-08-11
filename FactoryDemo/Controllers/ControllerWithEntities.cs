using DataAccess;
using System.Web.Mvc;
using FactoryDemo.ActionFilters;
using System.Web.Routing;

namespace FactoryDemo.Controllers
{
    [NeedsEntityContainer]
    public class ControllerWithEntities : Controller
    {

        // ---- Fields

        private INorthwindEntities _entityContainer;

        // ---- Properties

        public INorthwindEntities EntityContainer
        {
            get { return _entityContainer; }
            set { _entityContainer = value; }
        }


        // -- Operations

        public virtual void SetupBeforeRequest()
        {
        }

        public virtual void TeardownAfterRequest()
        {
        }

        protected override void Execute(RequestContext requestContext)
        {
            SetupBeforeRequest();

            base.Execute(requestContext);

            TeardownAfterRequest();
        }
    }
}