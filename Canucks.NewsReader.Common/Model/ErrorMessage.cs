using System;

namespace Canucks.NewsReader.Common.Model
{
    public class ErrorMessage
    {
        public Exception Error { get; set; }

        public string FriendlyError { get; set; }

        public string Title { get; set; }
    }
}
