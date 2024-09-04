using CanvasDemo.Common;
using CanvasDemo.Core;
using CanvasDemo.Core.Settings;
using CanvasDemo.Core.StateManager;
using CanvasDemo.Operations;
using CanvasDemo.Widgets.SelectorWidget;
using System.Diagnostics;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CanvasDemo.Handler
{
    public class SelectorMoveEventHandler : EventHandler, IEventHandler
    {
        private readonly Operation _operation;
        private readonly ICommonSetting _commonSetting;
        private readonly IUndoRedoManager _undoRedoManager;
        private readonly ISelectionManager _selectionManager;
        private bool _isMove = false;

        public SelectorMoveEventHandler(ICommonSetting eventHandlerParam, IUndoRedoManager undoRedoManager, ISelectionManager selectionManager) : base(eventHandlerParam)
        {
            _operation = OperationManager.GetOperation(LayerOperationTypeEnum.Move);
            _commonSetting = eventHandlerParam;
            _undoRedoManager = undoRedoManager;
            _selectionManager = selectionManager;
        }

        public override void HandleMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left) return;

            var mousePosition = e.GetPosition(_commonSetting.DrawingCanvas);
            var fisrtId = Uitl.IsExistElementUnderMouse(_commonSetting.Layers, e.GetPosition(_commonSetting.WidgetsCanvas.Element));
            // 检查是否点击到了小矩形
            if (fisrtId != null)
            {
                base.HandleMouseDown(sender, e);
                var data = JsonSerializer.Serialize(_commonSetting.Layers.Select(s => s.ToLayerEntity()).ToList());
                var operation = new OperationState(data, ActionTypeEnum.Move);
                _undoRedoManager.ExecuteOperation(operation);
                var selectedCount = _commonSetting.Layers.Count(c => c.IsSelected);
                if (selectedCount <= 1)
                {
                    _selectionManager.CancelSelection();
                    _selectionManager.SelectionSingle(fisrtId);
                }
                _operation.BeforeExecute(new BeforeOperationParam(null, new Point(0, 0), StretchDirectionTypeEnum.None));
            }
            else
            {
                _selectionManager.CancelSelection();
            }
            // e.Handled = true;
        }

        public override void HandleMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_isMove && e.ChangedButton == MouseButton.Left && e.LeftButton == MouseButtonState.Released)
            {
                base.HandleMouseUp(sender, e);
                _operation.AfterExecute(new AfterOperationParam(_commonSetting.Selector));
                _isMove = false;
            }
        }

        public override void HandleMouseMove(object sender, MouseEventArgs e)
        {
            if (_operation.Actioning && e.LeftButton == MouseButtonState.Pressed)
            {
                var currentPoint = e.GetPosition(_commonSetting.DrawingCanvas);
                var delta = currentPoint - _startPoint;
                var matrixTransform = _commonSetting.WidgetsCanvas.Element.RenderTransform as MatrixTransform ?? throw new NullReferenceException("no matrixTransform");
                var temp = matrixTransform.Matrix;
                temp.Invert();
                _operation.Execute(new OperationParam(_commonSetting.Selector, _commonSetting.Selector, delta, _startPoint, currentPoint));
                delta.X *= temp.M11;
                delta.Y *= temp.M22;
                foreach (var layer in _commonSetting.Layers)
                {
                    if (!layer.IsSelected) continue;
                    _operation.Execute(new OperationParam(layer, _commonSetting.Selector, delta, _startPoint, currentPoint));
                }
                _startPoint = currentPoint;
                _isMove = true;
            }
            e.Handled = true;
        }
    }
}