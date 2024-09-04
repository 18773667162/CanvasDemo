using CanvasDemo.Common;
using CanvasDemo.Core.Settings;
using CanvasDemo.Core.StateManager;
using CanvasDemo.Operations;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace CanvasDemo.Handler
{
    public class SelectorScaleEventHandler
        : EventHandler, IEventHandler
    {
        private readonly ScaleOperation _operation;
        private readonly ICommonSetting _commonSetting;
        private readonly IUndoRedoManager _undoRedoManager;

        public SelectorScaleEventHandler(ICommonSetting eventHandlerParam, IUndoRedoManager undoRedoManager) : base(eventHandlerParam)
        {
            _commonSetting = eventHandlerParam;
            _operation = OperationManager.GetOperation(LayerOperationTypeEnum.Stretch) as ScaleOperation ?? throw new InvalidCastException("can't cast vaild obj");
            _undoRedoManager = undoRedoManager;
        }

        public override void HandleMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                base.HandleMouseDown(sender, e);
                var rect = e.Source as Rectangle ?? throw new InvalidCastException("invalid cast rectangle");
                var directionNum = Convert.ToInt32(rect.Tag);
                var direction = (StretchDirectionTypeEnum)directionNum;
                var point = new Point(_commonSetting.Selector.Left, _commonSetting.Selector.Top);
                _operation.BeforeExecute(new BeforeOperationParam(null, point, direction));
                _operation.SetProportion(_commonSetting);
            }
            e.Handled = true;
        }

        public override void HandleMouseMove(object sender, MouseEventArgs e)
        {
            if (_operation.Actioning && e.LeftButton == MouseButtonState.Pressed)
            {
                var currentPoint = e.GetPosition(_commonSetting.DrawingCanvas);
                var delta = currentPoint - _startPoint;
                _operation.Execute(new OperationParam(_commonSetting.Selector, _commonSetting.Selector, delta, _startPoint, currentPoint));
                _operation.BatchScale(_commonSetting);
                _startPoint = currentPoint;
                e.Handled = true;
            }
        }

        public override void HandleMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                base.HandleMouseUp(sender, e);
                var data = JsonSerializer.Serialize(_commonSetting.Layers.Select(s => s.ToLayerEntity()).ToList());
                var operation = new OperationState(data, ActionTypeEnum.Stretch);
                _undoRedoManager.ExecuteOperation(operation);
                _operation.AfterExecute(new AfterOperationParam(_commonSetting.Selector));
            }
        }
    }
}