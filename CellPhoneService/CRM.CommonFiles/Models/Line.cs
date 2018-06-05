using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.CommonFiles.Models
{
    public enum Status
    {
        avaliable, used, stolen, blocked
    }
    public class Line
    {
        public Line()
        {
            SMSs = new HashSet<SMS>();
            Calls = new HashSet<Call>();
        }
        [Key]
        public int Id { get; set; }
        [ForeignKey("Client")]
        public string ClientId { get; set; }
        public Client Client { get; set; }
        public string Number { get; set; }
        public Status Status { get; set; }
        [ForeignKey("Package")]
        public int PackageId { get; set; }
        public Package Package { get; set; }

        public ICollection<SMS> SMSs { get; set; }
        public ICollection<Call> Calls { get; set; }
        public override string ToString()
        {
            return Number;
        }
    }
}
