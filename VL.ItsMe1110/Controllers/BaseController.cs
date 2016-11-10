using System.Web.Mvc;

namespace VL.ItsMe1110.Controllers
{
    public abstract class BaseController : Controller
    {
        public const string PageNameOfError = "error";
        public const string PageNameOfHome = "Home";
        public const string PageNameOfAccount = "Account";
        public const string PageNameOfManage = "Manage";
    }
}