namespace CanvasDemo.Operations
{
    public class SelectionOperation : Operation
    {

        public override void BeforeExecute(BeforeOperationParam param)
        {
            base.BeforeExecute(param);
            var element = param.Layer;
            element.IsVisiable = true;
            element.ZIndex = 999;
        }

        public override void Execute(OperationParam param)
        {
            var element = param.Layer;
            element.Left = Math.Min(param.StartPoint.X, param.EndPoint.X);
            element.Top = Math.Min(param.StartPoint.Y, param.EndPoint.Y);
            element.Width = Math.Abs(param.EndPoint.X - param.StartPoint.X);
            element.Height = Math.Abs(param.EndPoint.Y - param.StartPoint.Y);
            element.Render();
        }

        public override void AfterExecute(AfterOperationParam param)
        {
            base.AfterExecute(param);
            var element = param.Layer;
            element.IsVisiable = false;
            element.Width = 0;
            element.Height = 0;
            element.ZIndex = -999;
            element.Render();
        }
    }
}