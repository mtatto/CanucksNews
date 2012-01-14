namespace Canucks.NewsReader.Common.Model
{
    public class ScheduleViewBase : ModelBase
    {
        private const string Dt = "GameDateTime";
        private const string PlayingTeams = "Teams";
        private string _gameDateTime;
        private string _teams;

        public string GameDateTime
        {
            get { return _gameDateTime; }
            set
            {
                if (_gameDateTime != value)
                {
                    _gameDateTime = value;
                    NotifyPropertyChanged(Dt);
                }
            }
        }

        public string Teams
        {
            get { return _teams; }
            set
            {
                if (_teams != value)
                {
                    _teams = value;
                    NotifyPropertyChanged(PlayingTeams);
                }
            }
        }
    }
}