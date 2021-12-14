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

            if (!row.Cells[indexInPut - 2].IsEmpty && row.Cells[indexInPut - 2].Item.Subject.Name == item.Subject.Name)
            {
                if (!row.Cells[indexInPut - 1].IsEmpty && row.Cells[indexInPut - 1].Item.Subject.Name == item.Subject.Name)
                {
                    return false;
                }
            }

            return true;
        }
    }
}