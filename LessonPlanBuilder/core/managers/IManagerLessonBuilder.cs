using LessonPlanBuilder.api;

namespace LessonPlanBuilder.core.managers;

public interface IManagerLessonBuilder
{
    public List<LessonPlan> GenerateLessonPlan(int countRow, int countCell, int countLessonPlan);
}