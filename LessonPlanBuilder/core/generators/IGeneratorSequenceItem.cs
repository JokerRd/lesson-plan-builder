using System.Collections.Generic;

namespace LessonPlanBuilder.core.generators
{
    public interface IGeneratorSequenceItem<TItem, TInfoItem>
    {
        public Queue<TItem> Generate(List<TInfoItem> items);
    }
}