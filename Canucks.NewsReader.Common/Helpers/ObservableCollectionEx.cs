using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Canucks.NewsReader.Common.Helpers
{
    public class ObservableCollectionEx<T> : ObservableCollection<T>
    {
        private bool _suspendCollectionChangeNotification;

        public ObservableCollectionEx()
        {
            _suspendCollectionChangeNotification = false;
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (!_suspendCollectionChangeNotification)
            {
                base.OnCollectionChanged(e);
            }
        }

        public void SuspendCollectionChangeNotification()
        {
            _suspendCollectionChangeNotification = true;
        }

        public void ResumeCollectionChangeNotification()
        {
            _suspendCollectionChangeNotification = false;
        }


        public void AddRange(IEnumerable<T> items)
        {
            SuspendCollectionChangeNotification();
            int count = base.Count;
            try
            {
                foreach (var i in items)
                {
                    base.InsertItem(base.Count, i);
                }
            }
            finally
            {
                ResumeCollectionChangeNotification();
                var arg = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
                OnCollectionChanged(arg);
            }
        }
    }
}