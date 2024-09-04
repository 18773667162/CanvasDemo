using System.Windows;
using System.Windows.Media;

namespace CanvasDemo.Widgets
{
    public class ImageBox : WidgetStyleBase
    {
        // 定义依赖属性用于绑定图片源
        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(ImageBox), new PropertyMetadata(null, OnImageSourceChanged));

        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        private static void OnImageSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // 当图片源更改时，触发重绘
            var imageBox = d as ImageBox ?? throw new NullReferenceException("ImageBox is Null");
            imageBox.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            // 计算内容区域的矩形
            Rect contentRect = new(BorderThickness / 2, BorderThickness / 2,
                Width - BorderThickness, Height - BorderThickness);

            // 计算边框矩形
            Rect borderRect = new(0, 0, Width, Height);

            // 定义填充刷子，使用透明度为30%的浅蓝色
            //SolidColorBrush fillBrush = new SolidColorBrush(Color.FromArgb(77, 173, 216, 230)); // 77 是 30% 的 255

            // 绘制边框
            Pen borderPen = new(BorderBrush, BorderThickness)
            {
                DashStyle = base.BorderDashStyle
            };
            drawingContext.DrawRectangle(null, borderPen, borderRect);

            // 绘制内容区域
            //drawingContext.DrawRectangle(fillBrush, null, contentRect);

            // 如果 ImageSource 不为 null，则绘制图片
            if (ImageSource != null)
            {
                // 创建图像并绘制
                ImageDrawing imageDrawing = new(ImageSource, contentRect);
                drawingContext.DrawDrawing(imageDrawing);
            }
        }
    }
}
