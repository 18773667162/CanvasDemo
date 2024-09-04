using CanvasDemo.Common;
using CanvasDemo.Core;
using CanvasDemo.Core.Settings;
using CanvasDemo.Operations;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CanvasDemo.Handler
{
    public class CanvasSelectionEventHandler : EventHandler, IEventHandler
    {
        private readonly Operation _operation;
        private readonly ICommonSetting _commonSetting;
        private readonly ISelectionManager _selectionManager;

        public CanvasSelectionEventHandler(ICommonSetting eventHandlerParam, ISelectionManager selectionManager) : base(eventHandlerParam)
        {
            _commonSetting = eventHandlerParam;
            _operation = OperationManager.GetOperation(LayerOperationTypeEnum.Selection);
            _selectionManager = selectionManager;
        }

        public override void HandleMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                var isClickRect = Uitl.IsExistElementUnderMouse(_commonSetting.Layers, e.GetPosition(_commonSetting.WidgetsCanvas.Element));
                if (isClickRect != null) return;
                _startPoint = Mouse.GetPosition(_commonSetting.DrawingCanvas);
                _selectionManager.CancelSelection();
                _operation.BeforeExecute(new BeforeOperationParam(_commonSetting.BoxSelector, _startPoint, StretchDirectionTypeEnum.None));
                Mouse.Capture(sender as FrameworkElement);
                e.Handled = true;
            }
        }

        public override void HandleMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Mouse.Capture(null);
                var currentPoint = e.GetPosition(_commonSetting.DrawingCanvas);
                var _boxSelector = _commonSetting.BoxSelector;
                if (!_boxSelector.IsVisiable) return;
                var rect = new RectPoints()
                {
                    LeftTop = _boxSelector.LeftTop,
                    TopRight = _boxSelector.RightTop,
                    BottomLeft = _boxSelector.LeftBottom,
                    BottomRight = _boxSelector.RightBottom,
                    Angle = 0.0
                };
                var matrixTransform = _commonSetting.WidgetsCanvas.Element.RenderTransform as MatrixTransform ?? throw new NullReferenceException("no matrixTransform");
                var temp = matrixTransform.Matrix;
                var selectionRes = new List<Layer>();
                var widgetCanvas = _commonSetting.WidgetsCanvas;
                foreach (var item in _commonSetting.Layers)
                {
                    var isRange = GraphicsUitl.CheckIsRange(rect, new RectPoints
                    {
                        LeftTop = new Point(item.Left * temp.M11 + widgetCanvas.Left, item.Top * temp.M22 + widgetCanvas.Top),
                        TopRight = new Point(item.Left * temp.M11 + item.Width * temp.M11 + widgetCanvas.Left, item.Top * temp.M22 + widgetCanvas.Top),
                        BottomLeft = new Point(item.Left * temp.M11 + widgetCanvas.Left, item.Top * temp.M22 + item.Height * temp.M22 + widgetCanvas.Top),
                        BottomRight = new Point(item.Left * temp.M11 + item.Width * temp.M11 + widgetCanvas.Left, item.Top * temp.M22 + item.Height * temp.M22 + widgetCanvas.Top),
                        Angle = item.Angle
                    });
                    if (isRange) selectionRes.Add(item);
                }
                if (selectionRes.Count > 0)
                {
                    _selectionManager.SelectionMultiple(selectionRes.Select(s => s.Id).ToList());
                }
                _operation.AfterExecute(new AfterOperationParam(_boxSelector));
                e.Handled = true;
            }
        }

        public override void HandleMouseMove(object sender, MouseEventArgs e)
        {
            if (_operation.Actioning && e.LeftButton == MouseButtonState.Pressed)
            {
                // 指定对应的逻辑
                var currentPoint = e.GetPosition(_commonSetting.DrawingCanvas);
                var delta = currentPoint - _startPoint;
                var _boxSelector = _commonSetting.BoxSelector;
                _operation.Execute(new OperationParam(_boxSelector, _commonSetting.Selector, delta, _startPoint, currentPoint));
                e.Handled = true;
            }
        }
    }
}
