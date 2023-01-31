using Tracker.Models;

namespace Tracker.Data
{
    public interface ITrackerActionRepository
    {
        TrackerAction Create(TrackerAction trackerAction);
    }
}
