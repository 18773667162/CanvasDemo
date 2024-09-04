using CanvasDemo.Common;

namespace CanvasDemo.Operations
{
    public class OperationManager
    {
        public static Operation GetOperation(LayerOperationTypeEnum type)
        {
            return type switch
            {
                LayerOperationTypeEnum.Move => new MoveOperation(),
                LayerOperationTypeEnum.Rotate => new RoateOperation(),
                LayerOperationTypeEnum.Stretch => new ScaleOperation(),
                LayerOperationTypeEnum.Selection => new SelectionOperation(),
                _ => new MoveOperation(),
            };
        }
    }
}