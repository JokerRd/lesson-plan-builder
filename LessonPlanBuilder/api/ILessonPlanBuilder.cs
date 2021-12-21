using System.Collections.Generic;
using LessonPlanBuilder.api.model;

namespace LessonPlanBuilder.api
{
    public interface ILessonPlanBuilder
    {

        /// <summary>
        /// Аpi к библиотеки генерации расписаний
        /// </summary>
        /// <param name="planParameters">входные параметры для генерации расписания</param>
        /// <returns>Список составленных расписаний</returns>
        public List<LessonPlan> GenerateLessonPlan(List<Subject> subjects);
        
        /// <summary>
        /// Аpi к библиотеки генерации расписаний
        /// </summary>
        /// <param name="planParameters"> входные параметры для генерации расписания </param>
        /// <param name="settings"> системные настройки генератора</param>
        /// <returns>Список составленных расписаний</returns>
        public List<LessonPlan> GenerateLessonPlan(LessonPlanParameters planParameters, GenerateSettings settings = null);

        
        
        
        
        
        /// <summary>
        /// Попытаться получить уже готовые расписания, если они есть, во время работы основной генерации.
        /// </summary>
        /// <returns>Список составленных расписаний</returns>
        public List<LessonPlan> TryGetCreatedLessonPlan();

        /// <summary>
        /// Попытаться сложить уже готовые расписания, если они есть, в предоставленный пользователем контейнер.
        /// </summary>
        /// <param name="container"> контейнер пользователя для расписаний</param>
        public void TryPutCreatedLessonPlanInContainer(List<LessonPlan> container);
    }
}