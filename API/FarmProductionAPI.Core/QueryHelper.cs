using FarmProductionAPI.Core.PagingHelper;
using System.Linq.Expressions;

namespace FarmProductionAPI.Core
{
    public static class QueryHelper
    {
        public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(
            this IQueryable<TSource> query,
            Expression<Func<TSource, TKey>> keySelector,
            bool isDescending)
        {
            return isDescending ? query.OrderByDescending(keySelector) : query.OrderBy(keySelector);
        }


        public static IQueryable<T> BuildOrderExpression<T>(IQueryable<T> queryAble, PagingAndSortingModel pagingAndSortingModel)
        {
            // get orderby property
            var property = typeof(T).GetProperties()
                .FirstOrDefault(x => x.GetCustomAttributes(typeof(OrderableAttribute), false)
                    .Any() && x.Name.Equals(pagingAndSortingModel.OrderColumn, StringComparison.InvariantCultureIgnoreCase));
            return queryAble;
        }
    }
}
