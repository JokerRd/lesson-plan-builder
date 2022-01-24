using LessonPlanBuilder.api.model;
using LessonPlanBuilder.api.model.enums;
using LessonPlanBuilder.core.model;

namespace LessonPlanBuilder.core.restrictions;

public class TeacherFreeRestriction : IRestrictionOnCell<Lesson>
{
    public bool Check(Lesson item, Row<Lesson> row, int indexInPut)
    {
        var cell = row.Cells[indexInPut];
        var scheduleCell = item.Subject.Teacher.Schedule[row.Number, cell.Number];
        return scheduleCell!= ScheduleCell.Impossible;
    }
}