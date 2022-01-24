using LessonPlanBuilder.api;
using LessonPlanBuilder.api.model;

namespace LessonPlanBuilder.core
{
    public class LessonPlanBuilderCore : ILessonPlanBuilder
    {
        private InitializerDiContainer? initializerDiContainer;

        public List<LessonPlan> GenerateLessonPlan(List<Subject> subjects, GenerateSettings generateSettings)
        {
            InitializeDiContainer(subjects, generateSettings);
            var manager = initializerDiContainer!.GetManager();
            return manager.GenerateLessonPlan(generateSettings.CountDay,
                generateSettings.CountLessonPerDay, generateSettings.CountLessonPlan);
        }

        private void InitializeDiContainer(List<Subject> subjects, GenerateSettings settings)
        {
            initializerDiContainer ??= new InitializerDiContainer(subjects, settings);
        }
    }
}