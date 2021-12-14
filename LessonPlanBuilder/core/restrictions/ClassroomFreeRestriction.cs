using LessonPlanBuilder.api.model;
using LessonPlanBuilder.api.model.enums;
using LessonPlanBuilder.core.model;

namespace LessonPlanBuilder.core.restrictions;

public class ClassroomFreeRestriction : IRestrictionOnCell<Lesson>
{
    public bool Check(Lesson item, Row<Lesson> row, int indexInPut)
    {
        var cell = row.Cells[indexInPut];
        var classrooms = item.Subject.AvailableClassrooms;
        var result = classrooms
            .Any(classroom => classroom.Schedule[row.Number, cell.Number].Status != ScheduleCellStatus.Busy);
        return result;
    }
}