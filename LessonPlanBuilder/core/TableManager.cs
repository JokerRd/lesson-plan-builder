using System.Collections.Generic;
using LessonPlanBuilder.core.model;

namespace LessonPlanBuilder.core
{
    public class TableManager<TItem> : ITableManager<TItem>
    {
        private IRowManager<TItem> RowManager { get; }
        
        private IRowService<TItem> RowService { get; }
        

        public TableManager(IRowService<TItem> rowService, IRowManager<TItem> rowManager)
        {
            RowService = rowService;
            RowManager = rowManager;
        }
        
        public bool TryPutItemsInTable(List<Row<TItem>> rows, List<TItem> items)
        {
            foreach (var item in items)
            {
                foreach (var row in rows)
                {
                    if (RowService.IsPutInRow(item, row))
                    {
                        var resultPutItem = RowManager.TryPutItemInRow(item, row, 0);
                    }
                }
            }

            return true;
        }
        
    }
}