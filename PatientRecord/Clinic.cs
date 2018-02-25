using System;
using System.Collections.Generic;
using System.Linq;

namespace PatientRecord
{
    static class Clinic
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

            if (balance > 0)
                Patient.Charge(balance);

            db.Patients.Add(Patient);
            db.SaveChanges();
            return Patient;
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
                Description = "Branch deposit",
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
                Description = "Branch withdraw",
                TransactionType = TypesOfTransaction.Payment,
                ChartNumber = Patient.ChartNumber
            };
            db.Transactions.Add(transaction);
            db.SaveChanges();

        }

        public static void EditPatient(Patient Patient)
        {
            var oldPatient = GetPatientByPatientNumber(Patient.ChartNumber);
            oldPatient.EmailAddress = Patient.EmailAddress;
            oldPatient.Condition = Patient.Condition;
            oldPatient.FirstName = Patient.FirstName;
            oldPatient.LastName = Patient.LastName;

            db.Entry(oldPatient).CurrentValues.SetValues(oldPatient);
            db.SaveChanges();
        }
    }
}
   
