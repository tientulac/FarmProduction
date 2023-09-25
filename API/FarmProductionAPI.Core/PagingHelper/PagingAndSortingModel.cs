namespace FarmProductionAPI.Core.PagingHelper
{
    public class PagingAndSortingModel
    {

        public PagingAndSortingModel()
        {
            PageSize = 20;
            PageIndex = 1;
            OrderColumn = string.Empty;
            OrderDirection = string.Empty;
            Filters = new List<FilterModel>();
        }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public string OrderColumn { get; set; }

        public string OrderDirection { get; set; }

        public IEnumerable<FilterModel> Filters { get; set; }

    }

    public class FilterModel
    {
        public string Field { get; set; }

        public string Value { get; set; }
    }
}
