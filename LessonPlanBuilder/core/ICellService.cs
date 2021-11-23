using LessonPlanBuilder.core.model;

namespace LessonPlanBuilder.core
{
    public interface ICellService<TItem>
    {
        public bool IsPutInCell(TItem item, Cell<TItem> cell);
    }
}