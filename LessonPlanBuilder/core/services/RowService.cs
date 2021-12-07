using System.Collections.Generic;
using System.Linq;
using LessonPlanBuilder.core.model;
using LessonPlanBuilder.core.restrictions;

namespace LessonPlanBuilder.core.services
{
    public class RowService<TItem> : IRowService<TItem>
    {
        public RowService(List<IRestrictionOnRow<TItem>> restriction)
        {
            Restriction = restriction;
        }

        private List<IRestrictionOnRow<TItem>> Restriction { get; }

        public bool IsPutInRow(TItem item, Row<TItem> row)
        {
            return Restriction.All(onRow => onRow.Check(item, row));
        }
    }
}