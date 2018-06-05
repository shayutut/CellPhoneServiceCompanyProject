using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost service = new ServiceHost(typeof(CRMService)))
            {
                service.Open();
                Console.WriteLine("The service is runing...");
                Console.ReadKey();
            }
        }
    }
}
