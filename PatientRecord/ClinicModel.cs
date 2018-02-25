using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientRecord
{
    public class ClinicModel : DbContext
    {
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
    }
}
