using System.Windows.Media;

namespace CanvasDemo.Other
{
    public class DashStyleItem
    {
        public string Name { get; set; } // 中文名称
        public DashStyle Value { get; set; } // DashStyle值

        public override string ToString()
        {
            return Name; // 在ComboBox中显示中文名称
        }
    }
}