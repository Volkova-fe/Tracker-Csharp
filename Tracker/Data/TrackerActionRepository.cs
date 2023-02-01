using Tracker.Models;

namespace Tracker.Data
{
    public class TrackerActionRepository : ITrackerActionRepository
    {
        private readonly DataBaseContext _context;

        public TrackerActionRepository(DataBaseContext context)
        {
            _context = context;
        }

        public TrackerAction Create(TrackerAction trackerAction)
        {
            _context.Trackers.Add(trackerAction);
            _context.SaveChanges();
            return trackerAction;
        }
        public TrackerAction GetByDate(string date)
        {

            return (TrackerAction)_context.Trackers
        }
    }
}
