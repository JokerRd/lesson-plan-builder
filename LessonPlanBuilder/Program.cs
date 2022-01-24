using System.Diagnostics;
using LessonPlanBuilder.api;
using LessonPlanBuilder.api.model;
using LessonPlanBuilder.core;
using LessonPlanBuilder.core.generators;
using LessonPlanBuilder.core.restrictions;
using LessonPlanBuilder.core.services;
using LessonPlanBuilder.core.subjectAppraiser;
using Ninject;
using GoogleSheets;
using LessonPlanBuilder.api.initializer;
using LessonPlanBuilder.core.managers;
using LessonPlanBuilder.test;

namespace LessonPlanBuilder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var google = new GoogleInitialization();
            var tableTeachers = google.GetTeachersSchedule();
            var tableClassrooms = google.GetRoomsSchedule();
            var tableLessons = google.GetLessonsSchedule();
            var lessonPlanBuilder = new LessonPlanBuilderCore();
            var initializer = new Initializer(new TableParser(8));
            var lessons = initializer.InitializeSubjects(tableLessons, tableTeachers, tableClassrooms)
                .ToList();
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var lessonPlan = lessonPlanBuilder.GenerateLessonPlan(lessons,
                new GenerateSettings(5, 7, 8, 4));
            stopWatch.Stop();
            //var tables = ParserLessonPlanToOutputTable.ParseLessonPlanToTable(lessonPlan, 7, 8);
            //google.WriteToSheets(tables);
            Console.WriteLine("Время генерации: \n" + "Секунды: " + (stopWatch.ElapsedMilliseconds / 1000) + "\n"
                              + "Миллисекунды: " + stopWatch.ElapsedMilliseconds % 1000);
        }

        private static void test()
        {
            var teachers = Utils.CreateTeachers(8, 6, 7);
            var classrooms = Utils.CreateClassrooms(10, 6, 7);
            var listAvailableClassroom = Utils.CreateHashSetClassroom(classrooms, 4, 8);
            var subjects = Utils.CreateSubjectList(teachers, listAvailableClassroom);
            var lessons = Utils.CreateLessons(subjects);
            var gradeLessons = Utils.CreateGradeLessons(subjects, 3);
            var manager = Init(lessons);
            var lessonPlans = manager.GenerateLessonPlan(6, 7, 5);
            var lessonPlansStrings = ParserLessonPlanToOutputTable.ParseLessonPlanToTable(lessonPlans, 6, 7);
            /*foreach (var lessonPlan in lessonPlansStrings)
            {
                Utils.PrintTwoArray(lessonPlan);
            }*/
        }

        public static ManagerLessonBuilder Init(List<Lesson> lessons)
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
            container.Bind<ICellService<Lesson>>().To<CellService<Lesson>>();
            container.Bind<IRowManager<Lesson>>().To<RowManager<Lesson>>();
            var teachers = Utils.CreateTeachers(8, 6, 7);
            var classrooms = Utils.CreateClassrooms(10, 6, 7);
            var listAvailableClassroom = Utils.CreateHashSetClassroom(classrooms, 4, 8);
            var subjects = Utils.CreateSubjectList(teachers, listAvailableClassroom);
            container.Bind<IGeneratorSequenceItem<Subject, Lesson>>().ToConstant(new GeneratorSequenceItem(subjects));
            container.Bind<ITableManager<Lesson>>().To<TableManager<Lesson>>();
            container.Bind<Appraiser<Subject>>().ToConstant(new SubjectAppraiser(6, 7));
            container.Bind<ManagerLessonBuilder>().ToSelf();
            return container.Get<ManagerLessonBuilder>();
        }
    }
}