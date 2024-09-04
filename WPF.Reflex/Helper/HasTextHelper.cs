using System.Windows;

namespace WPF.Reflex.Helper
{
    public class HasTextHelper
    {
        public static bool GetHasText(DependencyObject obj)
        {
            return (bool)obj.GetValue(HasTextProperty);
        }

        public static void SetHasText(DependencyObject obj, bool value)
        {
            obj.SetValue(HasTextProperty, value);
        }

        public static readonly DependencyProperty HasTextProperty =
            DependencyProperty.RegisterAttached(
                "HasText",
                typeof(bool),
                typeof(HasTextHelper),
                new PropertyMetadata(0)
            );
    }
}
