namespace LessonPlanBuilder.core.subjectAppraiser;

public interface ISubjectAppraiser<TItem>
{
    public double AppraiseSubject(TItem subject);
}