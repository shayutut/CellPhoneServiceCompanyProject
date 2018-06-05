using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Common.Models
{
    public class PackageInclude
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Package")]
        public int PackageId { get; set; }
        public Package Package { get; set; }
        public string Name { get; set; }
        public int MaxMinute { get; set; }
        public double FixedPrice { get; set; }
        public double DiscountPrecentage { get; set; }
        [ForeignKey("FavoriteNumbers")]
        public int SelectedNumbersId { get; set; }
        public SelectedNumbers FavoriteNumbers { get; set; }
        public bool MostCalledNumber { get; set; }
        public bool InsedeFamilyCalls { get; set; }
    }
}
