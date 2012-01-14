namespace Canucks.NewsReader.Common.Model
{
    public class TwitterStatusModel : ModelBase
    {
        private const string TwtLink = "TwitterLink";
        private string _date;
        private string _text;
        private string _twitterLink;

        public string Date
        {
            get { return _date; }
            set
            {
                if (_date == value)
                    return;
                _date = value;
                NotifyPropertyChanged("Date");
            }
        }

        public string TwitterLink
        {
            get { return _twitterLink; }
            set
            {
                if (_twitterLink != value)
                {
                    _twitterLink = value;
                    NotifyPropertyChanged(TwtLink);
                }
            }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                if (value != _text)
                {
                    _text = value;
                    NotifyPropertyChanged("Text");
                }
            }
        }
    }
}