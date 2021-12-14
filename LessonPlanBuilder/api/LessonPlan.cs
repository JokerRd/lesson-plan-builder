using System;
using System.Collections.Generic;
using LessonPlanBuilder.api.model;

namespace LessonPlanBuilder.api
{
 
    /// <summary>
    /// Класс описывающий выходный параметры
    /// </summary>
    public class LessonPlan
    {
        public Dictionary<DayOfWeek, Dictionary<int, Lesson>> Plan { get; }

        public LessonPlan(Dictionary<DayOfWeek, Dictionary<int, Lesson>> plan)
        {
            Plan = plan;
        }
    }
}