using System.Windows.Media;
using CanvasDemo.Core;
using System.Windows;

namespace CanvasDemo.Common
{
    public class Uitl
    {
        public static string? IsExistElementUnderMouse(List<Layer> widgets, Point mousePoint)
        {
            var res = new List<Layer>();
            foreach (var widget in widgets)
            {
                var newLeftTop = GraphicsUitl.PointRatateByCenter(widget.LeftTop, widget.GetCenter(), widget.Angle);
                var newRightTop = GraphicsUitl.PointRatateByCenter(widget.RightTop, widget.GetCenter(), widget.Angle);
                var newLeftBottom = GraphicsUitl.PointRatateByCenter(widget.LeftBottom, widget.GetCenter(), widget.Angle);
                var xAxios = newRightTop - newLeftTop;
                var yAxios = newLeftBottom - newLeftTop;
                var v = mousePoint - newLeftTop;
                var xAngle = GraphicsUitl.CalculateAngle(xAxios, v);
                var yAngle = GraphicsUitl.CalculateAngle(yAxios, v);
                if (xAngle > 90.0 || yAngle > 90.0) continue;
                var xMagnitude = GraphicsUitl.CalculateProjection(xAxios, v);
                var yMagnitude = GraphicsUitl.CalculateProjection(yAxios, v);
                if (xMagnitude.Length <= widget.Width && yMagnitude.Length <= widget.Height)
                {
                    res.Add(widget);
                }
            }
            var resFirst = res.Where(c => c.Type == LayerOperationTypeEnum.ImageBox)
                .OrderByDescending(c => c.ZIndex)
                .FirstOrDefault()?.Id;
            return resFirst;
        }

        /// <summary>
        /// 将向量旋转对应角度
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Vector RotateVector(Vector vector, double angle)
        {
            double sin = Math.Sin(GraphicsUitl.AngleToRadian(angle));
            double cos = Math.Cos(GraphicsUitl.AngleToRadian(angle));

            double tx = vector.X;
            double ty = vector.Y;

            vector.X = cos * tx - sin * ty;
            vector.Y = sin * tx + cos * ty;

            return vector;
        }

        public static List<T> FindVisualChildren<T>(DependencyObject parent) where T : DependencyObject
        {
            List<T> children = [];

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child != null && child is T)
                {
                    children.Add((T)child);
                }

                // 递归查找子控件
                children.AddRange(FindVisualChildren<T>(child));
            }
            return children;
        }
    }
}