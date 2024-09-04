using System.Windows.Media;

namespace CanvasDemo.Core.StateManager
{
    public class StyleEntity
    {
        public StyleEntity() { }

        public StyleEntity(
            double borderThickness,
            Color borderBrush,
            Color backgroundColor,
            string? backgroundImage,
            BorderDashStyleEnum dashStyle
        )
        {
            BorderThickness = borderThickness;
            BorderBrush = borderBrush;
            BackgroundColor = backgroundColor;
            BackgroundImage = backgroundImage;
            DashStyle = dashStyle;
        }

        public double BorderThickness { get; set; }

        public Color BorderBrush { get; set; }

        public Color BackgroundColor { get; set; }

        public string? BackgroundImage { get; set; }

        public BorderDashStyleEnum DashStyle { get; set; }
    }
}