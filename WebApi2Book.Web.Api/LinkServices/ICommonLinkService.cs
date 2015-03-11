using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.LinkServices
{
    public interface  ICommonLinkService
    {
        void AddPageLinks(IPageLinkContaining linkContainer, string currentPageQueryString,
            string previousPageQueryString, string nextPageQuerySTring);

        Link GetLink(string pathFragment, string relValue, HttpMethod httpMethod);
    }
}