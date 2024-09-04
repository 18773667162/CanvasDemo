using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CanvasDemo.Widgets;

public class WidgetCanvas : Canvas
{
    public double GridSize { get; set; } = 20; // 网格大小
    public Color GridColor { get; set; } = Colors.LightGray; // 网格颜色
    public bool IsVisibleGrid { get; set; } = true; // 是否显示网格

    protected override void OnRender(DrawingContext dc)
    {
        base.OnRender(dc);
        // 绘制网格
        var gridBrush = new SolidColorBrush(GridColor);
        for (double x = 0; x < RenderSize.Width; x += GridSize)
        {
            dc.DrawLine(new Pen(gridBrush, 1), new Point(x, 0), new Point(x, RenderSize.Height));
        }
        for (double y = 0; y < RenderSize.Height; y += GridSize)
        {
            dc.DrawLine(new Pen(gridBrush, 1), new Point(0, y), new Point(RenderSize.Width, y));
        }
    }
}