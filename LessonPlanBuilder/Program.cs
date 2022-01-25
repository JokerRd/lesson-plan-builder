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
                new GenerateSettings(5, 7, 8, 3));
            stopWatch.Stop();
            Console.WriteLine("Время генерации: \n" + "Секунды: " + (stopWatch.ElapsedMilliseconds / 1000) + "\n"
                              + "Миллисекунды: " + stopWatch.ElapsedMilliseconds % 1000);
            var tables = ParserLessonPlanToOutputTable.ParseLessonPlanToTable(lessonPlan, 7, 8);
            google.WriteToSheets(tables);
        }
    }
}