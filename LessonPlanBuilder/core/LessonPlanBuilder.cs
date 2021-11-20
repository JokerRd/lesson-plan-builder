using System.Collections.Generic;
using LessonPlanBuilder.api;

namespace LessonPlanBuilder.core
{
    public class LessonPlanBuilder : ILessonPlanBuilder
    {
        public List<LessonPlan> GenerateLessonPlan(LessonPlanParameters planParameters)
        {
            throw new System.NotImplementedException();
        }

        public List<LessonPlan> GenerateLessonPlan(LessonPlanParameters planParameters, GenerateSettings settings = null)
        {
            throw new System.NotImplementedException();
        }

        
        public List<LessonPlan> TryGetCreatedLessonPlan()
        {
            throw new System.NotImplementedException();
        }

        public void TryPutCreatedLessonPlanInContainer(List<LessonPlan> container)
        {
            throw new System.NotImplementedException();
        }
    }
}