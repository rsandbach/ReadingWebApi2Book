using System;

namespace WebApi2Book.Common.Extensions
{
    public static class UriExtensions
    {
        public static Uri GetBaseUri(this Uri orginalUri)
        {
            var queryDelimeterIndex = orginalUri.AbsoluteUri.IndexOf("?", StringComparison.Ordinal);
            return queryDelimeterIndex < 0
                ? orginalUri
                : new Uri(orginalUri.AbsoluteUri.Substring(0, queryDelimeterIndex));
        }

        public static string QueryWithoutLeadingQuestionMark(this Uri uri)
        {
            const int indexToSkipQueryDelimeter = 1;
            return uri.Query.Length > 1 ? uri.Query.Substring(indexToSkipQueryDelimeter) : string.Empty;
        }
    }
}