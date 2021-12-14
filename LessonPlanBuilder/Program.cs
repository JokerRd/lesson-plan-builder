using System.Collections.Generic;
using LessonPlanBuilder.api.model;
using LessonPlanBuilder.core;
using LessonPlanBuilder.core.generators;
using LessonPlanBuilder.core.restrictions;
using LessonPlanBuilder.core.services;
using Ninject;

namespace LessonPlanBuilder
{
    public class Program
    {
        public static void Main(string[] args)
        {

        }

        private static void Init(List<Lesson> lessons)
        {
            var container = new StandardKernel();
            container.Bind<IShifter<Lesson>>().To<Shifter<Lesson>>();
            container.Bind<IRestrictionOnCell<Lesson>>().ToConstant(new CountLessonPerDayRestrictionForCell(4));
            container.Bind<IRestrictionOnCell<Lesson>>().To<TwoConsecutiveLessonsRestriction>();
            container.Bind<IRestrictionOnRow<Lesson>>().ToConstant(new CountLessonPerDayRestriction(4));
            container.Bind<IRowService<Lesson>>().To<RowService<Lesson>>();
            container.Bind<ICellService<Lesson>>().To<CellServices<Lesson>>();
            container.Bind<IRowManager<Lesson>>().To<RowManager<Lesson>>();
            container.Bind<IGeneratorSequenceItem<Lesson>>().ToConstant(new GeneratorSequenceItem<Lesson>(lessons));
            container.Bind<ITableManager<Lesson>>().To<TableManager<Lesson>>();
            container.Bind<Manager>().ToSelf();
        }
    }
}