using LessonPlanBuilder.core.model;

namespace LessonPlanBuilder.core.restrictions
{
    public interface IRestrictionOnRow<TItem>
    {
        public bool Check(TItem item, Row<TItem> row);
    }
}