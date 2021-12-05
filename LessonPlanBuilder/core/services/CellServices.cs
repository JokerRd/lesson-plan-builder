using LessonPlanBuilder.core.model;

namespace LessonPlanBuilder.core.services
{
    public class CellServices<TItem>: ICellService<TItem>
    {
        public bool IsPutInCell(TItem item, Cell<TItem> cell)
        {
            return true;
        }
    }
}