using System;
using System.Collections.Generic;

namespace PatientRecord
{
    static class Clinic
    {
        public static Patient CreatePatientRecord(string firstName, string lastName, string emailAddress, string birthDate, Condition condition, decimal balance)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<Patient> GetPatients()
        {
            throw new NotImplementedException();
        }

        internal static void Invoice(int accountNumber, decimal invoiceAmount)
        {
            throw new NotImplementedException();
        }

        internal static void Pay(int accountNumber, decimal paymentAmount)
        {
            throw new NotImplementedException();
        }
    }
}