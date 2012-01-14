namespace Canucks.NewsReader.Common.Model
{
    public class CompletedSchedule : ScheduleBase
    {
        private const string FScores = "FinalScores";
        private string _finalScores;

        public string FinalScores
        {
            get { return _finalScores; }
            set
            {
                if (_finalScores != value)
                {
                    _finalScores = value;
                    NotifyPropertyChanged(FScores);
                }
            }
        }
    }
}