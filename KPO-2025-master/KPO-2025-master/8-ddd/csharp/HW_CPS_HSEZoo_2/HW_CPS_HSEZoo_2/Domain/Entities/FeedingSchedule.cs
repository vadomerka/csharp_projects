using System.Windows.Input;

namespace HW_CPS_HSEZoo_2.Domain.Entities
{
    internal class FeedingSchedule : ICommand
    {
        private Animal _animal;
        private DateTime _feedTime;
        private string _foodType;

        public event EventHandler? CanExecuteChanged;

        public FeedingSchedule(Animal animal, DateTime feedTime, string foodType) { 
            _animal = animal;
            _feedTime = feedTime;
            _foodType = foodType;
        }

        public void ChangeSchedule(DateTime feedTime, TimeSpan untillNext) {
            if (feedTime >= DateTime.Now)
            {
                _feedTime = feedTime;
            }
        }

        public void Cancel() { 
            // ??.
        }

        public bool CanExecute(object? parameter)
        {
            return _feedTime >= DateTime.Now;
        }

        public void Execute(object? parameter)
        {
            _animal.Feed(_foodType);
        }
    }
}
