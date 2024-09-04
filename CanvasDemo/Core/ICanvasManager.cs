using CanvasDemo.Common;
using CanvasDemo.Core.StateManager;
using CanvasDemo.Core.StyleManager;

namespace CanvasDemo.Core
{
    public interface ICanvasManager
    {
        public List<string> SelectedList { get; }

        public void AddElement(LayerOperationTypeEnum type);

        public void RemoveElements();
    }
}