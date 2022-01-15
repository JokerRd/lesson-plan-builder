using LessonPlanBuilder.api;
using LessonPlanBuilder.api.model;
using LessonPlanBuilder.core.generators;
using LessonPlanBuilder.core.model;
using LessonPlanBuilder.core.subjectAppraiser;

namespace LessonPlanBuilder.core
{
    public class Manager
    {
        private ITableManager<Lesson> TableManager { get; }
        private SubjectAppraiser SubjectAppraiser { get; }
        private IGeneratorSequenceItem<Subject, Lesson> Generator { get; }
        
        
        public Manager(TableManager<Lesson> tableManager, IGeneratorSequenceItem<Subject, Lesson> generator, 
            SubjectAppraiser subjectAppraiser)
        {
            TableManager = tableManager;
            Generator = generator;
            SubjectAppraiser = subjectAppraiser;
        }


        public List<LessonPlan> GenerateLessonPlan(int countRow, int countCell, int countLessonPlan, 
            Dictionary<Subject, int> gradeLessons)
        {
            var lessonPlans = new List<LessonPlan>();
            for (var i = 0; i < countLessonPlan; i++)
            {
                var table = CreateTable(countRow, countCell);
                var items = Generator.Generate(SubjectAppraiser);
                if (TableManager.TryPutItemsInTable(table, items))
                {
                    lessonPlans.Add(LessonPlanOutputBuilder.CreateLessonPlan(table));
                    PrintTable(table);
                }
            }

            return lessonPlans;
        }

        private void PrintTable(List<Row<Lesson>> table)
        {
            Console.WriteLine("Новое расписание");
            foreach (var row in table)
            {
                foreach (var cell in row.Cells)
                {
                    if (cell.IsEmpty)
                    {
                        Console.WriteLine("Пусто");
                    }
                    else
                    {
                        Console.WriteLine(cell.Item);
                    }
                }

                Console.WriteLine("Следующий день");
            }
        }

        private List<Row<Lesson>> CreateTable(int countRow, int countCell)
        {
            var result = new List<Row<Lesson>>();
            for (var i = 0; i < countRow; i++)
            {
                var cells = new Cell<Lesson>[countCell];
                for (var j = 0; j < countCell; j++)
                {
                    cells[j] = new Cell<Lesson>(j);
                }
                result.Add(new Row<Lesson>(cells, i));
            }
            return result;
        }
    }
}