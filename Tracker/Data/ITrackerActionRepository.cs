using Tracker.Models;

namespace Tracker.Data
{
    public interface ITrackerActionRepository
    {
        TrackerAction Create(TrackerAction trackerAction);
        TrackerAction GetByDate(string date);
    }
}
