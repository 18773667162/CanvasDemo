using CanvasDemo.Common;
using CanvasDemo.Core.EventHandlerBindings;
using CanvasDemo.Core.Settings;
using System.Text.Json;

namespace CanvasDemo.Core.StateManager;

public class UndoRedoManager : IUndoRedoManager
{
    private Stack<OperationState> _undoStack = new();
    private Stack<OperationState> _redoStack = new();
    private readonly ICommonSetting _setting;
    private readonly IImageBoxEventHandlerManager _imageBoxEventHandlerManager;
    private readonly ISelectionManager _selectionManager;

    public UndoRedoManager(ICommonSetting setting, IImageBoxEventHandlerManager imageBoxEventHandlerManager, ISelectionManager selectionManager)
    {
        _setting = setting;
        _imageBoxEventHandlerManager = imageBoxEventHandlerManager;
        _selectionManager = selectionManager;
    }

    public void ExecuteOperation(OperationState state)
    {
        _undoStack.Push(state);
        _redoStack.Clear();
    }

    /// <summary>
    /// 撤销
    /// </summary>
    /// <returns></returns>
    public void Undo()
    {
        if (_undoStack.Count > 0)
        {
            if (_redoStack.Count <= 0) 
            {
                var json = JsonSerializer.Serialize(_setting.Layers.Select(s => s.ToLayerEntity()).ToList());
                _redoStack.Push(new OperationState(json, ActionTypeEnum.Add));
            }
            var state = _undoStack.Pop();
            _redoStack.Push(state);
            var objs = JsonSerializer.Deserialize<List<LayerEntity>>(state.Data);
            var list = objs?.Select(c => c.ToLayer()).ToList() ?? [];
            _setting.SetLayers(list, _imageBoxEventHandlerManager);
            _selectionManager.CancelSelection();
        }
    }

    /// <summary>
    /// 重做
    /// </summary>
    /// <returns></returns>
    public void Redo()
    {
        if (_redoStack.Count > 0)
        {
            var state = _redoStack.Pop();
            var objs = JsonSerializer.Deserialize<List<LayerEntity>>(state.Data);
            var list = objs?.Select(c => c.ToLayer()).ToList() ?? [];
            _setting.SetLayers(list, _imageBoxEventHandlerManager);
            _undoStack.Push(state);
            _selectionManager.CancelSelection();
        }
    }
}