using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientRecord
{
    public enum Condition
    {
        Healthy,
        Periodontal,
        Restorative,
        Prostodontics,
        Endodontic,
        OralSurgery,
        Orthodontics
    }

    public class Patient
    {

        [Key]
        public int ChartNumber { get; private set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime CreatedDate { get; private set; }

        public Condition Condition { get; set; }

        public decimal AccountBalance { get; private set; }

        public Patient()
        {
            CreatedDate = DateTime.Now;
        }

        public void Charge(decimal amount)
        {
            AccountBalance += amount;
        }

        public decimal Pay(decimal amount)
        {
            AccountBalance -= amount;
            return AccountBalance;
        }
    }
}
