using CanvasDemo.Common;
using System.Text.Json.Serialization;

namespace CanvasDemo.Core.StateManager;

public class LayerEntity
{
    public LayerEntity() { }

    public LayerEntity(
        string id,
        LayerOperationTypeEnum type,
        int zIndex,
        double left,
        double top,
        double angle,
        double width,
        double height,
        bool isSelected,
        bool isVisiable,
        StyleEntity styleEntity
        )
    {
        Id = id;
        Type = type;
        ZIndex = zIndex;
        Left = left;
        Top = top;
        Angle = angle;
        Width = width;
        Height = height;
        IsSelected = isSelected;
        IsVisiable = isVisiable;
        BorderStyle = styleEntity;
    }

    public string Id { get; set; }

    public LayerOperationTypeEnum Type { get; set; }

    public int ZIndex { get; set; }

    public double Left { get; set; }

    public double Top { get; set; }

    public double Angle { get; set; }

    public double Width { get; set; }

    public double Height { get; set; }

    public bool IsSelected { get; set; }

    public bool IsVisiable { get; set; }

    public StyleEntity BorderStyle { get; set; }
}