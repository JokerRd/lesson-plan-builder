using LessonPlanBuilder.api.model.enums;

namespace LessonPlanBuilder.api.model;

public class ApprovedLesson
{
    public string NameLesson { get; }

    public string NameTeacher { get; }

    public string NumberClassroom { get; }

    public bool IsLessonEmpty { get; }

    public ApprovedLesson(string nameLesson, string nameTeacher, string numberClassroom)
    {
        NameLesson = nameLesson;
        NameTeacher = nameTeacher;
        NumberClassroom = numberClassroom;
    }

    public ApprovedLesson()
    {
        IsLessonEmpty = true;
    }
}