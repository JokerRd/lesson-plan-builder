using System;
using System.Collections.Generic;
using System.Linq;

namespace LessonPlanBuilder.core.generators
{
    public class GeneratorSequenceItem<TInfoItem> : IGeneratorSequenceItem<TInfoItem>
    {
        private readonly List<TInfoItem> lessons;
        public GeneratorSequenceItem(List<TInfoItem> lessons)
        {
            this.lessons = lessons;
        }
        
        public Queue<TInfoItem> Generate(List<TInfoItem> items)
        {
            var rnd = new Random();
            var queueItems = new Queue<TInfoItem>();
            var shuffleList = lessons.OrderBy(s => rnd.NextDouble());
            foreach (var e in shuffleList)
                queueItems.Enqueue(e);
            return queueItems;
        }
    }
}