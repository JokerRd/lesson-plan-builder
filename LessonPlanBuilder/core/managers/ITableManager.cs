using LessonPlanBuilder.core.model;

namespace LessonPlanBuilder.core.managers
{
    public interface ITableManager<TItem>
    {
        public bool TryPutItemsInTable(List<Row<TItem>> rows, Queue<TItem> items);
    }
}