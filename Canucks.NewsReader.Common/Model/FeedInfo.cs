namespace Canucks.NewsReader.Common.Model
{
    public class FeedInfo : ModelBase
    {
        private string _feedName;
        private int _id;

        private string _key;

        public int Id
        {
            get { return _id; }
            set
            {
                if (_id == value)
                {
                    return;
                }
                _id = value;
                NotifyPropertyChanged(("Id"));
            }
        }

        public string Key
        {
            get { return _key; }
            set
            {
                if (_key == value)
                {
                    return;
                }
                _key = value;
                NotifyPropertyChanged("Key");
            }
        }

        public string FeedName
        {
            get { return _feedName; }
            set
            {
                if (_feedName == value)
                {
                    return;
                }
                _feedName = value;
                NotifyPropertyChanged("FeedName");
            }
        }
    }
}