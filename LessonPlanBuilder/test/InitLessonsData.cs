namespace LessonPlanBuilder.test;

public class InitLessonsData
{
    public int CountDays { get; set; }

    public int CountLessonInDay { get; set; }

    public int CountLessons { get; set; }

    public int CountTypeClassroom { get; set; }

    public int CountClassroomInHashSet { get; set; }

    public int CountListHashSet { get; set; }

    public InitLessonsData(int countDays, int countLessons, int countTypeClassroom,
        int countClassroomInHashSet, int countListHashSet, int countLessonInDay)
    {
        CountDays = countDays;
        CountLessons = countLessons;
        CountTypeClassroom = countTypeClassroom;
        CountClassroomInHashSet = countClassroomInHashSet;
        CountListHashSet = countListHashSet;
        CountLessonInDay = countLessonInDay;
    }
}