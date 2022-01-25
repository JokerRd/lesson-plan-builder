using LessonPlanBuilder.core.model;

namespace LessonPlanBuilder.core.managers
{
    public interface ITableManager<TItem>
    {
        public bool PutItemsInTable(List<Row<TItem>> rows, Queue<TItem> items);
    }
}