namespace CanvasDemo.Core
{
    public interface ISelectionManager
    {
        public void SelectionMultiple(List<string> widgetIds);
        public void SelectionSingle(string widgetId);
        public void CancelSelection();
    }
}
