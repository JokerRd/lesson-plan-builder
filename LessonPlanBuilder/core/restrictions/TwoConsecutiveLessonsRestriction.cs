using LessonPlanBuilder.api.model;
using LessonPlanBuilder.core.model;

namespace LessonPlanBuilder.core.restrictions
{
    public class TwoConsecutiveLessonsRestriction : IRestrictionOnCell<Lesson>
    {
        public bool Check(Lesson item, Row<Lesson> row, int indexInPut)
        {
            if (indexInPut < 2)
            {
                return true;
            }

            return row.Cells[indexInPut - 2].Item.Name != item.Name
                   || row.Cells[indexInPut - 1].Item.Name != item.Name;
        }
    }
}