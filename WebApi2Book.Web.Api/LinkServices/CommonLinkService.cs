﻿using System;
using System.Net.Http;
using WebApi2Book.Common;
using WebApi2Book.Common.Extensions;
using WebApi2Book.Common.Security;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.LinkServices
{
    public class CommonLinkService : ICommonLinkService
    {
        private readonly IWebUserSession _userSession;

        public CommonLinkService(IWebUserSession userSession)
        {
            _userSession = userSession;
        }

        public void AddPageLinks(IPageLinkContaining linkContainer, string currentPageQueryString, string previousPageQueryString,
            string nextPageQuerySTring)
        {
            var versionedBaseUri = _userSession.RequestUri.GetBaseUri();
            AddCurrentPageLink(linkContainer, versionedBaseUri, currentPageQueryString);
            var addPrevPageLink = ShouldAddPreviousPageLink(linkContainer.PageNumber);
            var addNextPageLink = ShouldAddNextPageLink(linkContainer.PageNumber, linkContainer.PageCount);

            if (addPrevPageLink || addNextPageLink)
            {
                if (addPrevPageLink)
                {
                    AddPreviousPageLink(linkContainer, versionedBaseUri, previousPageQueryString);
                }

                if (addNextPageLink)
                {
                    AddNextPageLink(linkContainer, versionedBaseUri, previousPageQueryString);
                }
            }
        }

        public bool ShouldAddNextPageLink(int pageNumber, int pageCount)
        {
            return pageNumber < pageCount;
        }

        public bool ShouldAddPreviousPageLink(int pageNumber)
        {
            return pageNumber > 1;
        }

        public virtual void AddCurrentPageLink(IPageLinkContaining linkContainer, Uri versionedBaseUri, string pageQueryString)
        {
            var uriBuilder = new UriBuilder(versionedBaseUri)
            {
                Query = pageQueryString
            };
            linkContainer.AddLink(GetCurrentPageLink(uriBuilder.Uri));
        }

        public virtual void AddPreviousPageLink(IPageLinkContaining linkContainer, Uri versionedBaseUri, string pageQueryString)
        {
            var uriBuilder = new UriBuilder(versionedBaseUri)
            {
                Query = pageQueryString
            };
            linkContainer.AddLink(GetPreviousPageLink(uriBuilder.Uri));
        }

        public virtual void AddNextPageLink(IPageLinkContaining linkContainer, Uri versionedBaseUri, string pageQueryString)
        {
            var uriBuilder = new UriBuilder(versionedBaseUri)
            {
                Query = pageQueryString
            };
            linkContainer.AddLink(GetNextPageLink(uriBuilder.Uri));
        }

        public virtual Link GetCurrentPageLink(Uri uri)
        {
            return new Link
            {
                Href = uri.AbsoluteUri,
                Rel = Constants.CommonLinkRelValues.CurrentPage,
                Method = HttpMethod.Get.Method
            };
        }

        public virtual Link GetPreviousPageLink(Uri uri)
        {
            return new Link
            {
                Href = uri.AbsoluteUri,
                Rel = Constants.CommonLinkRelValues.PeviousPage,
                Method = HttpMethod.Get.Method
            };
        }

        public virtual Link GetNextPageLink(Uri uri)
        {
            return new Link
            {
                Href = uri.AbsoluteUri,
                Rel = Constants.CommonLinkRelValues.NextPage,
                Method = HttpMethod.Get.Method
            };
        }
        public Link GetLink(string pathFragment, string relValue, HttpMethod httpMethod)
        {
            const string delimitedVersionedApiRouteBaseFormatString =
                Constants.CommonRoutingDefinitions.ApiSegmentName + "/{0}/";

            var path = string.Concat(
                string.Format(
                    delimitedVersionedApiRouteBaseFormatString,
                    _userSession.ApiVersionInUse), pathFragment);

            var uriBuilder = new UriBuilder
            {
                Scheme = _userSession.RequestUri.Scheme,
                Host = _userSession.RequestUri.Host,
                Port = _userSession.RequestUri.Port,
                Path = path
            };

            var link = new Link
            {
                Href = uriBuilder.Uri.AbsoluteUri,
                Rel = relValue,
                Method = httpMethod.Method
            };

            return link;
        }
    }
}