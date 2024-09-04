using CanvasDemo.Common;
using CanvasDemo.Core;
using System.Windows;

namespace CanvasDemo.Operations
{
    public class BeforeOperationParam(Layer widget, Point origin, StretchDirectionTypeEnum stretchDirectionType)
    {
        public Layer Layer { get; private set; } = widget;

        public StretchDirectionTypeEnum Direction { get; set; } = stretchDirectionType;

        public Point Origin { get; private set; } = origin;
    }
}