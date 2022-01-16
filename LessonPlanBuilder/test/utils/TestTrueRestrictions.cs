using LessonPlanBuilder.api.model;
using LessonPlanBuilder.core.model;
using LessonPlanBuilder.core.restrictions;

namespace LessonPlanBuilder.test.utils;

public class TestTrueRestrictions : IRestrictionOnCell<Lesson>
{
    public bool Check(Lesson item, Row<Lesson> row, int indexInPut)
    {
        return true;
    }
}