using LessonPlanBuilder.api;
using LessonPlanBuilder.api.model;

namespace LessonPlanBuilder.core;

public interface IManager
{
    public List<LessonPlan> GenerateLessonPlan(int countRow, int countCell, int countLessonPlan);
}