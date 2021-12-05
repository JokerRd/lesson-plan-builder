using System.Collections.Generic;
using LessonPlanBuilder.api.model;
using LessonPlanBuilder.core;
using LessonPlanBuilder.core.model;
using LessonPlanBuilder.core.services;

namespace LessonPlanBuilder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var shifter = new Shifter<Lesson>();
            var cellService = new CellServices<Lesson>();
            var rowManager = new RowManager<Lesson>(shifter, cellService);
            var rowService = new RowService<Lesson>();
            var tableManager = new TableManager<Lesson>(rowManager, rowService);
            var rows = new List<Row<Lesson>>()
            {
                new Row<Lesson>(new Cell<Lesson>[] {new(), new(), new()}),
                new Row<Lesson>(new Cell<Lesson>[] {new(), new(), new()}),
                new Row<Lesson>(new Cell<Lesson>[] {new(), new(), new()})
            };
            var queue = new Queue<Lesson>();
            queue.Enqueue(new Lesson("1", 1, null));
            queue.Enqueue(new Lesson("2", 1, null));
            queue.Enqueue(new Lesson("3", 1, null));
            tableManager.TryPutItemsInTable(rows, queue);

        }

    }
}