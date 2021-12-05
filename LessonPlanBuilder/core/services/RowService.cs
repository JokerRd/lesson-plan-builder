using LessonPlanBuilder.core.model;

namespace LessonPlanBuilder.core.services
{
    public class RowService<TItem> : IRowService<TItem>
    {
        public bool IsPutInRow(TItem item, Row<TItem> row)
        {
            return true;
        }
    }
}