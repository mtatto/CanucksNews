using System.Windows.Navigation;
using Canucks.NewsReader.Phone.ViewModels;
using Microsoft.Phone.Controls;

namespace Canucks.NewsReader.Phone.Views
{
    public partial class Schedule : PhoneApplicationPage
    {
        public Schedule()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ScheduleViewModel viewModel = App.ScheduleViewModel;
            DataContext = viewModel;
        }
    }
}