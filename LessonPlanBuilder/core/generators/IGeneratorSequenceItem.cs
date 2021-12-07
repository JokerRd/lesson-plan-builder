using System.Collections.Generic;

namespace LessonPlanBuilder.core.generators
{
    public interface IGeneratorSequenceItem<TInfoItem>
    {
        public Queue<TInfoItem> Generate(List<TInfoItem> items);
    }
}