using CanvasDemo.Common;

namespace CanvasDemo.Core.StateManager;

public record OperationState
{
    public OperationState(string data, ActionTypeEnum type) 
    {
        Data = data;
        Type = type;
    }

    public string Data { get; private set; }

    public DateTimeOffset Timestamp { get; private set; } = DateTimeOffset.Now;

    public ActionTypeEnum Type { get; private set; }
}
