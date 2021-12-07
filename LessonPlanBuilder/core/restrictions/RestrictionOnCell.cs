using LessonPlanBuilder.core.model;

namespace LessonPlanBuilder.core.restrictions
{
    public interface IRestrictionOnCell<TItem>
    {
        public bool Check(TItem item, Row<TItem> row, int indexInPut);
    }
}