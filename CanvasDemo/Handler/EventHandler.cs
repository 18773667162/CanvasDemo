using CanvasDemo.Core.Settings;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CanvasDemo.Handler
{
    public abstract class EventHandler
    {
        private readonly ICommonSetting _commonSetting;

        protected Point _startPoint { get; set; }

        public EventHandler(ICommonSetting commonSetting)
        {
            _commonSetting = commonSetting;
        }

        public virtual void HandleMouseDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = Mouse.GetPosition(_commonSetting.DrawingCanvas);
            Mouse.Capture(sender as FrameworkElement);
            e.Handled = true;
        }

        public virtual void HandleMouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
            e.Handled = true;
        }

        public virtual void HandleMouseMove(object sender, MouseEventArgs e)
        {
            e.Handled = true;
        }

        public virtual void KeyDown(object sender, KeyEventArgs e)
        { }

        public virtual void KeyUp(object sender, KeyEventArgs e)
        { }
    }
}