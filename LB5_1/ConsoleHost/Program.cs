using LB5_1;
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
                if(host.State == CommunicationState.Opened)
                {
                    Console.WriteLine("PRESS A BUTTON TO CLOSE HOST\n");
                    Console.WriteLine("HOST STATUS: " + host.State);
                }
                Console.ReadLine();
            }
        }
    }
}
