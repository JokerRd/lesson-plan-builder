using LessonPlanBuilder.api.model;
using LessonPlanBuilder.core.model;

namespace LessonPlanBuilder.core.restrictions;

public class SuitableClassroomType : IRestrictionOnCell<Lesson>
{
    public bool Check(Lesson item, Row<Lesson> row, int indexInPut)
    {
        var cell = row.Cells[indexInPut];
        return item.Subject.AvailableClassrooms
            .Any(classroom => classroom.Type == cell.Item.Subject.ClassroomType);
    }
}