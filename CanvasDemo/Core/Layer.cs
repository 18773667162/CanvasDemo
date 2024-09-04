using CanvasDemo.Common;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using CanvasDemo.Core.StyleManager;

namespace CanvasDemo.Core
{
    public class Layer
    {

        public Layer(
            FrameworkElement frameworkElement,
            int zIndex,
            double left,
            double top,
            LayerOperationTypeEnum type,
            bool isVisiable,
            double width = 100.0,
            double height = 100.0)
        {
            Id = Guid.NewGuid().ToString("N");
            Element = frameworkElement;
            ZIndex = zIndex;
            Left = left;
            Top = top;
            IsSelected = false;
            Type = type;
            Width = width;
            Height = height;
            BorderStyle = new WidgetStyle()
            {
                BorderThickness = 1,
                BorderBrush = Colors.Black,
                BackgroundColor = Colors.White,
                DashStyle = DashStyles.Solid
            };
            IsVisiable = isVisiable;
        }

        public Layer(
            string id,
            FrameworkElement frameworkElement,
            int zIndex,
            double left,
            double top,
            LayerOperationTypeEnum type,
            bool isVisiable,
            WidgetStyle style,
            double width = 100.0,
            double height = 100.0
        ) : this(
            frameworkElement, zIndex, left, top, type, isVisiable, width, height
        )
        {
            Id = id;
            BorderStyle = style;
        }

        public string Id { get; private set; }

        public LayerOperationTypeEnum Type { get; private set; }

        public int ZIndex { get; set; }

        public double Left { get; set; }

        public double Top { get; set; }

        public double Angle { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public bool IsSelected { get; set; }

        public bool IsVisiable { get; set; }

        public WidgetStyle BorderStyle { get; set; }

        public Point LeftTop => new(Left, Top);

        public Point RightTop => new(Left + Width, Top);

        public Point LeftBottom => new(Left, Top + Height);

        public Point RightBottom => new(Left + Width, Top + Height);

        public FrameworkElement Element { get; private set; }

        public Point GetCenter()
        {
            return new Point(Left + Width / 2, Top + Height / 2);
        }

        public void SetNewLayer(Layer newLayer)
        {
            Id = newLayer.Id;
            Type = newLayer.Type;
            ZIndex = newLayer.ZIndex;
            Left = newLayer.Left;
            Top = newLayer.Top;
            Angle = newLayer.Angle;
            Width = newLayer.Width;
            Height = newLayer.Height;
            IsSelected = newLayer.IsSelected;
            IsVisiable = newLayer.IsVisiable;
            BorderStyle = newLayer.BorderStyle;
            Element = newLayer.Element;
        }

        public void SetElementShow(bool val)
        {
            Element.Visibility = val ? Visibility.Visible : Visibility.Hidden;
        }

        /// <summary>
        /// 绑定Transform
        /// 如果默认不传参数则为解绑
        /// </summary>
        /// <param name="transform"></param>
        public void BindingTransform(Transform? transform = null)
        {
            Element.RenderTransform = transform;
        }

        /// <summary>
        /// 渲染在Canvas
        /// </summary>
        public void Render()
        {
            if (Element.RenderTransform is RotateTransform)
            {
                var rotate = Element.RenderTransform as RotateTransform ?? new RotateTransform();
                rotate.Angle = Angle;
                Element.RenderTransform = rotate;
            }
            Panel.SetZIndex(Element, ZIndex);
            Canvas.SetLeft(Element, Left);
            Canvas.SetTop(Element, Top);
            Element.Width = Width;
            Element.Height = Height;
            Element.Tag = Id;
            BorderStyle.RenderBorderStyle(this);
            SetElementShow(IsVisiable);
        }
    }
}
