using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Canucks.NewsReader.Phone.Services.Contracts;
using Microsoft.Phone.Controls;

namespace Canucks.NewsReader.Phone.Services
{
    public class SimpleNavigationService : INavigationService
    {
        #region INavigationService Members

        public void Navigate(string uri)
        {
            Navigate(uri, null);
        }

        public void Navigate(string uri, IDictionary<string, string> parameters)
        {
            var frame = Application.Current.RootVisual as PhoneApplicationFrame;

            if (frame != null)
            {
                RootFrameNavigate(frame, CreateUri(uri, parameters));
            }
            else
            {
                PhoneApplicationFrame rootFrame = ((App) Application.Current).RootFrame;

                if (rootFrame != null)
                {
                    RootVisualNavigate(rootFrame, uri);
                }
            }
        }

        #endregion

        private static void RootFrameNavigate(PhoneApplicationFrame frame, string uri)
        {
            frame.Navigate(new Uri(uri, UriKind.RelativeOrAbsolute));
        }

        private static void RootVisualNavigate(PhoneApplicationFrame frame, string uri)
        {
            frame.Navigate(new Uri(uri, UriKind.RelativeOrAbsolute));
        }

        private static string CreateUri(string uri, IDictionary<string, string> parameters)
        {
            var uriBuilder = new StringBuilder();
            uriBuilder.Append(uri);
            if (parameters != null && parameters.Count > 0)
            {
                uriBuilder.Append("?");
                bool prependAmp = false;
                foreach (var pair in parameters)
                {
                    if (prependAmp)
                    {
                        uriBuilder.Append("&");
                    }
                    uriBuilder.AppendFormat("{0}={1}", pair.Key, pair.Value);
                    prependAmp = true;
                }
            }

            uri = uriBuilder.ToString();
            return uri;
        }
    }
}