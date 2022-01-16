using System;

namespace LessonPlanBuilder.core.model
{
    public class Cell<TItem>
    {

        public int Number { get; }

        private TItem item;
        public TItem Item
        {
            get => item;
            private set
            {
                item = value;
                IsEmpty = false;
            }
        }

        public bool IsEmpty { get; private set; }

        public Cell(TItem item, int number)
        {
            Item = item;
            Number = number;
        }

        public Cell(int number)
        {
            Number = number;
            IsEmpty = true;
        }

        public void PutItem(TItem item)
        {
            Item = item;
        }

        public TItem TakeItem()
        {
            var item = Item;
            Item = default;
            IsEmpty = true;
            return item;
        }
    }
}