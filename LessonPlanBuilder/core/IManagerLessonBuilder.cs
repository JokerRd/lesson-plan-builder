using LessonPlanBuilder.api;
using LessonPlanBuilder.api.model;

namespace LessonPlanBuilder.core;

public interface IManagerLessonBuilder
{
    public List<LessonPlan> GenerateLessonPlan(int countRow, int countCell, int countLessonPlan);
}