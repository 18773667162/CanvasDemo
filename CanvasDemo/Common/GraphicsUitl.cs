using System.Windows;

namespace CanvasDemo.Common
{
    public struct RectPoints 
    {
        public Point LeftTop { get; set; }

        public Point TopRight { get; set; }

        public Point BottomLeft { get; set; }

        public Point BottomRight { get; set;}

        public double Angle { get; set; }
    }

    public static class GraphicsUitl
    {

        public static double AngleToRadian(double angle)
        {
            return angle * Math.PI / 180;
        }

        public static Point PointRatateByCenter(Point point, Point center, double angle)
        {
            var radian = AngleToRadian(angle);
            var newX = center.X + (point.X - center.X) * Math.Cos(radian) - (point.Y - center.Y) * Math.Sin(radian);
            var newY = center.Y + (point.X - center.X) * Math.Sin(radian) + (point.Y - center.Y) * Math.Cos(radian);
            return new Point(newX, newY);
        }

        public static bool CheckIsRange(RectPoints rectPoints, RectPoints checkRect)
        {
            var centerX = (checkRect.LeftTop.X + checkRect.BottomRight.X) / 2.0;
            var centerY = (checkRect.LeftTop.Y + checkRect.BottomRight.Y) / 2.0;
            var center = new Point(centerX, centerY);
            var leftTop = PointRatateByCenter(checkRect.LeftTop, center, checkRect.Angle);
            var rightTop = PointRatateByCenter(checkRect.TopRight, center, checkRect.Angle);
            var bottomLeft = PointRatateByCenter(checkRect.BottomLeft, center, checkRect.Angle);
            var bottomRight = PointRatateByCenter(checkRect.BottomRight, center, checkRect.Angle);
            var range = rectPoints.BottomRight - rectPoints.LeftTop;
            var r1 = leftTop - rectPoints.LeftTop;
            var r2 = rightTop - rectPoints.LeftTop;
            var r3 = bottomLeft - rectPoints.LeftTop;
            var r4 = bottomRight - rectPoints.LeftTop;
            if (r1.X < 0 || r1.Y < 0 || r1.X > range.X || r1.Y > range.Y) return false;
            if (r2.X < 0 || r2.Y < 0 || r2.X > range.X || r2.Y > range.Y) return false;
            if (r3.X < 0 || r3.Y < 0 || r3.X > range.X || r3.Y > range.Y) return false;
            if (r4.X < 0 || r4.Y < 0 || r4.X > range.X || r4.Y > range.Y) return false;
            return true;
        }
        

        /// <summary>
        /// 计算两个响亮的夹角
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static double CalculateAngle(Vector u, Vector v) 
        {
            // 计算点积
            double dotProduct = Vector.Multiply(u, v);

            // 计算模长
            double magnitudeA = u.Length;
            double magnitudeB = v.Length;

            // 计算夹角的余弦值
            double cosTheta = dotProduct / (magnitudeA * magnitudeB);

            // 计算夹角（弧度）
            double angleInRadians = Math.Acos(cosTheta);

            // 转换为度数
            double angleInDegrees = angleInRadians * (180 / Math.PI);

            return angleInDegrees;
        }

        /// <summary>
        /// 向量b在向量a上的投影
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector CalculateProjection(Vector a, Vector b)
        {
            // 计算点积
            double dotProduct = Vector.Multiply(a, b);
            // 计算a的模的平方
            double aMagnitudeSquared = a.X * a.X + a.Y * a.Y;

            // 计算投影向量
            Vector projection = (dotProduct / aMagnitudeSquared) * a;
            return projection;
        }
    }


}