using System.Collections.Generic;
using LessonPlanBuilder.api.model;

namespace LessonPlanBuilder.api
{
    public interface ILessonPlanBuilder
    {

        /// <summary>
        /// Аpi к библиотеки генерации расписаний
        /// </summary>
        /// <returns>Список составленных расписаний</returns>
        public List<LessonPlan> GenerateLessonPlan(List<Subject> subjects, GenerateSettings generateSettings);
    }
}