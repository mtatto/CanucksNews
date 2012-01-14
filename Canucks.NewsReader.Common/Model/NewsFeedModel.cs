using System.Runtime.Serialization;
using Canucks.NewsReader.Common.Helpers;

namespace Canucks.NewsReader.Common.Model
{
    [DataContract]
    public class NewsFeedModel : ModelBase
    {
        private string _copyright;
        private string _description;
        private string _imageUrl;
        private ObservableCollectionEx<NewsItemModel> _items = new ObservableCollectionEx<NewsItemModel>();
        private string _language;
        private string _link;
        private string _title;

        [DataMember]
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title == value)
                    return;
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }

        [DataMember]
        public string Link
        {
            get { return _link; }
            set
            {
                if (_link == value)
                    return;
                _link = value;
                NotifyPropertyChanged("Link");
            }
        }

        [DataMember]
        public string Description
        {
            get { return _description; }
            set
            {
                if (value != _description)
                {
                    _description = value;
                    NotifyPropertyChanged("Description");
                }
            }
        }

        [DataMember]
        public string Language
        {
            get { return _language; }
            set
            {
                if (value != _language)
                {
                    _language = value;
                    NotifyPropertyChanged("Language");
                }
            }
        }

        [DataMember]
        public string Copyright
        {
            get { return _copyright; }
            set
            {
                if (value != _copyright)
                {
                    _copyright = value;
                    NotifyPropertyChanged("Copyright");
                }
            }
        }

        [DataMember]
        public string ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                if (_imageUrl == value)
                    return;
                _imageUrl = value;
                NotifyPropertyChanged("ImageUrl");
            }
        }

        [DataMember]
        public ObservableCollectionEx<NewsItemModel> Items
        {
            get { return _items; }
            set
            {
                if (value != _items)
                {
                    _items = value;
                    NotifyPropertyChanged("Items");
                }
            }
        }
    }
}