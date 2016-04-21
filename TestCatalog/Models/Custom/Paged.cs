using System.Collections.Generic;

namespace TestCatalog.Models.Custom
{
    public class Paged<T> where T : class
    {
        public Paged()
        {
            Items = new HashSet<T>();
        }

        public ICollection<T> Items { get; set; }

        public int Total { get; set; }

        public int Pages { get; set; }

        public int Page { get; set; }
    }

}