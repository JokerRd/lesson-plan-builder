namespace LessonPlanBuilder.api
{
    public class GenerateSettings
    {
        public int CountLessonPlan { get; }

        public int CountDay { get; }

        public int CountLessonPerDay { get; }

        public int MaxLessonPerDay { get; }

        public GenerateSettings(int countLessonPlan, int countDay,
            int countLessonPerDay, int maxLessonPerDay)
        {
            CountLessonPlan = countLessonPlan;
            CountDay = countDay;
            CountLessonPerDay = countLessonPerDay;
            MaxLessonPerDay = maxLessonPerDay;
        }
    }
}