using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TestCatalog.Models
{
    [MetadataType(typeof(CountryMetadata))]
    public partial class Country
    {
         
    }

    public class CountryMetadata
    {
        [JsonIgnore]
        public ICollection<User> Users { get; set; }
    }
}