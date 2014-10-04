namespace WebApi2Book.Web.Api.Models
{
    public class Link
    {
        public string Rel { get; set; }
        public string Href { get; set; }
        public string Method { get; set; }
    }

    public class Status
    {
        public long StatusId { get; set; }
        public string Name { get; set; }
        public int Ordinal { get; set; }
    }
}