using System.Windows.Navigation;
using Canucks.NewsReader.Phone.ViewModels;
using Microsoft.Phone.Controls;

namespace Canucks.NewsReader.Phone
{
    public partial class FinalScores : PhoneApplicationPage
    {
        public FinalScores()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            FinalScoresViewModel viewModel = App.FinalScoresViewModel;
            DataContext = viewModel;
        }
    }
}