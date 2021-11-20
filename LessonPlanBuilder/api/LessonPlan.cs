using System.Collections.Generic;
using LessonPlanBuilder.api.model;

namespace LessonPlanBuilder.api
{
 
    /// <summary>
    /// Класс описывающий выходный параметры
    /// </summary>
    public class LessonPlan
    {
        public Dictionary<DayWeek, Dictionary<int, LessonInfo>> Plan { get; }

        public LessonPlan(Dictionary<DayWeek, Dictionary<int, LessonInfo>> plan)
        {
            Plan = plan;
        }
    }
}