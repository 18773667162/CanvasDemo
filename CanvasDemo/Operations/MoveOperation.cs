namespace CanvasDemo.Operations
{
    public class MoveOperation : Operation
    {

        public override void Execute(OperationParam param)
        {
            var element = param.Layer;
            var vector = param.Delta;
            element.Left += vector.X;
            element.Top += vector.Y;
            element.Render();
        }
    }
}