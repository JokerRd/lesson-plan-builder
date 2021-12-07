using System;
using System.Collections.Generic;
using System.Linq;
using LessonPlanBuilder.api.model;
using LessonPlanBuilder.core.model;

namespace LessonPlanBuilder.core.generators
{
    public class GeneratorSequenceItem<TInfoItem> : IGeneratorSequenceItem<TInfoItem>
    {
        private readonly List<Lesson> lessons;
        public GeneratorSequenceItem(List<Lesson> lessons)
        {
            this.lessons = lessons;
        }
        
        public Queue<TInfoItem> Generate(List<TInfoItem> items)
        {
            var rnd = new Random();
            var queueItems = new Queue<TInfoItem>();
            var shuffleList = items.OrderBy(s => rnd.NextDouble());
            foreach (var e in shuffleList)
                queueItems.Enqueue(e);
            return queueItems;
        }
    }
}