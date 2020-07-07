using System;
using System.Collections.Generic;
using System.Text;

namespace EFModule.Data.Models.DB
{
    public class CreditCardInfo
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public string CardHolderName { get; set; }
        public virtual Employees Employee { get; set; }
    }
}
