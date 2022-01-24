using LessonPlanBuilder.core.model;

namespace LessonPlanBuilder.core
{
    public interface IRowManager<TItem>
    {
        public ResultPutItem TryPutItemInRow(TItem item, Row<TItem> row, int start, Action downCountItems);
    }
}