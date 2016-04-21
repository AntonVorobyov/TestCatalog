using System;
using System.Linq;
using System.Linq.Expressions;
using TestCatalog.Models;
using TestCatalog.Models.Custom;
using TestCatalog.Models.Types;

namespace TestCatalog.Extensions
{
    public static class OrderedQueryableExtensions
    {
        private static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, TKey>> keySelector,
            OrderType order)
        {
            return order == OrderType.Asc
                ? source.OrderBy(keySelector)
                : source.OrderByDescending(keySelector);
        }

        private static IOrderedQueryable<TSource> ThenBy<TSource, TKey>(
            this IOrderedQueryable<TSource> source,
            Expression<Func<TSource, TKey>> keySelector,
            OrderType order)
        {
            return order == OrderType.Asc
                ? source.ThenBy(keySelector)
                : source.ThenByDescending(keySelector);
        }

        private static IOrderedQueryable<User> Sort(this IQueryable<User> query, SortType sort, OrderType order)
        {
            switch (sort)
            {
                case SortType.ByCountry:
                    return query.OrderBy(q => q.Country.Title, order);
                case SortType.ByName:
                default:
                    return query.OrderBy(q => q.FIO, order);
            }
        }

        public static IQueryable<User> Sort(this IQueryable<User> query, Filter filter)
        {
            return filter?.Sorting == null
                ? query.Sort(SortType.ByName, OrderType.Asc)
                : Sort(query, filter.Sorting.Sort, filter.Sorting.Order);
        }
    }
}
