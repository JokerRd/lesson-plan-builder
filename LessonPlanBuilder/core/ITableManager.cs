using System.Collections.Generic;
using LessonPlanBuilder.core.model;

namespace LessonPlanBuilder.core
{
    public interface ITableManager<TItem>
    {
        public bool TryPutItemsInTable(List<Row<TItem>> rows, List<TItem> items);
    }
}