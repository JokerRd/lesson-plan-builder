using LessonPlanBuilder.api.model;
using LessonPlanBuilder.api.model.enums;

namespace LessonPlanBuilder.core;

public class Utils
{

    private static List<string> ClassRoomType = new ()
    {
        "Лаборатория",
        "Обычная",
        "Компьютерный класс"
    };
    
    public static void PrintTwoArray<T>(T[,] array)
    {
        var countColumns = array.GetLength(0);
        var countRows = array.GetLength(1);
        for (int i = 0; i < countRows; i++)
        {
            for (int j = 0; j < countColumns; j++)
            {
                Console.Write(array[j, i]);
                Console.Write(" ");
            }

            Console.WriteLine();
        }
    }

    public static void PutRandomInTwoArray<T>(T[,] array, Func<T> getRandomItem)
    {
        var countColumns = array.GetLength(0);
        var countRows = array.GetLength(1);
        for (int i = 0; i < countColumns; i++)
        {
            for (int j = 0; j < countRows; j++)
            {
                array[i, j] = getRandomItem();
            }

            Console.WriteLine();
        }
    }

    public static ScheduleCell[,] CreateScheduleCells(int countDays, int countLessons)
    {
        var table = new ScheduleCell[countDays, countLessons];
        var random = new Random();
        Utils.PutRandomInTwoArray(table,
            () => (ScheduleCell)random.Next(2));
        return table;

        return CreateFreeScheduleCells(countDays, countLessons);
    }

    private static ScheduleCell[,] CreateFreeScheduleCells(int countDays, int countLessons)
    {
        var table = new ScheduleCell[countDays, countLessons];
        return table;
    }

    public static List<Teacher> CreateTeachers(int countTeacher, int countDays, int countLessons)
    {
        var teachers = new List<Teacher>();
        for (var i = 0; i < countTeacher; i++)
        {
            var table = CreateScheduleCells(countDays, countLessons);
            PrintTwoArray(table);
            teachers.Add(new Teacher("Учитель" + (i + 1),
                new Schedule(table)));
        }

        return teachers;
    }

    public static List<Classroom> CreateClassrooms(int countClassrooms, int countDays, int countLessons)
    {
        var classrooms = new List<Classroom>();
        var random = new Random();
        for (var i = 0; i < countClassrooms; i++)
        {
            var table = CreateScheduleCells(countDays, countLessons);
            PrintTwoArray(table);
            classrooms.Add(new Classroom("Аудитория" + (i + 1),
                ClassRoomType[random.Next(3)],
                new Schedule(table)));
        }

        
        return classrooms;
    }

    public static List<Subject> CreateSubject(List<Teacher> teachers, List<HashSet<Classroom>> classrooms)
    {
        var subjects = new List<Subject>();
        var random = new Random();
        for (var index = 0; index < teachers.Count; index++)
        {
            var teacher = teachers[index];
            subjects.Add(new Subject("Предмет" + (index + 1), random.Next(1, 5),
                ClassRoomType[random.Next(3)], teacher, classrooms[index] ));
        }

        return subjects;
    }

    public static List<HashSet<Classroom>> CreateHashSetClassroom(List<Classroom> classroomsInfo,
        int countClassroomInHashSet, int countListHashSet)
    {
        var classrooms = new List<HashSet<Classroom>>();
        var random = new Random();
        for (var i = 0; i < countListHashSet; i++)
        {
            var set = new HashSet<Classroom>();
            for (var j = 0; j < countClassroomInHashSet; j++)
            {
                var classRoom = classroomsInfo[random.Next(classroomsInfo.Count)];
                if (!set.Contains(classRoom))
                {
                    set.Add(classRoom);
                }
            }
            classrooms.Add(set);
        }

        return classrooms;
    }

    public static List<Lesson> CreateLessons(List<Subject> subjects)
    {
        var lessons = new List<Lesson>();
        foreach (var subject in subjects)
        {
            for (var i = 0; i < subject.LessonsCount; i++)
            {
                lessons.Add(new Lesson(subject, i + 1));
            }
        }

        return lessons;
    }

    public static Dictionary<Subject, int> CreateGradeLessons(List<Subject> subjects, int maxGrade)
    {
        var random = new Random();
        return subjects
            .ToDictionary(subject => subject, subject => random.Next(1, maxGrade));
    }
}