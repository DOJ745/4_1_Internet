using System.Dynamic;

namespace LB3.Models
{
    public abstract class HATEOASModel
    {
        public dynamic _links = new ExpandoObject();
    }

    public class Link
    {
        public string href;
        public Link(string href) { this.href = href; }
    }
}