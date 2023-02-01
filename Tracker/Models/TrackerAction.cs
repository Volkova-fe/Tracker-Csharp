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
        public DateOnly date { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public TimeOnly time { get; set; }
    }
}
