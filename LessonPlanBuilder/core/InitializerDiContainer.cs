using LessonPlanBuilder.api;
using LessonPlanBuilder.api.initializer;
using LessonPlanBuilder.api.model;
using LessonPlanBuilder.core.generators;
using LessonPlanBuilder.core.managers;
using LessonPlanBuilder.core.restrictions;
using LessonPlanBuilder.core.services;
using LessonPlanBuilder.core.subjectAppraiser;
using Ninject;

namespace LessonPlanBuilder.core;

public class InitializerDiContainer
{
    private readonly StandardKernel container;

    public InitializerDiContainer(List<Subject> lessons, GenerateSettings settings)
    {
        container = new StandardKernel();
        InitializeService(lessons, settings);
        InitializeRestriction(settings);
    }

    public IManagerLessonBuilder GetManager()
    {
        return container.Get<ManagerLessonBuilder>();
    }

    public Initializer GetInitializer()
    {
        return container.Get<Initializer>();
    }

    private void InitializeService(List<Subject> lessons, GenerateSettings settings)
    {
        container.Bind<IShifter<Lesson>>().To<Shifter<Lesson>>();
        container.Bind<IRowService<Lesson>>().To<RowService<Lesson>>();
        container.Bind<ICellService<Lesson>>().To<CellService<Lesson>>();
        container.Bind<IRowManager<Lesson>>().To<RowManager<Lesson>>();
        container.Bind<IGeneratorSequenceItem<Subject, Lesson>>().ToConstant(new GeneratorSequenceItem(lessons));
        container.Bind<ITableManager<Lesson>>().To<TableManager<Lesson>>();
        container.Bind<Appraiser<Subject>>().ToConstant(new SubjectAppraiser(settings.CountDay,
            settings.CountLessonPerDay));
        container.Bind<IManagerLessonBuilder>().To<ManagerLessonBuilder>();
    }


    private void InitializeRestriction(GenerateSettings settings)
    {
        container.Bind<IRestrictionOnCell<Lesson>>().ToConstant(new CountLessonPerDayRestrictionForCell(settings.MaxLessonPerDay));
        container.Bind<IRestrictionOnCell<Lesson>>().To<TwoConsecutiveLessonsRestriction>();
        container.Bind<IRestrictionOnRow<Lesson>>().ToConstant(new CountLessonPerDayRestriction(settings.MaxLessonPerDay));
        container.Bind<IRestrictionOnCell<Lesson>>().To<ClassroomFreeRestriction>();
        container.Bind<IRestrictionOnCell<Lesson>>().To<TeacherFreeRestriction>();
        container.Bind<IRestrictionOnCell<Lesson>>().To<OccupiedCellRestriction>();
        container.Bind<IRestrictionOnRow<Lesson>>().To<NoMoreThanTwoTypesLessonPerDayRestrictionRow>();
    }
}