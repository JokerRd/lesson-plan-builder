using System;

namespace LessonPlanBuilder.core.model
{
    public class Cell<TItem>
    {

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

        public Cell(TItem item)
        {
            Item = item;
        }

        public Cell()
        {
            IsEmpty = true;
        }

        public void PutItem(TItem item)
        {
            Item = item;
        }

        public TItem TakeItem()
        {
            var item = Item;
            Item = default(TItem);
            return item;
        }
    }
}