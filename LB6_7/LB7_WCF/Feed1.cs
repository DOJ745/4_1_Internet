using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.ServiceModel.Web;
using System.Text;

namespace LB7_WCF
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде и файле конфигурации.
    public class Feed1 : IFeed1
    {
        private LB6_7.WSSDAEntities entities;

        public Feed1()
        {
            entities = new LB6_7.WSSDAEntities();
        }
        public SyndicationFeedFormatter CreateFeed()
        {
            // Создать новый веб-канал.
            SyndicationFeed feed = new SyndicationFeed(
                title: "FEED",
                description: "WCF Syndication Feed",
                //feedAlternateLink: new Uri("http://localhost:8733/Design_Time_Addresses/WCFSyndicationService/Feed1/"),
                feedAlternateLink: new Uri("http://localhost:8733/Design_Time_Addresses/LB7_WCF/Feed1/"),
                id: "id",
                lastUpdatedTime: new DateTimeOffset(DateTime.Now),
                items: GetNewsItems());

            // Возвращать канал ATOM или RSS, основываясь на строке запроса
            // RSS-&gt; http://localhost:8733/Design_Time_Addresses/WCFSyndicationService/Feed1/
            // http://localhost:8733/Design_Time_Addresses/LB7_WCF/Feed1/

            // Atom-&gt; http://localhost:8733/Design_Time_Addresses/WCFSyndicationService/Feed1/?format=atom
            // http://localhost:8733/Design_Time_Addresses/LB7_WCF/Feed1/?format=atom
            string query = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["format"];
            SyndicationFeedFormatter formatter = null;
            if (query == "atom")
            {
                formatter = new Atom10FeedFormatter(feed);
            }
            else
            {
                formatter = new Rss20FeedFormatter(feed);
            }

            return formatter;
        }

        private List<SyndicationItem> GetNewsItems()
        {
            List<SyndicationItem> items = new List<SyndicationItem>();
            foreach (var note in entities.Note.AsEnumerable())
            {
                var student = (from stud in entities.Student where stud.id == note.studentId select stud).First();
                items.Add(new SyndicationItem(
                    title: note.subject + " : " + note.note1,
                    content: student.name.ToString(),
                    itemAlternateLink: new Uri("http://localhost:56386/MainWcfDataService.svc/Note(" + note.id + ")"),
                    id: student.id.ToString(),
                    lastUpdatedTime: DateTime.Now));
            }
            return items;
        }
    }
}
