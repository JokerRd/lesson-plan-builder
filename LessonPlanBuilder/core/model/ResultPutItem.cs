namespace LessonPlanBuilder.core.model
{
    public class ResultPutItem
    {
        public bool IsPut { get; }

        public int IndexPut { get; }
        
        public ResultPutItem(bool isPut, int indexPut)
        {
            IsPut = isPut;
            IndexPut = indexPut;
        }
    }
}