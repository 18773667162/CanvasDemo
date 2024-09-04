using CanvasDemo.Common;
using CanvasDemo.Core.Settings;
using CanvasDemo.Core.StyleManager;
using System.Windows;
using System.Windows.Media;

namespace CanvasDemo.Core
{
    public class SelectionManager : ISelectionManager
    {
        private readonly ICommonSetting _commonSetting;
        private readonly IStyleManager _styleManager;

        public SelectionManager(ICommonSetting commonSetting, IStyleManager styleManager) 
        {
            _commonSetting = commonSetting;
            _styleManager = styleManager;
        }   

        /// <summary>
        /// 单选
        /// </summary>
        /// <param name="widgets"></param>
        /// <param name="selector"></param>
        /// <param name="widgetId"></param>
        public void SelectionSingle(string widgetId)
        {
            var selectedELement = _commonSetting.Layers.First(c => c.Id == widgetId);
            selectedELement.IsSelected = true;
            var matrixTransform = _commonSetting.WidgetsCanvas.Element.RenderTransform as MatrixTransform ?? throw new NullReferenceException("no matrixTransform");
            var transformedPoint = _commonSetting.WidgetsCanvas.Element.TransformToAncestor(_commonSetting.DrawingCanvas).Transform(new Point(0, 0));
            var matrix = matrixTransform.Matrix;
            _commonSetting.Selector.Width = selectedELement.Width * matrix.M11;
            _commonSetting.Selector.Height = selectedELement.Height * matrix.M22;
            _commonSetting.Selector.Left = selectedELement.Left * matrix.M11 + transformedPoint.X;
            _commonSetting.Selector.Top = selectedELement.Top * matrix.M22 + transformedPoint.Y;
            _commonSetting.Selector.IsVisiable = true;
            _commonSetting.Selector.Angle = selectedELement.Angle;
            _commonSetting.Selector.ZIndex = 999;
            _commonSetting.Selector.Render();
            // 执行外部的执行逻辑: 往外部传入对应的Layer
            _styleManager.OnStyleChangeEvent(new StyleChangeEventArgs(_commonSetting.Layers.FirstOrDefault(c => c.IsSelected)));
        }

        /// <summary>
        /// 多选
        /// </summary>
        /// <param name="widgets"></param>
        /// <param name="selector"></param>
        /// <param name="widgetIds"></param>
        public void SelectionMultiple(List<string> widgetIds)
        {
            var selectedELements = _commonSetting.Layers.FindAll(c => widgetIds.Contains(c.Id));
            var matrixTranform = _commonSetting.WidgetsCanvas.Element.RenderTransform as MatrixTransform ?? throw new NullReferenceException("no matirxTranform");
            var matrix = matrixTranform.Matrix;
            var points = new List<Point>();
            points.AddRange(selectedELements
                .Select(s => GraphicsUitl.PointRatateByCenter(s.LeftTop, s.GetCenter(), s.Angle))
                .ToList());
            points.AddRange(selectedELements
                .Select(s => GraphicsUitl.PointRatateByCenter(s.RightTop, s.GetCenter(), s.Angle))
                .ToList());
            points.AddRange(selectedELements
                .Select(s => GraphicsUitl.PointRatateByCenter(s.LeftBottom, s.GetCenter(), s.Angle))
                .ToList());
            points.AddRange(selectedELements
                .Select(s => GraphicsUitl.PointRatateByCenter(s.RightBottom, s.GetCenter(), s.Angle))
                .ToList());
            var minLeft = points.Min(c => c.X);
            var maxRight = points.Max(c => c.X);
            var minTop = points.Min(c => c.Y);
            var maxBottom = points.Max(c => c.Y);
            selectedELements.ForEach(s => {
                s.IsSelected = true;
            });
            minLeft *= matrix.M11;
            maxRight *= matrix.M11;
            minTop *= matrix.M22;
            maxBottom *= matrix.M22;
            _commonSetting.Selector.Left = minLeft + _commonSetting.WidgetsCanvas.Left;
            _commonSetting.Selector.Top = minTop + _commonSetting.WidgetsCanvas.Top;
            _commonSetting.Selector.IsVisiable = true;
            _commonSetting.Selector.Width = maxRight - minLeft;
            _commonSetting.Selector.Height = maxBottom - minTop;
            _commonSetting.Selector.Angle = 0;
            _commonSetting.Selector.Render();
            // 执行外部的执行逻辑: 往外部传入对应的Layer
            _styleManager.OnStyleChangeEvent(new StyleChangeEventArgs(_commonSetting.Layers.FirstOrDefault(c => c.IsSelected)));
        }

        public void CancelSelection()
        {
            var selectedELements = _commonSetting.Layers.FindAll(c => c.IsSelected && c.Type == LayerOperationTypeEnum.ImageBox);
            foreach (var widget in selectedELements)
            {
                widget.IsSelected = false;
            }
            _commonSetting.Selector.IsVisiable = false;
            _commonSetting.Selector.Render();
            // 执行外部的执行逻辑: 往外部传入对应的Layer
            _styleManager.OnStyleChangeEvent(new StyleChangeEventArgs(null));
        }
    }
}