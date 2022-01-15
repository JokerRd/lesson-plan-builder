using System.Collections.Generic;
using LessonPlanBuilder.api.model;
using LessonPlanBuilder.core.subjectAppraiser;

namespace LessonPlanBuilder.core.generators
{
    public interface IGeneratorSequenceItem<TItem, TLesson>
    {
        /// <summary>
        /// Генерирует нужное количество расписаний
        /// </summary>
        /// <param name="gradedLessons">
        /// Оценщик (то каким образом оценятся предметы)
        /// <returns>
        /// Очередь предметов в их простом представлении
        /// </returns>
        public Queue<TLesson> Generate(Appraiser<TItem> appraiser);
    }
}