using CanvasDemo.Core.EventHandlerBindings;

namespace CanvasDemo.Core.RenderManagers
{
    public class RenderManager : IRenderManager
    {
        private readonly IBindingEventManager _bindingEventManager;

        public RenderManager(IBindingEventManager bindingEventManager) 
        {
            _bindingEventManager = bindingEventManager;
        }


    }
}
