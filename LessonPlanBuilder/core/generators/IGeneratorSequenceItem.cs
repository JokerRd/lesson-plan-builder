using System.Collections.Generic;
using LessonPlanBuilder.api.model;

namespace LessonPlanBuilder.core.generators
{
    public interface IGeneratorSequenceItem<TSubject>
    {
        /// <summary>
        /// Генерирует нужное количество расписаний
        /// </summary>
        /// <param name="gradedLessons">
        /// Соответствия предметов и их оценки
        /// <returns>
        /// Очередь предметов в их простом представлении
        /// </returns>
        public Queue<Lesson> Generate(Dictionary<TSubject, int> gradedLessons);
    }
}