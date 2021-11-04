using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace LB5_1
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract]
    public interface IWCFSiplex
    {

        [OperationContract]
        int Add(int x, int y);

        [OperationContract]
        string Concat(string str, double d);

        [OperationContract]
        A Sum(A objOne, A objTwo);

    }

    [DataContract]
    public class A
    {
        [DataMember]
        public string s { get; set; }

        [DataMember]
        public int k { get; set; }

        [DataMember]
        public float f { get; set; }
    }
}
