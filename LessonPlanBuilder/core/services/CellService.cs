using System.Collections.Generic;
using System.Linq;
using LessonPlanBuilder.core.model;
using LessonPlanBuilder.core.restrictions;

namespace LessonPlanBuilder.core.services
{
    public class CellService<TItem> : ICellService<TItem>
    {
        private readonly List<IRestrictionOnCell<TItem>> restrictions;

        private List<IRestrictionOnCell<TItem>> Restrictions
        {
            get => restrictions;
            init
            {
                if (value == null || value.Count == 0)
                {
                    throw new ArgumentException("Restrictions is empty");
                }

                restrictions = value;
            }
        }

        public CellService(List<IRestrictionOnCell<TItem>> restrictions)
        {
            Restrictions = restrictions;
        }


        public bool IsPutInCell(TItem item, Row<TItem> row, int indexInPut)
        {
            return Restrictions.All(cell => cell.Check(item, row, indexInPut));
        }
    }
}