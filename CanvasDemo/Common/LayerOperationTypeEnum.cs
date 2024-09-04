namespace CanvasDemo.Common
{
    [Flags]
    public enum LayerOperationTypeEnum : int
    {
        // 拉伸
        Stretch = 1,
        // 移动
        Move = 2,
        // 旋转
        Rotate = 4,
        // 框选动作
        Selection = 8,

        Selector = 16,

        BoxSelector = 32,

        ImageBox = 64,

        WidgetsCanvas = 128
    }
}