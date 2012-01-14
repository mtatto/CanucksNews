using System;

namespace Canucks.NewsReader.Common.Model
{
    public class NewsFeatureItem : ModelBase
    {
        private const string TitleProp = "Title";
        private const string ArticleLinkProp = "ArticleLink";
        private const string DateProp = "Date";
        private const string DescriptionProp = "Description";
        private const string ImageUrlProp = "ImageUrl";

        private string _articleLink;
        private DateTime _date;
        private string _description;
        private string _imageUrl;
        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    NotifyPropertyChanged(TitleProp);
                }
            }
        }

        public string ArticleLink
        {
            get { return _articleLink; }
            set
            {
                if (_articleLink != value)
                {
                    _articleLink = value;
                    NotifyPropertyChanged(ArticleLinkProp);
                }
            }
        }

        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (_date != value)
                {
                    _date = value;
                    NotifyPropertyChanged(DateProp);
                }
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    NotifyPropertyChanged(DescriptionProp);
                }
            }
        }

        public string ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                if (_imageUrl != value)
                {
                    _imageUrl = value;
                    NotifyPropertyChanged(ImageUrlProp);
                }
            }
        }
    }
}