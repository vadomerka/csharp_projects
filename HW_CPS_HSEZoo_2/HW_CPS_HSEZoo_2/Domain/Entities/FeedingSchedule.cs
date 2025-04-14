namespace HW_CPS_HSEZoo_2.Domain.Entities
{
    internal class FeedingSchedule
    {
        private Animal _animal;
        private DateTime _nextFeed;
        private TimeSpan _untillNext;

        public FeedingSchedule(Animal animal, DateTime firstFeed, TimeSpan untillNext) { 
            _animal = animal;
            _nextFeed = firstFeed;
            _untillNext = untillNext;
        }

        void GetNextFeed() { 
        
        }
    }
}
