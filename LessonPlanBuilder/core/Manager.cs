using System.Collections.Generic;
using LessonPlanBuilder.api.model;
using LessonPlanBuilder.core.generators;
using LessonPlanBuilder.core.model;

namespace LessonPlanBuilder.core
{
    public class Manager
    {
        private ITableManager<LessonInfo> TableManager { get; }

        private IGeneratorSequenceItem<Lesson> Generator { get; }
        

        public Manager(List<Lesson> lessons)
        {
            Generator = new GeneratorSequenceItem<Lesson>(lessons);
        }


        public void GenerateLessonPlan()
        {
            for (var i = 0; i < 5; i++)
            {
                var table = new List<Row<Lesson>>();
                var items = Generator.Generate(new List<Lesson>());
                TableManager.TryPutItemsInTable(table, items);
            }
        }

        private List<Row<LessonName>> CreateTable()
        {
            return null;
        }
    }
}