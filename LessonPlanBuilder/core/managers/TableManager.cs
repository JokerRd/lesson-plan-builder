using LessonPlanBuilder.core.model;
using LessonPlanBuilder.core.services;

namespace LessonPlanBuilder.core.managers
{
    public class TableManager<TItem> : ITableManager<TItem>
    {
        private IRowManager<TItem> RowManager { get; }

        private IRowService<TItem> RowService { get; }


        public TableManager(IRowManager<TItem> rowManager, IRowService<TItem> rowService)
        {
            RowService = rowService;
            RowManager = rowManager;
        }

        public bool PutItemsInTable(List<Row<TItem>> rows, Queue<TItem> items)
        {
            var item = items.Peek();
            foreach (var row in rows)
            {
                if (RowService.IsPutInRow(item, row))
                {
                    var index = 0;
                    while (index < row.Cells.Length)
                    {
                        var resultPutItem = RowManager.TryPutItemInRow(item, row, index, () => items.Dequeue());
                        if (!resultPutItem.IsPut)
                        {
                            break;
                        }

                        if (items.Count <= 0)
                        {
                            return true;
                        }


                        item = items.Peek();
                        index = resultPutItem.IndexPut + 1;
                    }
                }
            }
            
            return items.Count <= 0;
        }
    }
}