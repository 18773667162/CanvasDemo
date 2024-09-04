using System.Windows;
using System.Windows.Media;

namespace CanvasDemo.Widgets;

public class WidgetStyleBase : FrameworkElement
{
    public static readonly DependencyProperty BorderThicknessProperty =
        DependencyProperty.Register("BorderThickness", typeof(double), typeof(WidgetStyleBase), new PropertyMetadata(10.0, OnBorderThicknessChanged));

    public static readonly DependencyProperty BorderBrushProperty =
        DependencyProperty.Register("BorderBrush", typeof(SolidColorBrush), typeof(WidgetStyleBase), new PropertyMetadata(Brushes.Black, OnBorderBrushChanged));

    public static readonly DependencyProperty BorderDashStyleProperty = 
        DependencyProperty.Register("BorderDashStyle", typeof(DashStyle), typeof(WidgetStyleBase), new PropertyMetadata(DashStyles.Solid, OnBorderDashStyleChanged));

    private static void OnBorderThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) 
    {
        var imageBox = d as ImageBox ?? throw new NullReferenceException("ImageBox is Null");
        imageBox.InvalidateVisual();
    }

    private static void OnBorderBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) 
    {
        var imageBox = d as ImageBox ?? throw new NullReferenceException("ImageBox is Null");
        imageBox.InvalidateVisual();
    }

    private static void OnBorderDashStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var imageBox = d as ImageBox ?? throw new NullReferenceException("ImageBox is Null");
        imageBox.InvalidateVisual();
    }

    public double BorderThickness
    {
        get { return (double)GetValue(BorderThicknessProperty); }
        set { SetValue(BorderThicknessProperty, value); }
    }

    public SolidColorBrush BorderBrush
    {
        get { return (SolidColorBrush)GetValue(BorderBrushProperty); }
        set { SetValue(BorderBrushProperty, value); }
    }

    public DashStyle BorderDashStyle
    {
        get { return (DashStyle)GetValue(BorderDashStyleProperty); }
        set { SetValue(BorderDashStyleProperty, value); }
    }
}
