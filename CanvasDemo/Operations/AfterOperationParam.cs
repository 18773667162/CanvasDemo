using CanvasDemo.Core;

namespace CanvasDemo.Operations
{
    public class AfterOperationParam(Layer widget)
    {
        public Layer Layer { get; private set; } = widget;
    }
}
