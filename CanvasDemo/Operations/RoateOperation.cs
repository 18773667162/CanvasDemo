using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace CanvasDemo.Operations
{
    public class RoateOperation : Operation
    {

        public override void BeforeExecute(BeforeOperationParam param)
        {
            base.BeforeExecute(param);
        }

        public override void Execute(OperationParam param)
        {
            var vector = param.Delta;
            var element = param.Layer;
            var center = new Vector(element.Left + element.Width / 2, element.Top + element.Height / 2);
            var v1 = new Vector(param.StartPoint.X, param.StartPoint.Y) - center;
            var v2 = new Vector(param.EndPoint.X, param.EndPoint.Y) - center;
            var newAngle = Vector.AngleBetween(v1, v2);
            // Trace.WriteLine(angle);
            Trace.WriteLine($"new: {newAngle}");
            var rotateTransform = element.Element.RenderTransform as RotateTransform ?? throw new InvalidOperationException("can't bind transform");
            rotateTransform.Angle += newAngle;
            rotateTransform.Angle %= 360;
            element.Angle = rotateTransform.Angle;
            element.Render();
        }
    }
}