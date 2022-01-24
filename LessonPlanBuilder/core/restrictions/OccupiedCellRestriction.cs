using LessonPlanBuilder.api.model;
using LessonPlanBuilder.core.model;

namespace LessonPlanBuilder.core.restrictions;

public class OccupiedCellRestriction : IRestrictionOnCell<Lesson>
{
    public bool Check(Lesson item, Row<Lesson> row, int indexInPut)
    {
        var isEmpty = row.Cells[indexInPut].IsEmpty;
        Console.WriteLine(row.Cells[indexInPut].Item + " " + isEmpty);
        return isEmpty;
    }
}