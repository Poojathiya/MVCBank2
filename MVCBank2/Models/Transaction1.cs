using System;

#nullable disable

namespace MVCBank2.Models
{
    public partial class Transaction1
    {
        public int TransactionId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public int AccountNumber { get; set; }
        public int? Amount { get; set; }
        public string TransactionType { get; set; }

        public virtual Newaccount AccountNumberNavigation { get; set; }
    }
}
