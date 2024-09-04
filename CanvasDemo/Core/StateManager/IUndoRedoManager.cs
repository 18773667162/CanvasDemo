namespace CanvasDemo.Core.StateManager
{
    public interface IUndoRedoManager
    {
        public void ExecuteOperation(OperationState state);

        public void Undo();

        public void Redo();
    }
}