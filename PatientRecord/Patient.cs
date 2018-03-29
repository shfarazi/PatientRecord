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
        public int ChartNumber { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Birth Date ")]
        [DataType (DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [Display(Name = "Condition")]
        public Condition Condition { get; set; }

        [Display(Name = "Account Balance")]
        [DataType (DataType.Currency)]
        public decimal AccountBalance { get; set; }

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
