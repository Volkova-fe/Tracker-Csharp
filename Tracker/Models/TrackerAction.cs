using System.ComponentModel.DataAnnotations;

namespace Tracker.Models
{
    public class TrackerAction
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string type { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy'/'MM'/'dd}", ApplyFormatInEditMode = true)]
        public DateTime date { get; set; }
    }
}
