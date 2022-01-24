using LessonPlanBuilder.api.model;
using LessonPlanBuilder.core.model;

namespace LessonPlanBuilder.core.restrictions;

public class NoMoreThanTwoTypesLessonPerDayRestrictionRow : IRestrictionOnRow<Lesson>
{
    public bool Check(Lesson item, Row<Lesson> row)
    {
        var countIdenticalTypes = 0;
        foreach (var cell in row.Cells)
        {
            if (!cell.IsEmpty && cell.Item.Subject.Name == item.Subject.Name)
            {
                countIdenticalTypes++;
            }

            if (countIdenticalTypes == 2)
            {
                return false;
            }
        }

        return true;
    }
}