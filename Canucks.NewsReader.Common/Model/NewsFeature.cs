using System.Collections.ObjectModel;

namespace Canucks.NewsReader.Common.Model
{
    public class NewsFeature : ModelBase
    {
        private const string TitleProp = "Title";
        private const string LinkProp = "Link";
        private const string DescriptionProp = "Description";
        private const string CopyrightProp = "Copyright";
        private const string LanguageProp = "Language";
        private const string FeatureItemsProp = "FeatureItems";
        private string _copyright;
        private string _description;
        private ObservableCollection<NewsFeatureItem> _featureItems;
        private string _language;
        private string _link;
        private string _title;

        public NewsFeature()
        {
            _featureItems = new ObservableCollection<NewsFeatureItem>();
        }

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


        public string Link
        {
            get { return _link; }
            set
            {
                if (_link != value)
                {
                    _link = value;
                    NotifyPropertyChanged(LinkProp);
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


        public string Copyright
        {
            get { return _copyright; }
            set
            {
                if (_copyright != value)
                {
                    _copyright = value;
                    NotifyPropertyChanged(CopyrightProp);
                }
            }
        }


        public string Language
        {
            get { return _language; }
            set
            {
                if (_language != value)
                {
                    _language = value;
                    NotifyPropertyChanged(LanguageProp);
                }
            }
        }


        public ObservableCollection<NewsFeatureItem> FeatureItems
        {
            get { return _featureItems; }
            set
            {
                if (_featureItems != value)
                {
                    _featureItems = value;
                    NotifyPropertyChanged(FeatureItemsProp);
                }
            }
        }
    }
}