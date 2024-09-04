using CanvasDemo.Common;
using CanvasDemo.Widgets.SelectorWidget;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CanvasDemo.Widgets
{
    public class WidgetFactory
    {
        public static FrameworkElement CreateElement(LayerOperationTypeEnum layerType)
        {
            switch (layerType)
            {
                case LayerOperationTypeEnum.Selector:
                    var selector = new Selector
                    {
                        RenderTransform = new RotateTransform(),
                        RenderTransformOrigin = new Point(0.5, 0.5)
                    };
                    return selector;
                case LayerOperationTypeEnum.BoxSelector:
                    var boxSelector = new BoxSelector();
                    return boxSelector;
                case LayerOperationTypeEnum.ImageBox:
                    var img = new ImageBox
                    {
                        RenderTransform = new RotateTransform(),
                        RenderTransformOrigin = new Point(0.5, 0.5)
                    };
                    // 设置 ImageSource 属性
                    var bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.UriSource = new Uri("pack://application:,,,/images/1.jpg");
                    bitmapImage.EndInit();
                    img.ImageSource = bitmapImage;
                    return img;
                case LayerOperationTypeEnum.WidgetsCanvas:
                    var widgetCanvas = new WidgetCanvas
                    {
                        Background = new SolidColorBrush(Color.FromRgb(32, 184, 205)),
                        IsVisibleGrid = true,
                        RenderTransform = new MatrixTransform()
                    };
                    return widgetCanvas;
                default:
                    throw new InvalidOperationException("no type");
            }
        }
    }
}