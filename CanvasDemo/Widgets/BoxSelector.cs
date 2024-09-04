using System.Windows;
using System.Windows.Media;

namespace CanvasDemo.Widgets
{
    public class BoxSelector : FrameworkElement
    {
        protected override void OnRender(DrawingContext drawingContext)
        {
            // Use the Width and Height properties to define the rectangle's size
            Rect rect = new(0, 0, Width, Height);

            // Define the brush with light blue color and 30% opacity
            SolidColorBrush fillBrush = new SolidColorBrush(Color.FromArgb(77, 173, 216, 230)); // 77 is 30% of 255

            // Define the pen with blue color for the border
            Pen borderPen = new Pen(Brushes.Blue, 1);

            // Draw the rectangle
            drawingContext.DrawRectangle(fillBrush, borderPen, rect);
        }
    }
}