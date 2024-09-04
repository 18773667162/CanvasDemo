namespace CanvasDemo.Core.StyleManager
{
    public interface IStyleManager
    {
        public event StyleChangeEventHandler StyleChangeEvent;

        public bool IsSelected { get; }

        public void OnStyleChangeEvent(StyleChangeEventArgs e);

        public void SetElementStyle(WidgetStyle widgetStyle);
    }
}
