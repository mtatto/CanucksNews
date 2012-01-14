using System;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Media.Animation;

namespace Canucks.NewsReader.Phone.Helpers
{
    public class LoadedBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            CreateStoryboard().Begin();
        }

        private Storyboard CreateStoryboard()
        {
            var sb = new Storyboard();
            var animation = new DoubleAnimation
                                {
                                    Duration = TimeSpan.FromMilliseconds(1000),
                                    To = 0,
                                    EasingFunction =
                                        new CubicEase {EasingMode = EasingMode.EaseOut}
                                };
            Storyboard.SetTargetProperty(animation,
                                         new PropertyPath("(UIElement.Projection).(PlaneProjection.RotationY)"));

            sb.Children.Add(animation);

            animation = new DoubleAnimation
                            {
                                Duration = TimeSpan.FromMilliseconds(1000),
                                To = 1,
                                EasingFunction =
                                    new CubicEase {EasingMode = EasingMode.EaseOut}
                            };
            Storyboard.SetTargetProperty(animation, new PropertyPath(UIElement.OpacityProperty));

            sb.Children.Add(animation);
            Storyboard.SetTarget(sb, AssociatedObject);
            return sb;
        }
    }
}