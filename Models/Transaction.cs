using System.Text.Json.Serialization;
using skuska_01.JsonConverters;

namespace skuska_01.Models
{
    public class Transaction
    {
        public int id { get; set; }
        public DateTime transactionDate { get; set; }
        public string description { get; set; }
        public string category { get; set; }

        [JsonConverter(typeof(FlexibleDecimalConverter))]
        public decimal? debit { get; set; }

        [JsonConverter(typeof(FlexibleDecimalConverter))]
        public decimal? credit { get; set; }
    }
}
