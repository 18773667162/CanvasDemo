using CanvasDemo.Common;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace CanvasDemo.Widgets.SelectorWidget
{
    public class Selector : Control
    {
        static Selector()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Selector), new FrameworkPropertyMetadata(typeof(Selector)));
        }

        public delegate void SmallRectangleMouseDownEventHandler(object sender, MouseButtonEventArgs e);
        public event SmallRectangleMouseDownEventHandler SmallRectangleMouseDown;

        public delegate void SmallRectangleMouseMoveEventHandler(object sender, MouseEventArgs e);
        public event SmallRectangleMouseMoveEventHandler SmallRectangleMouseMove;

        public delegate void SmallRectangleMouseUpEventHandler(object sender, MouseButtonEventArgs e);
        public event SmallRectangleMouseUpEventHandler SmallRectangleMouseUp;

        public delegate void RotateMouseDownEventHandler(object sender, MouseButtonEventArgs e);
        public event RotateMouseDownEventHandler RotateMouseDown;

        public delegate void RotateMouseMoveEventHandler(object sender, MouseEventArgs e);
        public event RotateMouseMoveEventHandler RotateMouseMove;

        public delegate void RotateMouseUpEventHandler(object sender, MouseButtonEventArgs e);
        public event RotateMouseUpEventHandler RotateMouseUp;

        /// <summary>
        /// 渲染之前，添加控件之后执行
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var rectangleCanvas = GetTemplateChild("rectangleCanvas") ?? throw new InvalidOperationException("can't find Canvas");
            var canvas = rectangleCanvas as Canvas ?? throw new InvalidCastException("can't cast canvas");
            var list = Uitl.FindVisualChildren<Rectangle>(rectangleCanvas);
            var rects = list.Where(c => c.Name != "rotateRectangle").ToList();
            var rotateRect = list.FirstOrDefault(c => c.Name == "rotateRectangle") ?? throw new NullReferenceException();
            rotateRect.MouseDown += RotateMouseDownHandler;
            rotateRect.MouseUp += RotateMouseUpHandler;
            rotateRect.MouseMove += RotateMouseMoveHandler;
            foreach (var child in rects)
            {
                child.MouseDown += SmallRectangleMouseDownHandler;
                child.MouseMove += SmallRectangleMouseMoveHandler;
                child.MouseUp += SmallRectangleMouseUpHandler;
            }
        }

        public void RotateMouseDownHandler(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                RotateMouseDown?.Invoke(sender, e);
            }
            e.Handled = true;
        }

        public void RotateMouseMoveHandler(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                RotateMouseMove?.Invoke(sender, e);
            }
            // 禁止向下传播
            e.Handled = true;
        }
        public void RotateMouseUpHandler(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                RotateMouseUp?.Invoke(sender, e);
            }
            // 禁止向下传播
            e.Handled = true;
        }

        public void SmallRectangleMouseDownHandler(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) 
            {
                SmallRectangleMouseDown?.Invoke(sender, e);
            }
            e.Handled = true;
        }

        public void SmallRectangleMouseMoveHandler(object sender, MouseEventArgs e) 
        {
            if (e.LeftButton == MouseButtonState.Pressed) 
            {
                SmallRectangleMouseMove?.Invoke(sender, e);
            }
            // 禁止向下传播
            e.Handled = true;
        }

        public void SmallRectangleMouseUpHandler(object sender, MouseButtonEventArgs e) 
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                SmallRectangleMouseUp?.Invoke(sender, e);
            }
            // 禁止向下传播
            e.Handled = true;
        }
    }
}
