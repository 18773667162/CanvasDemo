using CanvasDemo.Common;

namespace CanvasDemo.Operations
{
    public abstract class Operation
    {

        protected StretchDirectionTypeEnum _directionType;

        public bool Actioning { get; private set; } = false;

        public virtual void BeforeExecute(BeforeOperationParam param)
        {
            _directionType = param.Direction;
            Actioning = true;
        }

        public abstract void Execute(OperationParam param);

        public virtual void AfterExecute(AfterOperationParam param)
        {
            Actioning = false;
        }
    }
}