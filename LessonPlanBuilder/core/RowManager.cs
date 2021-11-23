using LessonPlanBuilder.core.model;

namespace LessonPlanBuilder.core
{
    public class RowManager<TItem> : IRowManager<TItem>
    {
        private Shifter<TItem> Shifter { get; }

        private ICellService<TItem> CellService { get; }
        
        public RowManager(Shifter<TItem> shifter, ICellService<TItem> cellService)
        {
            Shifter = shifter;
            CellService = cellService;
        }

        public ResultPutItem TryPutItemInRow(TItem item, Row<TItem> row, int start)
        {
            var cells = row.Cells;
            for (var i = start; i < cells.Length; i++)
            {
                if (CellService.IsPutInCell(item, cells[i]))
                {
                    Shifter.PutInCell(item, cells[i]);
                    return new ResultPutItem(true, i);
                }
            }

            return new ResultPutItem(false, cells.Length - 1);
        }
    }
}