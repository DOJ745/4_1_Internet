using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpEndpoint
{
    class Program
    {
        static void Main(string[] args)
        {
            WCFSiplexClient client = new WCFSiplexClient("BasicHttpBinding_IWCFSiplex");

            LB5_1.A objOne = new LB5_1.A();
            LB5_1.A objTwo = new LB5_1.A();

            Console.WriteLine("METHOD ADD\nInput x: ");
            int x = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Input y: ");
            int y = Convert.ToInt32(Console.ReadLine());

            int result = client.Add(x, y);
            Console.WriteLine($"{x} + {y} = " + result);

            Console.WriteLine("METHOD SUM\nOBJ ONE\nInput string:");
            objOne.s = Console.ReadLine();
            Console.WriteLine("Input int:");
            objOne.k = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Input float:");
            objOne.f = float.Parse(Console.ReadLine().Replace(".", ","));

            Console.WriteLine("\nOBJ TWO\nInput string:");
            objTwo.s = Console.ReadLine();
            Console.WriteLine("Input int:");
            objTwo.k = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Input float:");
            objTwo.f = float.Parse(Console.ReadLine().Replace(".", ","));

            LB5_1.A resultA = client.Sum(objOne, objTwo);
            Console.WriteLine($"SUM = {resultA.s} --- {resultA.k} --- {resultA.f}"); 
            Console.ReadLine();
        }
    }
}
