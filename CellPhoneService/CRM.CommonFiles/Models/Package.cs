﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace CRM.CommonFiles.Models
{
    [DataContract]
    public class Package
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public double PackageTotalPrice { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
