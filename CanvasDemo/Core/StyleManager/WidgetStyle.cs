using CanvasDemo.Widgets;
using System.Windows.Media;

namespace CanvasDemo.Core.StyleManager
{
    public class WidgetStyle
    {
        public double BorderThickness { get; set; }

        public Color BorderBrush { get; set; }

        public Color BackgroundColor { get; set; }

        public string? BackgroundImage { get; set; }

        public DashStyle DashStyle { get; set; }

        public void RenderBorderStyle(Layer layer) 
        {
            if (layer.Type == Common.LayerOperationTypeEnum.ImageBox) 
            {
                var imageBox = layer.Element as ImageBox ?? throw new InvalidCastException("no ImageBox");
                imageBox.BorderThickness = BorderThickness;
                imageBox.BorderDashStyle = DashStyle;
                imageBox.BorderBrush = new SolidColorBrush(BorderBrush);
            }
        }
    }
}
