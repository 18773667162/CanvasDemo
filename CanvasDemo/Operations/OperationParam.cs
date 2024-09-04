using CanvasDemo.Core;
using System.Windows;

namespace CanvasDemo.Operations
{
    public class OperationParam(Layer widget, Layer selector, Vector vector, Point startPoint, Point endPoint)
    {
        public Layer Layer { get; private set; } = widget;

        public Layer Selector { get; private set; } = selector;

        public Vector Delta { get; private set; } = vector;

        public Point StartPoint { get; private set; } = startPoint;

        public Point EndPoint { get; private set; } = endPoint;
    }
}