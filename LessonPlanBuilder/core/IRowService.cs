using LessonPlanBuilder.core.model;

namespace LessonPlanBuilder.core
{
    public interface IRowService<TItem>
    {
        public bool IsPutInRow(TItem item, Row<TItem> row);
    }
}