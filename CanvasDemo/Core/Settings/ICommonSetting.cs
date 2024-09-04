using CanvasDemo.Core.EventHandlerBindings;
using System.Windows.Controls;

namespace CanvasDemo.Core.Settings
{
    public interface ICommonSetting
    {
        public Layer Selector { get; }

        public Layer BoxSelector { get; }
        public Layer WidgetsCanvas { get; }
        public Canvas DrawingCanvas { get; }
        public List<Layer> Layers { get; }
        public void Inititial(Canvas drawingCanvas);
        public void SetLayers(List<Layer> layers, IImageBoxEventHandlerManager imageBoxEventHandler);
    }
}