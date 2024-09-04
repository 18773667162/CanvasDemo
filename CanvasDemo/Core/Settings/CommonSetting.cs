using CanvasDemo.Common;
using CanvasDemo.Core.EventHandlerBindings;
using System.Reflection.Emit;
using System.Windows.Controls;

namespace CanvasDemo.Core.Settings
{
    public class CommonSetting : ICommonSetting
    {
        private Layer? _boxSelector;
        private Layer? _widgetsCanvas;
        private Layer? _selector;
        private List<Layer> _layers = [];
        private Canvas? _drawingCanvas;
        public List<Layer> Layers => _layers;

        public Canvas DrawingCanvas => _drawingCanvas;

        public Layer BoxSelector => _boxSelector;

        public Layer Selector => _selector;

        public Layer WidgetsCanvas => _widgetsCanvas;

        public void Inititial(Canvas drawingCanvas)
        {
            _drawingCanvas = drawingCanvas;
            _widgetsCanvas = LayerFactory.CreateLayer(LayerOperationTypeEnum.WidgetsCanvas);
            drawingCanvas.Children.Add(_widgetsCanvas.Element);
            _widgetsCanvas.Render();
            _boxSelector = LayerFactory.CreateLayer(LayerOperationTypeEnum.BoxSelector);
            drawingCanvas.Children.Add(_boxSelector.Element);
            _boxSelector.Render();
            _selector = LayerFactory.CreateLayer(LayerOperationTypeEnum.Selector);
            drawingCanvas.Children.Add(_selector.Element);
            _selector.Render();
        }

        public void SetLayers(List<Layer> layers, IImageBoxEventHandlerManager imageBoxEventHandler)
        {
            var widgetCanvas = _widgetsCanvas?.Element as Canvas ?? throw new InvalidCastException("no widgetCanvas");
            Layers.Clear();
            widgetCanvas.Children.Clear();
            foreach (var item in layers)
            {
                Layers.Add(item);
                imageBoxEventHandler.BindingEventImageBox(item);
                item.Render();
                widgetCanvas.Children.Add(item.Element);
            }
        }
    }
}