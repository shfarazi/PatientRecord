using System;
using System.Collections.Generic;
using System.Linq;

namespace PatientRecord
{
    public static class Clinic
    {
        private static ClinicModel db = new ClinicModel();

        public static Patient CreatePatientRecord(string firstName, string lastName, string emailAddress, DateTime birthDate, Condition condition, decimal balance)
        {
            if (string.IsNullOrEmpty(emailAddress) || string.IsNullOrWhiteSpace(emailAddress))
                throw new ArgumentNullException("emailAddress", "Email Address cannot be empty.");


            var Patient = new Patient
            {
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = emailAddress,
                BirthDate = birthDate,
                Condition = condition,
            };

            db.Patients.Add(Patient);
            db.SaveChanges();

            ChargeByCondition(condition, Patient);

            return Patient;
        }

        private static void ChargeByCondition(Condition condition, Patient Patient)
        {
            if (condition == Condition.Healthy)
                Charge(Patient.ChartNumber, 350);

            if (condition == Condition.Periodontal)
                Charge(Patient.ChartNumber, 800);

            if (condition == Condition.Restorative)
                Charge(Patient.ChartNumber, 500);

            if (condition == Condition.Prostodontics)
                Charge(Patient.ChartNumber, 1800);

            if (condition == Condition.Endodontic)
                Charge(Patient.ChartNumber, 1200);

            if (condition == Condition.OralSurgery)
                Charge(Patient.ChartNumber, 2000);

            if (condition == Condition.Orthodontics)
                Charge(Patient.ChartNumber, 5000);

        }

        public static Patient CreatePatientRecord(Patient patient)
        {
            if (patient == null)
                throw new ArgumentNullException("patient", "Patient cannot be empty.");
            return CreatePatientRecord(patient.FirstName, patient.LastName, patient.EmailAddress, patient.BirthDate, patient.Condition, patient.AccountBalance);
        }

        public static IEnumerable<Patient> GetPatients()
        {
            return db.Patients;
        }
    
        public static IEnumerable<Patient> GetPatientsByEmailAddress(string emailAddress)
        {
            return db.Patients.Where(a => a.EmailAddress == emailAddress);
        }

        public static Patient GetPatientByPatientNumber(int PatientNumber)
        {
            var Patient = db.Patients.Where(a => a.ChartNumber == PatientNumber).FirstOrDefault();
            if (Patient == null)
                return null;

            return Patient;

        }

        public static IEnumerable<Transaction> GetTransactionsByPatientNumber(int PatientNumber)
        {
            return db.Transactions.Where(t => t.ChartNumber == PatientNumber).OrderByDescending(t => t.TransactionDate);
        }

        public static void Charge(int PatientNumber, decimal amount)
        {
            var Patient = GetPatientByPatientNumber(PatientNumber);
            Patient.Charge(amount);

            var transaction = new Transaction
            {
                TransactionDate = DateTime.Now,
                TransactionAmount = amount,
                Description = "Office charge",
                TransactionType = TypesOfTransaction.Invoice,
                ChartNumber = Patient.ChartNumber
            };
            db.Transactions.Add(transaction);
            db.SaveChanges();
        }
        public static void Pay(int PatientNumber, decimal amount)
        {
            var Patient = GetPatientByPatientNumber(PatientNumber);
            Patient.Pay(amount);

            var transaction = new Transaction
            {
                TransactionDate = DateTime.Now,
                TransactionAmount = amount,
                Description = "Online payment",
                TransactionType = TypesOfTransaction.Payment,
                ChartNumber = Patient.ChartNumber
            };
            db.Transactions.Add(transaction);
            db.SaveChanges();

        }

        public static void EditPatient(Patient patient)
        {
            var oldPatient = GetPatientByPatientNumber(patient.ChartNumber);
            var oldCondition = oldPatient.Condition;

            oldPatient.Condition = patient.Condition;
            oldPatient.FirstName = patient.FirstName;
            oldPatient.LastName = patient.LastName;
            oldPatient.BirthDate = patient.BirthDate;

            db.Entry(oldPatient).CurrentValues.SetValues(oldPatient);
            db.SaveChanges();

            if (oldCondition != patient.Condition)
            {
                ChargeByCondition(patient.Condition, oldPatient);
            }
        }
    }
}
   
