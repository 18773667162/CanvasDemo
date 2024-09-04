using CanvasDemo.Common;
using CanvasDemo.Core;
using CanvasDemo.Core.Settings;
using System.Windows;
using System.Windows.Media;

namespace CanvasDemo.Operations
{
    public class ScaleOperation : Operation
    {
        private record ScaleSize
        {
            public ScaleSize(string id, double xScale, double yScale, double wScale, double hScale)
            {
                Id = id;
                XScale = xScale;
                YScale = yScale;
                WScale = wScale;
                HScale = hScale;
            }

            public string Id { get; private set; }

            public double XScale { get; private set; }

            public double YScale { get; private set; }

            public double WScale { get; private set; }

            public double HScale { get; private set; }
        }

        private const double _minWidth = 6;
        private const double _minHeight = 6;
        private List<ScaleSize> _scaleSizes = [];

        /// <summary>
        /// 设置比例
        /// </summary>
        /// <param name="layers"></param>
        /// <param name="selector"></param>
        public void SetProportion(ICommonSetting commonSetting)
        {
            _scaleSizes.Clear();
            var matrixTransform = commonSetting.WidgetsCanvas.Element.RenderTransform as MatrixTransform ?? throw new NullReferenceException("no matrixTransform");
            var temp = matrixTransform.Matrix;
            // temp.Invert();
            foreach (var layer in commonSetting.Layers.Where(c => c.IsSelected))
            {
                var xScale = (layer.Left * temp.M11 - commonSetting.Selector.Left + commonSetting.WidgetsCanvas.Left) / commonSetting.Selector.Width;
                var yScale = (layer.Top * temp.M22 - commonSetting.Selector.Top + commonSetting.WidgetsCanvas.Top) / commonSetting.Selector.Height;
                var wScale = layer.Width / commonSetting.Selector.Width;
                var hScale = layer.Height / commonSetting.Selector.Height;
                var scale = new ScaleSize(layer.Id, xScale, yScale, wScale, hScale);
                _scaleSizes.Add(scale);
            }
        }

        /// <summary>
        /// 按照比例变化
        /// </summary>
        /// <param name="layers"></param>
        /// <param name="selector"></param>
        public void BatchScale(ICommonSetting commonSetting)
        {
            var matrixTransform = commonSetting.WidgetsCanvas.Element.RenderTransform as MatrixTransform ?? throw new NullReferenceException("no matrixTransform");
            var temp = matrixTransform.Matrix;
            temp.Invert();
            foreach (var layer in commonSetting.Layers.Where(c => c.IsSelected))
            {
                var scale = _scaleSizes.First(c => c.Id == layer.Id);
                layer.Left = (scale.XScale * commonSetting.Selector.Width + commonSetting.Selector.Left - commonSetting.WidgetsCanvas.Left) * temp.M11;
                layer.Top = (scale.YScale * commonSetting.Selector.Height + commonSetting.Selector.Top - commonSetting.WidgetsCanvas.Top) * temp.M22;
                layer.Width = scale.WScale * commonSetting.Selector.Width;
                layer.Height = scale.HScale * commonSetting.Selector.Height;
                layer.Render();
            }
        }

        /// <summary>
        /// 注意这里的向量
        /// </summary>
        /// <param name="vector"></param>
        public override void Execute(OperationParam param)
        {
            var element = param.Layer;
            var vector = param.Delta;
            if (element.Angle != 0 && element.Angle != 360) vector = Uitl.RotateVector(vector, -element.Angle);
            var width = element.Width;
            var height = element.Height;
            var newLeft = element.Left;
            var newTop = element.Top;
            Point oldRotatePoint;
            Point newRotatePoint;
            switch (_directionType)
            {
                case StretchDirectionTypeEnum.TopLeft:
                    oldRotatePoint = GraphicsUitl.PointRatateByCenter(element.RightBottom, element.GetCenter(), element.Angle);
                    width -= vector.X;
                    height -= vector.Y;
                    newLeft += vector.X;
                    newTop += vector.Y;
                    newRotatePoint = GraphicsUitl.PointRatateByCenter(element.RightBottom, new Point(newLeft + width / 2, newTop + height / 2), element.Angle);
                    break;
                case StretchDirectionTypeEnum.Top:
                    oldRotatePoint = GraphicsUitl.PointRatateByCenter(element.RightBottom, element.GetCenter(), element.Angle);
                    height -= vector.Y;
                    newTop += vector.Y;
                    newRotatePoint = GraphicsUitl.PointRatateByCenter(element.RightBottom, new Point(newLeft + width / 2, newTop + height / 2), element.Angle);
                    break;
                case StretchDirectionTypeEnum.TopRight:
                    oldRotatePoint = GraphicsUitl.PointRatateByCenter(element.RightBottom, element.GetCenter(), element.Angle);
                    width += vector.X;
                    height -= vector.Y;
                    newTop += vector.Y;
                    newRotatePoint = GraphicsUitl.PointRatateByCenter(element.RightBottom, new Point(newLeft + width / 2, newTop + height / 2), element.Angle);
                    break;
                case StretchDirectionTypeEnum.Right:
                    oldRotatePoint = GraphicsUitl.PointRatateByCenter(element.LeftTop, element.GetCenter(), element.Angle);
                    width += vector.X;
                    newRotatePoint = GraphicsUitl.PointRatateByCenter(element.LeftTop, new Point(newLeft + width / 2, newTop + height / 2), element.Angle);
                    break;
                case StretchDirectionTypeEnum.BottomRight:
                    oldRotatePoint = GraphicsUitl.PointRatateByCenter(element.LeftTop, element.GetCenter(), element.Angle);
                    width += vector.X;
                    height += vector.Y;
                    newRotatePoint = GraphicsUitl.PointRatateByCenter(element.LeftTop, new Point(newLeft + width / 2, newTop + height / 2), element.Angle);
                    break;
                case StretchDirectionTypeEnum.Bottom:
                    oldRotatePoint = GraphicsUitl.PointRatateByCenter(element.LeftTop, element.GetCenter(), element.Angle);
                    height += vector.Y;
                    newRotatePoint = GraphicsUitl.PointRatateByCenter(element.LeftTop, new Point(newLeft + width / 2, newTop + height / 2), element.Angle);
                    break;
                case StretchDirectionTypeEnum.BottomLeft:
                    oldRotatePoint = GraphicsUitl.PointRatateByCenter(element.RightBottom, element.GetCenter(), element.Angle);
                    width -= vector.X;
                    height += vector.Y;
                    newLeft += vector.X;
                    newRotatePoint = GraphicsUitl.PointRatateByCenter(element.RightBottom, new Point(newLeft + width / 2, newTop + height / 2), element.Angle);
                    break;
                case StretchDirectionTypeEnum.Left:
                    oldRotatePoint = GraphicsUitl.PointRatateByCenter(element.RightBottom, element.GetCenter(), element.Angle);
                    width -= vector.X;
                    newLeft += vector.X;
                    newRotatePoint = GraphicsUitl.PointRatateByCenter(element.RightBottom, new Point(newLeft + width / 2, newTop + height / 2), element.Angle);
                    break;
                case StretchDirectionTypeEnum.Vertical:
                    height += vector.Y;
                    newLeft += vector.X;
                    newTop += vector.Y;
                    break;
                case StretchDirectionTypeEnum.Horizontal:
                    width += vector.X;
                    newLeft += vector.X;
                    break;
                default:
                    width += vector.X;
                    height += vector.Y;
                    break;
            }
            element.Width = width;
            element.Height = height;
            element.Left = newLeft;
            element.Top = newTop;
            var correctDistance = oldRotatePoint - newRotatePoint;
            element.Left += correctDistance.X;
            element.Top += correctDistance.Y;
            //Trace.WriteLine($"Selector!  Left:{correctDistance.X} Top:{correctDistance.Y}");
            element.Render();
        }
    }
}
