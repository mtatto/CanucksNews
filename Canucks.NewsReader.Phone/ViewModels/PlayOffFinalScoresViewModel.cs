using System.Collections.ObjectModel;
using Canucks.NewsReader.Common;
using Canucks.NewsReader.Common.Model;
using Canucks.NewsReader.Phone.Helpers;
using Canucks.NewsReader.Phone.Services;
using Canucks.NewsReader.Phone.Services.Contracts;

namespace Canucks.NewsReader.Phone.ViewModels
{
    public class PlayOffFinalScoresViewModel : ViewModelBase
    {
        private IFinalScoreService _finalScoreService;

        public PlayOffFinalScoresViewModel()
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

        private void GetPlayOffFinalScores()
        {
            GlobalLoading.Instance.IsLoading = true;
            FinalScores = FinalScoreService.GetFinalScores(Settings.FinalScoresPlayoff, "1", "120");
        }

        public void GetPlayOffFinalScores(string currentPage)
        {
            GlobalLoading.Instance.IsLoading = true;
            FinalScores = FinalScoreService.GetFinalScores(Settings.FinalScoresPlayoff, "1", "120");
        }

        public void LoadData()
        {
            GetPlayOffFinalScores();
        }
    }
}