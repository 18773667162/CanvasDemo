using CanvasDemo.Core;
using CanvasDemo.Core.StateManager;
using CanvasDemo.Core.StyleManager;
using CanvasDemo.Widgets;
using System.Windows.Media;

namespace CanvasDemo.Common
{
    public static class ObjConvert
    {
        public static BorderDashStyleEnum DashStyleToEnum(DashStyle dashStyle)
        {
            if (DashStyles.Solid == dashStyle) return BorderDashStyleEnum.Solid;
            if (DashStyles.Dash == dashStyle) return BorderDashStyleEnum.Dash;
            if (DashStyles.DashDot == dashStyle) return BorderDashStyleEnum.DashDot;
            if (DashStyles.DashDotDot == dashStyle) return BorderDashStyleEnum.DashDotDot;
            if (DashStyles.Dot == dashStyle) return BorderDashStyleEnum.Dot;
            return BorderDashStyleEnum.Solid;
        }

        public static DashStyle EnumToDashStyle(BorderDashStyleEnum dashStyle)
        {
            if (BorderDashStyleEnum.Solid == dashStyle) return DashStyles.Solid;
            if (BorderDashStyleEnum.Dash == dashStyle) return DashStyles.Dash;
            if (BorderDashStyleEnum.DashDot == dashStyle) return DashStyles.DashDot;
            if (BorderDashStyleEnum.DashDotDot == dashStyle) return DashStyles.DashDotDot;
            if (BorderDashStyleEnum.Dot == dashStyle) return DashStyles.Dot;
            return DashStyles.Solid;
        }

        public static LayerEntity ToLayerEntity(this Layer layer)
        {
            ArgumentNullException.ThrowIfNull(layer);
            return new LayerEntity(
                layer.Id,
                layer.Type,
                layer.ZIndex,
                layer.Left,
                layer.Top,
                layer.Angle,
                layer.Width,
                layer.Height,
                layer.IsSelected,
                layer.IsVisiable,
                layer.BorderStyle?.ToStyleEntity()
            );
        }

        public static Layer ToLayer(this LayerEntity layerEntity)
        {
            ArgumentNullException.ThrowIfNull(layerEntity);
            var element = WidgetFactory.CreateElement(layerEntity.Type);
            return new Layer(
                layerEntity.Id,
                element,
                layerEntity.ZIndex,
                layerEntity.Left,
                layerEntity.Top,
                layerEntity.Type,
                layerEntity.IsVisiable,
                layerEntity.BorderStyle.ToWidgetStyle(),
                layerEntity.Width,
                layerEntity.Height
            );
        }

        public static StyleEntity ToStyleEntity(this WidgetStyle? widgetStyle)
        {
            if (widgetStyle == null) return new StyleEntity(1.0, Colors.Black, Colors.White, null, BorderDashStyleEnum.Solid);
            return new StyleEntity(
                widgetStyle.BorderThickness,
                widgetStyle.BorderBrush,
                widgetStyle.BackgroundColor,
                widgetStyle.BackgroundImage,
                DashStyleToEnum(widgetStyle.DashStyle)
            );
        }

        public static WidgetStyle ToWidgetStyle(this StyleEntity styleEntity)
        {
            ArgumentNullException.ThrowIfNull(styleEntity);
            return new WidgetStyle()
            {
                BorderThickness = styleEntity.BorderThickness,
                BackgroundColor = styleEntity.BackgroundColor,
                BackgroundImage = styleEntity.BackgroundImage,
                BorderBrush = styleEntity.BorderBrush,
                DashStyle = EnumToDashStyle(styleEntity.DashStyle),
            };
        }
    }
}