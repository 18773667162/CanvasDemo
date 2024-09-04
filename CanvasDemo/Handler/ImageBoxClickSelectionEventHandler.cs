using CanvasDemo.Core;
using CanvasDemo.Core.Settings;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CanvasDemo.Handler
{
    public class ImageBoxClickSelectionEventHandler : EventHandler, IEventHandler
    {
        private readonly ICommonSetting _commonSetting;
        private readonly ISelectionManager _selectionManager;

        public ImageBoxClickSelectionEventHandler(
            ICommonSetting commonSetting, 
            ISelectionManager selectionManager
            ) : base(commonSetting)
        {
            _commonSetting = commonSetting;
            _selectionManager = selectionManager;
        }

        public override void HandleMouseDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = Mouse.GetPosition(_commonSetting.DrawingCanvas);
            Mouse.Capture(sender as FrameworkElement);
            var element = sender as FrameworkElement ?? throw new InvalidCastException("can't cast FrameworkElement");
            var id = element.Tag?.ToString() ?? throw new NullReferenceException("no bind element");
            var selectedLayer = _commonSetting.Layers.First(x => x.Id == id);
            var rotate = _commonSetting.Selector.Element.RenderTransform as RotateTransform ?? throw new NullReferenceException("selector rotateTransform can't not be null");
            rotate.Angle = selectedLayer.Angle;
            _selectionManager.CancelSelection();
            _selectionManager.SelectionSingle(id);
            Mouse.Capture(null);
        }
    }
}