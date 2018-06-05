using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.CommonFiles.Models
{
    public class Client
    {
        public Client()
        {
            Payments = new HashSet<Payment>();
            Lines = new HashSet<Line>();
        }
        private string id;
        [Key]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name { get; set; }
        public string LastName { get; set; }

        [ForeignKey(nameof(TypeId))]
        public int? ClientTypeId { get; set; }
        public ClientType TypeId { get; set; }
        public string Adress { get; set; }
        public string Number { get; set; }
        public int CallstoCenter { get; set; }

        public ICollection<Payment> Payments { get; set; }
        public ICollection<Line> Lines { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        public override string ToString()
        {
            return Name + " " + LastName;
        }
    }
}
