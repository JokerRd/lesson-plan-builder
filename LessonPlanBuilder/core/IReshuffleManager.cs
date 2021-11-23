using LessonPlanBuilder.core.model;

namespace LessonPlanBuilder.core
{
    public interface IReshuffleManager<TItem>
    {
        public bool IsPutInRow(TItem item, Row<TItem> row);

        public bool IsPutInCell(TItem item, Cell<TItem> cell);
    }
}