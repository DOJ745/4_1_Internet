using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace LB5
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class WCFSiplex : IWCFSiplex
    {
        public int Add(int x, int y)
        {
            return x + y;
        }

        public string Concat(string str, double d)
        {
            return str + d;
        }

        public A Sum(A objOne, A objTwo)
        {
            A result = new A();

            result.s = objOne.s + objTwo.s;
            result.k = objOne.k + objTwo.k;
            result.f = objOne.f + objTwo.f;
            return result;
        }
    }
}
