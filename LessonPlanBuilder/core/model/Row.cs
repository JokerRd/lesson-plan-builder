namespace LessonPlanBuilder.core.model
{
    public class Row<TItem>
    {
        public Cell<TItem>[] Cells { get;  }

        public int Number { get; }

        public Row(Cell<TItem>[] cells, int number)
        {
            Cells = cells;
            Number = number;
        }
    }
}