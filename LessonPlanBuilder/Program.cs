using System.Collections.Generic;
using LessonPlanBuilder.api.model;
using LessonPlanBuilder.core;
using LessonPlanBuilder.core.restrictions;
using LessonPlanBuilder.core.services;
using Ninject;

namespace LessonPlanBuilder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var shifter = new Shifter<Lesson>();
            var countLessonRest = new CountLessonPerDayRestriction(3);
            var countLessonRestCell = new CountLessonPerDayRestrictionForCell(3);
            var twoConsLessonRest = new TwoConsecutiveLessonsRestriction();
            var cellService = new CellServices<Lesson>(new List<IRestrictionOnCell<Lesson>>()
            {
                twoConsLessonRest,
                countLessonRestCell
            });
            var rowManager = new RowManager<Lesson>(shifter, cellService);
            var rowService = new RowService<Lesson>(new List<IRestrictionOnRow<Lesson>>()
            {
                countLessonRest
            });
            var tableManager = new TableManager<Lesson>(rowManager, rowService);
            var manager = new Manager(tableManager, new List<Lesson>()
            {
                new("1"), new("1"), new("1"), new("1"), new("1"),
                new("2"), new("2"), new("3"), new("3"), new("4")
            });
            manager.GenerateLessonPlan(5, 6, 4);
        }

        private static void Init()
        {
            var container = new StandardKernel();
            container.Bind<IShifter<Lesson>>().To<Shifter<Lesson>>();
        }
    }
}