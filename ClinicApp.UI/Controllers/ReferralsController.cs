using PatientRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClinicApp.UI.Controllers
{
    public class ReferralsController : Controller
    {
        // GET: Referrals
        public ActionResult Index()
        {
            return View(new Patient());
        }
    }
}