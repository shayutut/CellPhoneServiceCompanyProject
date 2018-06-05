namespace CRM.DAL.Migrations
{
    using CRM.CommonFiles.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CRM.DAL.PhoneCompanyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CRM.DAL.PhoneCompanyContext context)
            {
                context.ClientTypes.AddOrUpdate(t => t.Id,
                                   new ClientType() { Id = 1, TypeName = "Business", MinutePrice = 0.8, SMSPrice = 0.2 },
                                   new ClientType() { Id = 2, TypeName = "Private", MinutePrice = 0.5, SMSPrice = 0.5 },
                                   new ClientType() { Id = 3, TypeName = "VIP", MinutePrice = 0.7, SMSPrice = 0.3 });

                context.Packages.AddOrUpdate(p => p.Name,
                  new Package() { Name = "Aboard", PackageTotalPrice = 99.99 },
                  new Package() { Name = "All Includes", PackageTotalPrice = 49.50 },
                  new Package() { Name = "Family", PackageTotalPrice = 39.90 });
            }
    }
}
