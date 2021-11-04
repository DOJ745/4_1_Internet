using LB5;
using System;

namespace HttpEndpoint
{
    class Program
    {
        static void Main(string[] args)
        {
            WCFSiplexClient client = new WCFSiplexClient("BasicHttpBinding_IWCFSiplex");

            Console.WriteLine("METHOD ADD\nInput x: ");
            int x = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Input y: ");
            int y = Convert.ToInt32(Console.ReadLine());

            int result = client.Add(x, y);
            Console.WriteLine($"{x} + {y} = " + result);
            Console.ReadLine();
            // Всегда закройте клиент.
            client.Close();
        }
    }
}
