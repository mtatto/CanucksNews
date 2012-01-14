using System;
using System.Runtime.Serialization;

namespace Canucks.NewsReader.Common.Model
{
    [DataContract]
    public class NewsItemModel : ModelBase
    {
        private string _articleLink;
        private DateTime _date;
        private string _description;
        private string _imgUrl;
        private string _title;

        [DataMember]
        public string Title
        {
            get { return _title; }

            set
            {
                if (_title == value)
                {
                    return;
                }
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }

        [DataMember]
        public string ArticleLink
        {
            get { return _articleLink; }
            set
            {
                if (_articleLink == value)
                {
                    return;
                }
                _articleLink = value;
                NotifyPropertyChanged(("ArticleLink"));
            }
        }

        [DataMember]
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (value != _date)
                {
                    _date = value;
                    NotifyPropertyChanged("Date");
                }
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
        public string ImageUrl
        {
            get { return _imgUrl; }

            set
            {
                if (value != _imgUrl)
                {
                    _imgUrl = value;
                    NotifyPropertyChanged("ImageUrl");
                }
            }
        }
    }
}