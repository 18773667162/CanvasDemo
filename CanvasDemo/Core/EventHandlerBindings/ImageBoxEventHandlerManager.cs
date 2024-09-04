using CanvasDemo.Handler;

namespace CanvasDemo.Core.EventHandlerBindings
{
    public class ImageBoxEventHandlerManager : IImageBoxEventHandlerManager
    {
        private readonly ImageBoxClickSelectionEventHandler _imageBoxClickSelectionEventHandler;

        public ImageBoxEventHandlerManager(ImageBoxClickSelectionEventHandler imageBoxClickSelectionEventHandler) 
        {
            _imageBoxClickSelectionEventHandler = imageBoxClickSelectionEventHandler;
        }

        public void BindingEventImageBox(Layer layer)
        {
            layer.Element.MouseDown += _imageBoxClickSelectionEventHandler.HandleMouseDown;
            layer.Element.MouseUp += _imageBoxClickSelectionEventHandler.HandleMouseUp;
            layer.Element.MouseMove += _imageBoxClickSelectionEventHandler.HandleMouseMove;
        }
    }
}
