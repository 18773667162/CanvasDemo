using CanvasDemo.Core.Settings;

namespace CanvasDemo.Core.StyleManager
{
    public class StyleManager : IStyleManager
    {
        private readonly ICommonSetting _setting;

        public StyleManager(ICommonSetting commonSetting)
        {
            _setting = commonSetting;
        }

        public bool IsSelected => _setting.Layers.Any(c => c.IsSelected);

        public event StyleChangeEventHandler StyleChangeEvent;

        public void OnStyleChangeEvent(StyleChangeEventArgs e)
        {
            StyleChangeEvent?.Invoke(this, e);
        }

        public void SetElementStyle(WidgetStyle widgetStyle)
        {
            foreach (var layer in _setting.Layers.Where(c => c.IsSelected))
            {
                layer.BorderStyle.BorderThickness = widgetStyle.BorderThickness;
                layer.BorderStyle.DashStyle = widgetStyle.DashStyle;
                layer.BorderStyle.BorderBrush = widgetStyle.BorderBrush;
                layer.Render();
            }
        }
    }
}