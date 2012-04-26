using System.Collections.ObjectModel;
using Canucks.NewsReader.Common.Model;
using Canucks.NewsReader.Phone.Helpers;

namespace Canucks.NewsReader.Phone.Services.Contracts
{
    public interface IFinalScoreService
    {
        event LoadEventHandler FinalScoresLoaded;

        ObservableCollection<CompletedViewSchedule> GetFinalScores(string url, string pageStart, string pageSize);
    }

    public interface IPlayoffFinalScoreService
    {
        event LoadEventHandler FinalScoresLoaded;

        ObservableCollection<CompletedViewSchedule> GetFinalScores(string url, string pageStart, string pageSize);
    }
}