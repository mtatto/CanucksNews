using System;

namespace Canucks.NewsReader.Phone.Helpers
{
    public delegate void LoadEventHandler(object sender, LoadEventArgs e);

    public class LoadEventArgs : EventArgs
    {
        public LoadEventArgs()
        {
        }

        public LoadEventArgs(bool isLoaded, string message)
        {
            IsLoaded = isLoaded;
            Message = message;
        }

        public bool IsLoaded { get; set; }
        public string Message { get; set; }
    }
}