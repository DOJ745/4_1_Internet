using System;
using System.ServiceModel.Syndication;
using System.Xml;

namespace LB7_CLIENT
{
    class Program
    {
        static void Main(string[] args)
        {
            string result = "";

            XmlReader xmlReader = XmlReader.Create("http://localhost:8733/Design_Time_Addresses/LB7_WCF/Feed1");
            SyndicationFeed feed = SyndicationFeed.Load(xmlReader);
            foreach (SyndicationItem item in feed.Items)
            {
                result += item.Title.Text + "\n";
            }

            Console.WriteLine(result);
        }
    }
}
