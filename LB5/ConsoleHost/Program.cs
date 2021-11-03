using LB5;
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
            using (ServiceHost host = new ServiceHost(typeof(WCFSiplex)))
            {
                host.Open();
                Console.WriteLine("HOST STATUS: OK");
                Console.ReadLine();
            }
        }
    }
}
