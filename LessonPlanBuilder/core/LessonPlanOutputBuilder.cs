using LessonPlanBuilder.api;
using LessonPlanBuilder.api.model;
using LessonPlanBuilder.core.model;

namespace LessonPlanBuilder.core;

public class LessonPlanOutputBuilder
{
    public static LessonPlan CreateLessonPlan(List<Row<Lesson>> table)
    {
        var countDays = table.Count;
        var countLesson = table.First().Cells.Length;
        var lessonPlan = new LessonPlan(new ApprovedLesson[countDays, countLesson]);
        for (var i = 0; i < countDays; i++)
        {
            for (var j = 0; j < countLesson; j++)
            {
                lessonPlan[i, j] = CreateApprovedLessonFromCell(table[i].Cells[j]);
            }
        }

        return lessonPlan;
    }

    private static ApprovedLesson CreateApprovedLessonFromCell(Cell<Lesson> cell)
    {
        return cell.IsEmpty ? new ApprovedLesson() 
            : new ApprovedLesson(cell.Item.Subject.Name,
                cell.Item.Subject.Teacher.Name, cell.Item.Subject.ClassroomType);
    }
}