using System;
using System.Collections.Generic;
using LessonPlanBuilder.api.model;
using LessonPlanBuilder.core.generators;
using LessonPlanBuilder.core.model;

namespace LessonPlanBuilder.core
{
    public class Manager
    {
        private ITableManager<Lesson> TableManager { get; }

        private IGeneratorSequenceItem<Lesson, Subject> Generator { get; }
        

        public Manager(TableManager<Lesson> tableManager, IGeneratorSequenceItem<Lesson, Subject> generator)
        {
            TableManager = tableManager;
            Generator = generator;
        }


        public void GenerateLessonPlan(int countRow, int countCell, int countLessonPlan)
        {
            for (var i = 0; i < countLessonPlan; i++)
            {
                var table = CreateTable(countRow, countCell);
                var items = Generator.Generate(new List<Lesson>());
                if (TableManager.TryPutItemsInTable(table, items))
                {
                    PrintTable(table);
                }
            }
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