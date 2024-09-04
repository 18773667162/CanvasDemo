using CanvasDemo.Common;
using CanvasDemo.Core.Settings;
using CanvasDemo.Operations;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CanvasDemo.Handler;

public class CanvasMoveEventHandler : EventHandler, IEventHandler
{
    private readonly Operation _operation;
    private Point _judgeSymbole;
    private ICommonSetting _commonSetting;

    public CanvasMoveEventHandler(ICommonSetting commonSetting) : base(commonSetting)
    {
        _commonSetting = commonSetting;
        _operation = OperationManager.GetOperation(LayerOperationTypeEnum.Move);
    }


    public override void HandleMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Right && e.ChangedButton == MouseButton.Right)
        {
            _judgeSymbole = e.GetPosition(_commonSetting.DrawingCanvas);
            base.HandleMouseDown(sender, e);
            _operation.BeforeExecute(new BeforeOperationParam(_commonSetting.WidgetsCanvas, new Point(0.5, 0.5), StretchDirectionTypeEnum.None));
            _commonSetting.DrawingCanvas.ContextMenu.IsOpen = false;
        }
    }

    public override void HandleMouseMove(object sender, MouseEventArgs e)
    {
        if (_operation.Actioning && e.RightButton == MouseButtonState.Pressed)
        {
            var currentPoint = e.GetPosition(_commonSetting.DrawingCanvas);
            var d = currentPoint - _startPoint;
            var matrixTransform = _commonSetting.WidgetsCanvas.Element.RenderTransform as MatrixTransform ?? throw new NullReferenceException("no matrixTransform");
            var matrix = matrixTransform.Matrix;
            matrix.OffsetX += d.X;
            matrix.OffsetY += d.Y;
            matrixTransform.Matrix = matrix;
            var transformedPoint = _commonSetting.WidgetsCanvas.Element.TransformToAncestor(_commonSetting.DrawingCanvas).Transform(new Point(0, 0));
            _commonSetting.WidgetsCanvas.Left = transformedPoint.X;
            _commonSetting.WidgetsCanvas.Top = transformedPoint.Y;
            _operation.Execute(new OperationParam(_commonSetting.Selector, _commonSetting.Selector, d, _startPoint, currentPoint));
            _startPoint = currentPoint;
        }
    }

    public override void HandleMouseUp(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Right && e.RightButton == MouseButtonState.Released)
        {
            base.HandleMouseUp(sender, e);
            if (_judgeSymbole == _startPoint)
            {
                _judgeSymbole = _startPoint;
                _commonSetting.DrawingCanvas.ContextMenu.IsOpen = true;
                e.Handled = true;
            }
            _operation.AfterExecute(new AfterOperationParam(_commonSetting.WidgetsCanvas));
        }
    }
}