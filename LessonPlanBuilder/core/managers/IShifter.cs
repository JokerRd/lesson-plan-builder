using LessonPlanBuilder.core.model;

namespace LessonPlanBuilder.core.managers
{
    public interface IShifter<TItem>
    {
        public void PutInCell(TItem item, Cell<TItem> cell);

        public TItem TakeFromCell(Cell<TItem> cell);
    }
}