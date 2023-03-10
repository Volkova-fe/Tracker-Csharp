using Tracker.Models;

namespace Tracker.Data
{
    public interface ITrackerActionRepository
    {
        TrackerAction Create(TrackerAction trackerAction);
        IEnumerable<TrackerAction> GetByDate(DateTime date);
        IEnumerable<TrackerAction> GetByDateFromTo(DateTime from, DateTime to);
    }
}
