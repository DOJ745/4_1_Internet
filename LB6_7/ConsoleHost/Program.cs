using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(LB7_WCF.Feed1)))
            {
                host.Open();
                Console.WriteLine("Press a button to close the host");
                Console.WriteLine("Host status: " + host.State);
                Console.ReadLine();
            }
        }
    }
}
