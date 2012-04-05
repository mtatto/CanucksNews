using System.Collections.ObjectModel;
using Canucks.NewsReader.Common;
using Canucks.NewsReader.Common.Model;
using Canucks.NewsReader.Phone.Helpers;
using Canucks.NewsReader.Phone.Services;
using Canucks.NewsReader.Phone.Services.Contracts;

namespace Canucks.NewsReader.Phone.ViewModels
{
    public class FinalScoresViewModel : ViewModelBase
    {
        private IFinalScoreService _finalScoreService;

        public FinalScoresViewModel()
        {
            Refresh();
        }

        private IFinalScoreService FinalScoreService
        {
            get { return _finalScoreService ?? (_finalScoreService = new FinalScoreService()); }
        }

        public ObservableCollection<CompletedViewSchedule> FinalScores { get; private set; }


        public void Refresh()
        {
            FinalScoreService.FinalScoresLoaded += ServiceFinalScoresLoaded;
            LoadData();
        }

        private static void ServiceFinalScoresLoaded(object sender, LoadEventArgs e)
        {
            GlobalLoading.Instance.IsLoading = !e.IsLoaded;
        }

        private void GetFinalScores()
        {
            GlobalLoading.Instance.IsLoading = true;
            FinalScores = FinalScoreService.GetFinalScores(Settings.FinalScores,"1", "120");
        }

        public void GetFinalScores(string currentPage)
        {
            GlobalLoading.Instance.IsLoading = true;
            FinalScores = FinalScoreService.GetFinalScores(Settings.FinalScores, "1", "120");
        }

        public void LoadData()
        {
            GetFinalScores();
        }
    }
}