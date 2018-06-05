using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace CRM.CommonFiles.Models
{
    [DataContract]
    public class ClientType
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string TypeName { get; set; }
        [DataMember]
        public double MinutePrice { get; set; }
        [DataMember]
        public double SMSPrice { get; set; }

        public override string ToString()
        {
            return TypeName;
        }
    }
}
