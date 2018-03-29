using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientRecord
{
    public enum TypesOfTransaction
    {
        Invoice,
        Payment
    }

    public class Transaction
    {
        [Key]
        [Display(Name = "Transaction Id")]
        public int TransactionId { get; set; }

        [Display(Name = "Transaction Date")]
        public DateTime TransactionDate { get; set; }

        [Display(Name = "Transaction Amount")]
        public decimal TransactionAmount { get; set; }

        public string Description { get; set; }

        [Display(Name = "Transaction Type")]
        public TypesOfTransaction TransactionType { get; set; }

        [ForeignKey("Patient")]
        [Display(Name = "Chart Number")]
        public int ChartNumber { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
