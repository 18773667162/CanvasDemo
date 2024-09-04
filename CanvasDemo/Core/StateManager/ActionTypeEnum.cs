using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDemo.Core.StateManager
{
    public enum ActionTypeEnum : int
    {
        // 拉伸
        Stretch = 1,
        // 移动
        Move = 2,
        // 旋转
        Rotate = 4,
        Add = 8,
        Delete = 16
    }
}
