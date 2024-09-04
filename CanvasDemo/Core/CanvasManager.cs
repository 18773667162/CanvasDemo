using CanvasDemo.Common;
using CanvasDemo.Core.EventHandlerBindings;
using CanvasDemo.Core.Settings;
using CanvasDemo.Core.StateManager;
using System.Text.Json;
using System.Windows.Controls;

namespace CanvasDemo.Core
{
    public class CanvasManager : ICanvasManager
    {
        private readonly ICommonSetting _commonSetting;
        private readonly IImageBoxEventHandlerManager _bindingEventManager;
        private readonly IUndoRedoManager _undoRedoManager;
        private readonly ISelectionManager _selectionManager;

        public List<string> SelectedList => _commonSetting.Layers.Select(s => s.Id).ToList();

        public CanvasManager(
            ICommonSetting commonSetting,
            IImageBoxEventHandlerManager bindingEventManager, 
            IUndoRedoManager undoRedoManager,
            ISelectionManager selectionManager)
        {
            _commonSetting = commonSetting;
            _bindingEventManager = bindingEventManager;
            _undoRedoManager = undoRedoManager;
            _selectionManager = selectionManager;
        }

        public void AddElement(LayerOperationTypeEnum type)
        {
            var data = JsonSerializer.Serialize(_commonSetting.Layers.Select(s => s.ToLayerEntity()).ToList());
            var operation = new OperationState(data, ActionTypeEnum.Add);
            _undoRedoManager.ExecuteOperation(operation);
            var widgetsCanvas = _commonSetting.WidgetsCanvas.Element as Canvas ?? throw new InvalidCastException("no widgetCanvas");
            var element = LayerFactory.CreateLayer(type, _commonSetting.Layers.Count);
            element.Render();
            _commonSetting.Layers.Add(element);
            _bindingEventManager.BindingEventImageBox(element);
            widgetsCanvas.Children.Add(element.Element);
        }

        public void RemoveElements()
        {
            var data = JsonSerializer.Serialize(_commonSetting.Layers.Select(s => s.ToLayerEntity()).ToList());
            var operation = new OperationState(data, ActionTypeEnum.Delete);
            _undoRedoManager.ExecuteOperation(operation);
            var widgetsCanvas = _commonSetting.WidgetsCanvas.Element as Canvas ?? throw new InvalidCastException("no widgetCanvas");
            var list = _commonSetting.Layers.FindAll(c => c.IsSelected && c.Type == LayerOperationTypeEnum.ImageBox);
            _selectionManager.CancelSelection();
            _commonSetting.Layers.RemoveAll(c => c.IsSelected && c.Type == LayerOperationTypeEnum.ImageBox);
            foreach (var layer in list)
            {
                widgetsCanvas.Children.Remove(layer.Element);
                _commonSetting.Layers.Remove(layer);
            }
        }

    }
}