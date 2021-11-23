namespace LessonPlanBuilder.core.model
{
    public class Row<TItem>
    {
        public Cell<TItem>[] Cells { get;  }
        
        public Row(Cell<TItem>[] cells)
        {
            Cells = cells;
        }
    }
}