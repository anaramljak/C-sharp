using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    public class PaymentSlip
    {
        public string PayerName { get; set; }
        public string RecipientName { get; set; }
        public string Currency { get; set; }
        public string Total { get; set; }
        public string PayerIBAN { get; set; }
        public string PayerModel { get; set; }
        public string PayerNumber { get; set; }
        public string RecipientIBAN { get; set; }
        public string RecipientModel { get; set; }
        public string RecipientNumber { get; set; }
        public string PurposeCode { get; set; }
        public string PaymentDescription { get; set; }
        public DateTime Date { get; set; }
        public string Emergency { get; set; }
    }
}
