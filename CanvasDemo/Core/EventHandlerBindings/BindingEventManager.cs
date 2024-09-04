using CanvasDemo.Core.Settings;
using CanvasDemo.Core.StateManager;
using CanvasDemo.Handler;
using CanvasDemo.Widgets.SelectorWidget;

namespace CanvasDemo.Core.EventHandlerBindings
{
    public class BindingEventManager : IBindingEventManager
    {
        private readonly ICommonSetting _commonSetting;
        private readonly SelectorMoveEventHandler _selectorMoveEventHandler;
        private readonly SelectorScaleEventHandler _selectorScale;
        private readonly SelectorRoateEventHandler _selectorRoateEventHandler;
        private readonly CanvasSelectionEventHandler _canvasSelectionEventHandler;
        private readonly CanvasMoveEventHandler _canvasMoveEventHandler;
        private readonly CanvasScaleEventHandler _canvasScaleEventHandler;

        public BindingEventManager(
            ICommonSetting commonSetting,
            SelectorMoveEventHandler selectorMoveEventHandler,
            SelectorScaleEventHandler selectorScaleEvent,
            SelectorRoateEventHandler selectorRoateEventHandler,
            CanvasSelectionEventHandler canvasSelectionEvent,
            CanvasMoveEventHandler canvasMoveEventHandler,
            CanvasScaleEventHandler canvasScaleEventHandler
        )
        {
            _commonSetting = commonSetting;
            _selectorMoveEventHandler = selectorMoveEventHandler;
            _selectorScale = selectorScaleEvent;
            _selectorRoateEventHandler = selectorRoateEventHandler;
            _canvasSelectionEventHandler = canvasSelectionEvent;
            _canvasMoveEventHandler = canvasMoveEventHandler;
            _canvasScaleEventHandler = canvasScaleEventHandler;
        }

        public void Binding()
        {
            var selector = _commonSetting.Selector.Element as Selector ?? throw new InvalidCastException("invalid cast");
            selector.MouseDown += _selectorMoveEventHandler.HandleMouseDown;
            selector.MouseUp += _selectorMoveEventHandler.HandleMouseUp;
            selector.MouseMove += _selectorMoveEventHandler.HandleMouseMove;
            selector.SmallRectangleMouseDown += _selectorScale.HandleMouseDown;
            selector.SmallRectangleMouseUp += _selectorScale.HandleMouseUp;
            selector.SmallRectangleMouseMove += _selectorScale.HandleMouseMove;
            selector.RotateMouseDown += _selectorRoateEventHandler.HandleMouseDown;
            selector.RotateMouseMove += _selectorRoateEventHandler.HandleMouseMove;
            selector.RotateMouseUp += _selectorRoateEventHandler.HandleMouseUp;
            _commonSetting.DrawingCanvas.MouseDown += _canvasSelectionEventHandler.HandleMouseDown;
            _commonSetting.DrawingCanvas.MouseMove += _canvasSelectionEventHandler.HandleMouseMove;
            _commonSetting.DrawingCanvas.MouseUp += _canvasSelectionEventHandler.HandleMouseUp;
            _commonSetting.DrawingCanvas.MouseDown += _canvasMoveEventHandler.HandleMouseDown;
            _commonSetting.DrawingCanvas.MouseMove += _canvasMoveEventHandler.HandleMouseMove;
            _commonSetting.DrawingCanvas.MouseUp += _canvasMoveEventHandler.HandleMouseUp;
            _commonSetting.DrawingCanvas.PreviewMouseWheel += _canvasScaleEventHandler.PreviewMouseWheel;
        }
    }
}