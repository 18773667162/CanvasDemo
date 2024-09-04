using CanvasDemo.Common;
using CanvasDemo.Core.StyleManager;
using CanvasDemo.Widgets;

namespace CanvasDemo.Core
{
    public class LayerFactory
    {
        public static Layer CreateLayer(LayerOperationTypeEnum layerType, int count = 0)
        {
            var widget = WidgetFactory.CreateElement(layerType) ?? throw new InvalidOperationException("no create element");
            Layer? layer = null;
            switch (layerType)
            {
                case LayerOperationTypeEnum.Selector:
                    layer = new Layer(widget, 999, 0, 0, LayerOperationTypeEnum.Selector, false);
                    return layer;
                case LayerOperationTypeEnum.BoxSelector:
                    layer = new Layer(widget, 999, 0, 0, LayerOperationTypeEnum.BoxSelector, false);
                    return layer;
                case LayerOperationTypeEnum.ImageBox:
                    layer = new Layer(widget, count + 1, 100, 100, LayerOperationTypeEnum.ImageBox, true);
                    return layer;
                case LayerOperationTypeEnum.WidgetsCanvas:
                    layer = new Layer(widget, -999, 10, 10, LayerOperationTypeEnum.WidgetsCanvas, true, 600, 800);
                    return layer;
                default:
                    throw new InvalidOperationException("no create type");
            }
        }
    }
}