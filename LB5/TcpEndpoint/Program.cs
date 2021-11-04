using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpEndpoint
{
    class Program
    {
        static void Main(string[] args)
        {
            WCFSiplexClient client = new WCFSiplexClient("NetTcpBinding_IWCFSiplex");

            Console.WriteLine("METHOD CONCAT\nInput stirng: ");
            string str = Console.ReadLine();
            Console.WriteLine("Input float: ");
            double d = double.Parse(Console.ReadLine().Replace(".", ","));

            string result = client.Concat(str, d);
            Console.WriteLine($"{str} + {d} = " + result);
            Console.ReadLine();
            // Всегда закройте клиент.
            client.Close();
        }
    }
}
