using LessonPlanBuilder.api;
using LessonPlanBuilder.api.model;
using LessonPlanBuilder.core;
using LessonPlanBuilder.core.generators;
using LessonPlanBuilder.core.restrictions;
using LessonPlanBuilder.core.services;
using LessonPlanBuilder.core.subjectAppraiser;
using Ninject;

namespace LessonPlanBuilder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var teachers = Utils.CreateTeachers(8, 6, 7);
            var classrooms = Utils.CreateClassrooms(10, 6, 7);
            var listAvailableClassroom = Utils.CreateHashSetClassroom(classrooms, 4, 8);
            var subjects = Utils.CreateSubject(teachers, listAvailableClassroom);
            var lessons = Utils.CreateLessons(subjects);
            var gradeLessons = Utils.CreateGradeLessons(subjects, 3);
            var manager = Init(lessons);
            var lessonPlans = manager.GenerateLessonPlan(6, 7, 5, gradeLessons);
            var lessonPlansStrings = ParserLessonPlanToOutputTable.ParseLessonPlanToTable(lessonPlans, 6, 7);
            /*foreach (var lessonPlan in lessonPlansStrings)
            {
                Utils.PrintTwoArray(lessonPlan);
            }*/
        }

        public static Manager Init(List<Lesson> lessons)
        {
            var container = new StandardKernel();
            container.Bind<IShifter<Lesson>>().To<Shifter<Lesson>>();
            container.Bind<IRestrictionOnCell<Lesson>>().ToConstant(new CountLessonPerDayRestrictionForCell(3));
            container.Bind<IRestrictionOnCell<Lesson>>().To<TwoConsecutiveLessonsRestriction>();
            container.Bind<IRestrictionOnRow<Lesson>>().ToConstant(new CountLessonPerDayRestriction(3));
            container.Bind<IRestrictionOnCell<Lesson>>().To<ClassroomFreeRestriction>();
            container.Bind<IRestrictionOnCell<Lesson>>().To<SuitableClassroomType>();
            container.Bind<IRestrictionOnCell<Lesson>>().To<TeacherFreeRestriction>();
            container.Bind<IRowService<Lesson>>().To<RowService<Lesson>>();
            container.Bind<ICellService<Lesson>>().To<CellServices<Lesson>>();
            container.Bind<IRowManager<Lesson>>().To<RowManager<Lesson>>();
            container.Bind<IGeneratorSequenceItem<Subject>>().ToConstant(new GeneratorSequenceItem<Subject>());
            container.Bind<ITableManager<Lesson>>().To<TableManager<Lesson>>();
            container.Bind<ISubjectAppraiser<Subject>>().ToConstant(new SubjectAppraiser(6, 7));
            container.Bind<Manager>().ToSelf();
            return container.Get<Manager>();
        }
    }
}