using System.Linq;
using LessonPlanBuilder.api.model;
using LessonPlanBuilder.core.model;

namespace LessonPlanBuilder.core.restrictions
{
    public class CountLessonPerDayRestrictionForCell : IRestrictionOnCell<Lesson>
    {
        private readonly int countLessonPerDay;

        public CountLessonPerDayRestrictionForCell(int countLessonPerDay)
        {
            this.countLessonPerDay = countLessonPerDay;
        }

        public bool Check(Lesson item, Row<Lesson> row, int indexInPut)
        {
            var count = row.Cells.Count(cell => !cell.IsEmpty);
            return count < countLessonPerDay;
        }
    }
}