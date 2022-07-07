using System;
using System.Collections.Generic;

#nullable disable

namespace MVCBank2.Models
{
    public partial class Newaccount
    {
        public Newaccount()
        {
            Transaction1s = new HashSet<Transaction1>();
        }

        public int AccountNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public int? CurrentBalance { get; set; }

        public virtual ICollection<Transaction1> Transaction1s { get; set; }
    }
}
