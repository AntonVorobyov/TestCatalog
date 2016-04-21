using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TestCatalog.Models;
using TestCatalog.Models.Custom;

namespace TestCatalog.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<Paged<T>> ToPagedAsync<T>(this IQueryable<T> query, Filter filter = null) where T : class
        {
            var take = filter?.Take ?? 10;
            var page = filter?.Page ?? 1;
            var skip = (page - 1) * take;

            var total = await query.CountAsync();
            var result = await query.Skip(skip).Take(take).ToListAsync();

            return new Paged<T>
            {
                Items = result,
                Page = page,
                Pages = (int)Math.Ceiling((double)total / take),
                Total = total
            };
        }

        public static IQueryable<Country> Filter(this IQueryable<Country> query, Filter filter)
        {
            if (filter == null)
                return query;

            if (!string.IsNullOrEmpty(filter.Query))
                return query.Where(c => c.Title.Contains(filter.Query));

            return query;
        }

        public static IQueryable<User> Filter(this IQueryable<User> query, Filter filter)
        {
            if (filter == null)
                return query;

            if (!string.IsNullOrEmpty(filter.Query))
                return query.Where(c => c.FIO.Contains(filter.Query));

            return query;
        }
    }
}