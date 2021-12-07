using LessonPlanBuilder.core.model;

namespace LessonPlanBuilder.core.services
{
    public interface ICellService<TItem>
    {
        public bool IsPutInCell(TItem item, Row<TItem> row, int indexInPut);
    }
}