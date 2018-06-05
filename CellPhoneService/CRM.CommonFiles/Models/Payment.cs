using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.CommonFiles.Models
{
    public class Payment
    {
        public int Id { get; set; }
        [ForeignKey("Client")]
        public string ClientId { get; set; }
        public Client Client { get; set; }
        public DateTime Date { get; set; }
        public double TotalPayment { get; set; }
    }
}
