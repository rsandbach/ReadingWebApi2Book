using System.Collections.Generic;
using System.Linq;
using NHibernate.Util;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Data;
using WebApi2Book.Data.QueryProcessors;
using WebApi2Book.Web.Api.LinkServices;
using WebApi2Book.Web.Api.Models;
using PagedTaskDataInquiryResponse =
    WebApi2Book.Web.Api.Models.PagedDataInquiryResponse<WebApi2Book.Web.Api.Models.Task>;

namespace WebApi2Book.Web.Api.InquiryProcessing
{
    public class AllTasksInquiryProcessor : IAllTasksInquiryProcessor
    {
        private const string QueryStringFormat = "pagenumber={0}&pagesize={1}";

        private readonly IAutoMapper _autoMapper;
        private readonly ICommonLinkService _commonLinkService;
        private readonly IAllTasksQueryProcessor _queryProcessor;
        private readonly ITaskLinkService _taskLinkService;

        public AllTasksInquiryProcessor(IAllTasksQueryProcessor queryProcessor, IAutoMapper autoMapper,
            ITaskLinkService taskLinkService, ICommonLinkService commonLinkService)
        {
            _queryProcessor = queryProcessor;
            _autoMapper = autoMapper;
            _taskLinkService = taskLinkService;
            _commonLinkService = commonLinkService;
        }

        public PagedDataInquiryResponse<Task> GetTasks(PagedDataRequest requestInfo)
        {
            var queryResult = _queryProcessor.GetTasks(requestInfo);
            var tasks = GetTasks(queryResult.QueriedItems).ToList();

            var inquryResponse = new PagedTaskDataInquiryResponse
            {
                Items = tasks,
                PageCount = queryResult.TotalPageCount,
                PageNumber = requestInfo.PageNumber,
                PageSize = requestInfo.PageSize
            };

            AddLinksToInquiryResponse(inquryResponse);

            return inquryResponse;
        }

        public virtual void AddLinksToInquiryResponse(PagedTaskDataInquiryResponse inquryResponse)
        {
            inquryResponse.AddLink(_taskLinkService.GetAllTasksLink());

            _commonLinkService.AddPageLinks(inquryResponse
                , GetCurrentPageQueryString(inquryResponse)
                , GetPreviousPageQueryString(inquryResponse)
                , GetNextPageQueryString(inquryResponse));
        }

        public virtual string GetNextPageQueryString(PagedTaskDataInquiryResponse inquryResponse)
        {
            return string.Format(QueryStringFormat, inquryResponse.PageNumber + 1, inquryResponse.PageSize);
        }

        public virtual string GetPreviousPageQueryString(PagedTaskDataInquiryResponse inquryResponse)
        {
            return string.Format(QueryStringFormat, inquryResponse.PageNumber - 1, inquryResponse.PageSize);
        }

        public virtual string GetCurrentPageQueryString(PagedTaskDataInquiryResponse inquryResponse)
        {
            return string.Format(QueryStringFormat, inquryResponse.PageNumber, inquryResponse.PageSize);
        }

        public virtual IEnumerable<Task> GetTasks(IEnumerable<Data.Entities.Task> taskEntities)
        {
            var tasks = taskEntities.Select(x => _autoMapper.Map<Task>(x)).ToList();

            tasks.ForEach(x => {
                _taskLinkService.AddSelfLink(x);
                _taskLinkService.AddLinksToChildObjects(x);
            });

            return tasks;
        }
    }
}