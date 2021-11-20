using System.Collections.Generic;
using LessonPlanBuilder.api.model;

namespace LessonPlanBuilder.api
{

    /// <summary>
    /// Класс описывающий входные параметры
    /// </summary>
    public class LessonPlanParameters
    {
        public List<Lesson> Lessons { get; }

        public List<Teacher> Teachers { get; }

        public List<Room> Rooms { get; }

        public int CountLessonDay { get; }

        public LessonPlanParameters(List<Lesson> lessons, List<Teacher> teachers, List<Room> rooms, int countLessonDay)
        {
            Lessons = lessons;
            Teachers = teachers;
            Rooms = rooms;
            CountLessonDay = countLessonDay;
        }
    }
}