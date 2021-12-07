using System.Collections.Generic;
using LessonPlanBuilder.api.model;
using LessonPlanBuilder.core.generators;
using LessonPlanBuilder.core.model;

namespace LessonPlanBuilder.core
{
    public class Manager
    {
        private ITableManager<LessonInfo> TableManager { get; }

        private IGeneratorSequenceItem<LessonInfo> Generator { get; }
        

        public Manager(List<Lesson> lessonInfos)
        {
            Generator = new GeneratorSequenceItem<LessonInfo>(lessonInfos);
        }


        public void GenerateLessonPlan()
        {
            for (var i = 0; i < 5; i++)
            {
                var table = new List<Row<Lesson>>();
                var items = Generator.Generate(new List<LessonInfo>());
                TableManager.TryPutItemsInTable(table, items);
            }
        }

        private List<Row<LessonName>> CreateTable()
        {
            return null;
        }
    }
}