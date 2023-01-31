using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Tracker.Models
{
    [Table("user")]
    public class User
    {
        public int id { get; set; }
        public string email { get; set; }
        [JsonIgnore]
        public string password { get; set; }
        public string name { get; set; }
    }
}
