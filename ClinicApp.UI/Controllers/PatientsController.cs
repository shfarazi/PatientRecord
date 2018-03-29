using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PatientRecord;

namespace ClinicApp.UI.Controllers
{
    public class PatientsController : Controller
    {
        private ClinicModel db = new ClinicModel();

        // GET: Patients
        [Authorize]

        public ActionResult Index()
        {
            return View(Clinic.GetPatientsByEmailAddress(HttpContext.User.Identity.Name));
        }

        // GET: Patients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = Clinic.GetPatientByPatientNumber(id.Value);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // GET: Patients/Create
        public ActionResult Create()
        {
            var patient = new Patient
            {
                EmailAddress = HttpContext.User.Identity.Name
            };

            return View(patient);
        }

        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChartNumber,FirstName,LastName,EmailAddress,BirthDate,CreatedDate,Condition,AccountBalance")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                Clinic.CreatePatientRecord(patient);
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        // GET: Patients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = Clinic.GetPatientByPatientNumber (id.Value);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChartNumber,FirstName,LastName,EmailAddress,BirthDate,CreatedDate,Condition,AccountBalance")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                Clinic.EditPatient(patient);
                //db.Entry(patient).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }
        public ActionResult Charge(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = Clinic.GetPatientByPatientNumber(id.Value);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        [HttpPost]
        public ActionResult Charge(FormCollection controls)
        {
            var patientNumber = Convert.ToInt32(controls["ChartNumber"]);
            var amount = Convert.ToDecimal(controls["Amount"]);
            Clinic.Charge(patientNumber, amount);

            return RedirectToAction("Index");
        }

        public ActionResult Pay(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = Clinic.GetPatientByPatientNumber(id.Value);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        [HttpPost]
        public ActionResult Pay(FormCollection controls)
        {
            var patientNumber = Convert.ToInt32(controls["ChartNumber"]);
            var amount = Convert.ToDecimal(controls["Amount"]);
            Clinic.Pay(patientNumber, amount);

            return RedirectToAction("Index");
        }

        public ActionResult Transactions(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var transactions = Clinic.GetTransactionsByPatientNumber(id.Value);

            return View(transactions);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
