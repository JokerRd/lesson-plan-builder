using LessonPlanBuilder.core.model;

namespace LessonPlanBuilder.core.managers
{
    public class Shifter<TItem> : IShifter<TItem>
    {
        public void PutInCell(TItem item, Cell<TItem> cell)
        {
            cell.PutItem(item);
        }

        public TItem TakeFromCell(Cell<TItem> cell)
        {
            return cell.TakeItem();
        }
    }
}