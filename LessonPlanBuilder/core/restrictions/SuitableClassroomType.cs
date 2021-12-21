using LessonPlanBuilder.api.model;
using LessonPlanBuilder.core.model;

namespace LessonPlanBuilder.core.restrictions;

public class SuitableClassroomType : IRestrictionOnCell<Lesson>
{
    public bool Check(Lesson item, Row<Lesson> row, int indexInPut)
    {
        return true;
        /*var result = item.Subject.AvailableClassrooms
            .Any(classroom => classroom.Type == item.Subject.ClassroomType);
        return result;*/
    }
}