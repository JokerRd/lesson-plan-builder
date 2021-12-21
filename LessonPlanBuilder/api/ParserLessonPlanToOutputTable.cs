using LessonPlanBuilder.api.model;

namespace LessonPlanBuilder.api;

public class ParserLessonPlanToOutputTable
{
    public static List<string[,]> ParseLessonPlanToTable(List<LessonPlan> lessonsPlans, int countDays, int countLessons)
    {
        var results = new List<string[,]>();
        foreach (var lessonPlan in lessonsPlans)
        {
            var table = new string[countDays, countLessons];
            for (var i = 0; i < countDays; i++)
            {
                for (var j = 0; j < countLessons; j++)
                {
                    table[i, j] = FormatInfoAboutLesson(lessonPlan[i, j]);
                }
            }
        }

        return results;
    }

    private static string FormatInfoAboutLesson(ApprovedLesson approvedLesson)
    {
        if (approvedLesson.IsLessonEmpty)
        {
            return "Занятия нет";
        }

        return $"Предмет: {approvedLesson.NameLesson}\n" +
               $"Преподователь: {approvedLesson.NameTeacher}\n" +
               $"Аудитория: {approvedLesson.NumberClassroom}\n";
    }
}