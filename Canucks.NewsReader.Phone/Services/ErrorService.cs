using System;
using System.Text;
using System.Windows;
using Canucks.NewsReader.Common.Model;
using Canucks.NewsReader.Phone.Services.Contracts;

namespace Canucks.NewsReader.Phone.Services
{
    public class ErrorService
    {
        private readonly string _friendlyError;

        private readonly string _title;
        private Exception _error;

        private INavigationService _navigationService;
        private bool _showDialog;

        public ErrorService(string friendlyError, string title)
        {
            _friendlyError = friendlyError;
            _title = title;

            if (string.IsNullOrWhiteSpace(_title))
            {
                _title = @"Something bad happened...:(";
            }
        }

        private INavigationService NavigationService
        {
            get { return _navigationService ?? (_navigationService = new SimpleNavigationService()); }
        }

        public ErrorService IncludeException(Exception ex)
        {
            _error = ex;
            return this;
        }

        public ErrorService ErrorDialog(bool showDialog)
        {
            _showDialog = showDialog;
            return this;
        }

        public ErrorService Log()
        {
            throw new NotImplementedException();
        }

        public void HandleError()
        {
            if (_showDialog)
            {
                MessageBox.Show(BuildMessage());
            }
            else
            {
                SetErrorState();
                NavigationService.Navigate("/Error.xaml");
            }
        }

        private string BuildMessage()
        {
            var builder = new StringBuilder();
            builder.AppendLine(_title);
            builder.AppendLine("");
            builder.AppendLine(_friendlyError);
            return builder.ToString();
        }

        private void SetErrorState()
        {
            App.CurrentError = new ErrorMessage
                                   {
                                       Error = _error,
                                       FriendlyError = _friendlyError,
                                       Title = _title
                                   };
        }
    }
}