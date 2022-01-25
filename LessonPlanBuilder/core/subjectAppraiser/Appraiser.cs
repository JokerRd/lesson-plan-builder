namespace LessonPlanBuilder.core.subjectAppraiser;

public abstract class Appraiser<TItem> 
{
    public int[] RestrictionParameters { get; private set; }
    
    public Appraiser(params int[] restrictionParameters)
    {
        RestrictionParameters = restrictionParameters;
    }
    
    public virtual double AppraiseItem(TItem item)
    {
        return item == null ? 0.0 : 
            RestrictionParameters.Aggregate(1.0, (current, e) => current * e * item.GetHashCode());
    }
}