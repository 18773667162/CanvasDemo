using CanvasDemo.Core.Settings;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CanvasDemo.Handler;

public class CanvasScaleEventHandler : EventHandler, IEventHandler
{
    private readonly ICommonSetting _commonSetting;

    public CanvasScaleEventHandler(ICommonSetting commonSetting) : base(commonSetting)
    {
        _commonSetting = commonSetting;
    }

    public void PreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
        if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
        {
            var mousePosition = e.GetPosition(_commonSetting.DrawingCanvas);
            double scaleFactor = e.Delta > 0 ? 1.1 : 0.9;
            var widgetCanvas = _commonSetting.WidgetsCanvas.Element.RenderTransform as MatrixTransform ?? throw new InvalidCastException("no MatrixTransform");
            var oldTransformedPoint = _commonSetting.WidgetsCanvas.Element.TransformToAncestor(_commonSetting.DrawingCanvas).Transform(new Point(0, 0));
            var oldDistanceX = _commonSetting.Selector.Left - oldTransformedPoint.X;
            var oldDistanceY = _commonSetting.Selector.Top - oldTransformedPoint.Y;
            var t = widgetCanvas.Matrix;
            t.ScaleAt(scaleFactor, scaleFactor, mousePosition.X, mousePosition.Y);
            widgetCanvas.Matrix = t;
            var transformedPoint = _commonSetting.WidgetsCanvas.Element.TransformToAncestor(_commonSetting.DrawingCanvas).Transform(new Point(0, 0));
            _commonSetting.Selector.Left = transformedPoint.X + oldDistanceX * scaleFactor;
            _commonSetting.Selector.Top = transformedPoint.Y + oldDistanceY * scaleFactor;
            _commonSetting.Selector.Width *= scaleFactor;
            _commonSetting.Selector.Height *= scaleFactor;
            _commonSetting.WidgetsCanvas.Left = transformedPoint.X;
            _commonSetting.WidgetsCanvas.Top = transformedPoint.Y;
            _commonSetting.WidgetsCanvas.Width *= scaleFactor;
            _commonSetting.WidgetsCanvas.Height *= scaleFactor;
            _commonSetting.Selector.Render();
            _commonSetting.WidgetsCanvas.Element.RenderTransform = widgetCanvas;
            e.Handled = true;
        }
    }
}