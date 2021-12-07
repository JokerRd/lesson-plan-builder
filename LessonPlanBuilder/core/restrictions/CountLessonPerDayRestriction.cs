using System.Linq;
using LessonPlanBuilder.api.model;
using LessonPlanBuilder.core.model;

namespace LessonPlanBuilder.core.restrictions
{
    public class CountLessonPerDayRestriction : IRestrictionOnRow<Lesson>
    {
        private readonly int countLessonPerDay;

        public CountLessonPerDayRestriction(int countLessonPerDay)
        {
            this.countLessonPerDay = countLessonPerDay;
        }


        public bool Check(Lesson item, Row<Lesson> row)
        {
            var count = row.Cells.Count(cell => !cell.IsEmpty);
            return count <= countLessonPerDay;
        }
    }
}