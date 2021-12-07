using System.Collections.Generic;
using System.Linq;
using LessonPlanBuilder.core.model;
using LessonPlanBuilder.core.restrictions;

namespace LessonPlanBuilder.core.services
{
    public class CellServices<TItem>: ICellService<TItem>
    {
        public CellServices(List<IRestrictionOnCell<TItem>> restrictions)
        {
            Restrictions = restrictions;
        }

        private List<IRestrictionOnCell<TItem>> Restrictions { get; }
        
        public bool IsPutInCell(TItem item, Row<TItem> row, int indexInPut)
        {
            return Restrictions.All(cell => cell.Check(item, row, indexInPut));
        }
    }
}