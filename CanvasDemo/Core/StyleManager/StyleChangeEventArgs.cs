namespace CanvasDemo.Core.StyleManager
{
    public delegate void StyleChangeEventHandler(object sender, StyleChangeEventArgs e);

    public class StyleChangeEventArgs(Layer? layer) : EventArgs
    {
        public Layer? Layer { get; private set; } = layer;
    }
}
