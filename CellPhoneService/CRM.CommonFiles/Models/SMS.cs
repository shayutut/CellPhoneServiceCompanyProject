using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.CommonFiles.Models
{
    public class SMS
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Line")]
        public int LineId { get; set; }
        public Line Line { get; set; }
        public double ExternalPrice { get; set; }
        public string DestinaionNumber { get; set; }
    }
}
