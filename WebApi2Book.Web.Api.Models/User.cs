using System.Collections.Generic;

namespace WebApi2Book.Web.Api.Models
{
    public class User
    {
        private List<Link> _links;

        public long UserId { get; set; }
        public virtual string Firstname { get; set; }
        public virtual string Lastname { get; set; }
        public virtual string Username { get; set; }

        public List<Link> Links
        {
            get { return _links ?? (_links = new List<Link>()); }
        }

        public void AddLink(Link link)
        {
            Links.Add(link);
        }
    }
}