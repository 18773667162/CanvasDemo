using CanvasDemo.Common;
using CanvasDemo.Core.Settings;
using CanvasDemo.Core.StateManager;
using CanvasDemo.Operations;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;

namespace CanvasDemo.Handler
{
    public class SelectorRoateEventHandler(ICommonSetting eventHandlerParam, IUndoRedoManager undoRedoManager)
                : EventHandler(eventHandlerParam), IEventHandler
    {
        private readonly Operation _operation = OperationManager.GetOperation(LayerOperationTypeEnum.Rotate);
        private readonly ICommonSetting _setting = eventHandlerParam;
        private readonly IUndoRedoManager _undoRedoManager = undoRedoManager;
        private Point _widgetStartPoint;

        public override void HandleMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                base.HandleMouseDown(sender, e);
                _widgetStartPoint = e.GetPosition(_setting.WidgetsCanvas.Element);
                _operation.BeforeExecute(new BeforeOperationParam(_setting.Selector, new Point(_setting.Selector.Width / 2, _setting.Selector.Height / 2), StretchDirectionTypeEnum.None));
            }
        }

        public override void HandleMouseMove(object sender, MouseEventArgs e)
        {
            if (_operation.Actioning && e.LeftButton == MouseButtonState.Pressed)
            {
                // 指定对应的逻辑
                var currentPoint = e.GetPosition(_setting.DrawingCanvas);
                var widgetCurrentPoint = e.GetPosition(_setting.WidgetsCanvas.Element);
                var d = currentPoint - _startPoint;
                _operation.Execute(new OperationParam(_setting.Selector, _setting.Selector, d, _startPoint, currentPoint));
                foreach (var layer in _setting.Layers)
                {
                    if (!layer.IsSelected) continue;
                    _operation.Execute(new OperationParam(layer, _setting.Selector, d, _widgetStartPoint, widgetCurrentPoint));
                }
                _startPoint = currentPoint;
                _widgetStartPoint = widgetCurrentPoint;
            }
        }

        public override void HandleMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                base.HandleMouseUp(sender, e);
                var data = JsonSerializer.Serialize(_setting.Layers.Select(s => s.ToLayerEntity()).ToList());
                var operation = new OperationState(data, ActionTypeEnum.Rotate);
                _undoRedoManager.ExecuteOperation(operation);
                _operation.AfterExecute(new AfterOperationParam(_setting.Selector));
            }
        }

    }
}