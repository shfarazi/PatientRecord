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
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal TransactionAmount { get; set; }
        public string Description { get; set; }
        public TypesOfTransaction TransactionType { get; set; }
        [ForeignKey("Patient")]
        public int ChartNumber { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
