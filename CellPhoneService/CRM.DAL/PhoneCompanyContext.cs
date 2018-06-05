using CRM.CommonFiles.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL
{
    public class PhoneCompanyContext : DbContext
    {
        public PhoneCompanyContext()
           : base("name=PhoneCompanyContext")
        {
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Call> Calls { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<PackageInclude> PackageIncludes { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ClientType> ClientTypes { get; set; }
        public DbSet<SMS> SMSs { get; set; }
        public DbSet<SelectedNumbers> SelectedNumbers { get; set; }
    }
}
