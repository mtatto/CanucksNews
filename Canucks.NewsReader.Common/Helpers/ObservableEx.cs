using System.Collections.Generic;
using Microsoft.Phone.Reactive;

namespace System.Linq
{
    public static class ObservableEx
    {
        public static IObservable<TSource> OnTimeline<TSource>(this IObservable<TSource> source, TimeSpan period)
        {
            return source.Zip(Observable.Interval(period), (d, t) => d);
        }
    }
}