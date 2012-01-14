using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using Canucks.NewsReader.Common.Helpers;

namespace Canucks.NewsReader.Common.Model
{
    [DataContract]
    public class ModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// A static set of argument instances, one per property name.
        /// </summary>
        private static readonly Dictionary<string, PropertyChangedEventArgs> _argumentInstances =
            new Dictionary<string, PropertyChangedEventArgs>();

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        /// <summary>
        /// Notify any listeners that the property value has changed.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException("PropertyName cannot be empty or null.");
            }

            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChangedEventArgs args;
                if (!_argumentInstances.TryGetValue(propertyName, out args))
                {
                    args = new PropertyChangedEventArgs(propertyName);
                    _argumentInstances[propertyName] = args;
                }

                // Fire the change event. The smart dispatcher will directly
                // invoke the handler if this change happened on the UI thread,
                // otherwise it is sent to the proper dispatcher.
                SmartDispatcher.BeginInvoke(() => handler(this, args));
            }
        }
    }
}