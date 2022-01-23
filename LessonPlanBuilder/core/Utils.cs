using LessonPlanBuilder.api.model;
using LessonPlanBuilder.api.model.enums;
using LessonPlanBuilder.core.model;
using LessonPlanBuilder.core.restrictions;
using LessonPlanBuilder.core.services;
using LessonPlanBuilder.test;

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
        for (var i = 0; i < countRows; i++)
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
        PutRandomInTwoArray(table,
            () => (ScheduleCell)random.Next(2));
        return table;
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
            teachers.Add(CreateTeacher(i, table));
        }

        return teachers;
    }

    public static Teacher CreateTeacher(int index, ScheduleCell[,] table)
    {
        return new Teacher("Учитель" + (index + 1),
            new Schedule(table));
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

    public static List<Subject> CreateSubjectList(List<Teacher> teachers, List<HashSet<Classroom>> classrooms)
    {
        var subjects = new List<Subject>();
        var random = new Random();
        for (var index = 0; index < teachers.Count; index++)
        {
            var teacher = teachers[index];
            subjects.Add(CreateSubject(index, random, teacher, classrooms[index]));
        }

        return subjects;
    }

    public static Subject CreateSubject(int index, Random random, Teacher teacher, HashSet<Classroom> classroom)
    {
        return new Subject("Предмет" + (index + 1), random.Next(1, 5),
            ClassRoomType[random.Next(3)], teacher, classroom);
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
                lessons.Add(CreateLesson(subject, i));
            }
        }

        return lessons;
    }

    public static Lesson CreateLesson(Subject subject, int index)
    {
        return new Lesson(subject, index + 1);
    }

    public static Dictionary<Subject, int> CreateGradeLessons(List<Subject> subjects, int maxGrade)
    {
        var random = new Random();
        return subjects
            .ToDictionary(subject => subject, subject => random.Next(1, maxGrade));
    }
    
    public static CellService<Lesson> CreateCellServiceFilledRestrictions(Func<IRestrictionOnCell<Lesson>> generator,
        int countRestrictions, Action<List<IRestrictionOnCell<Lesson>>> addedRestrictions)
    {
        var restrictions = new List<IRestrictionOnCell<Lesson>>();
        for (var i = 0; i < countRestrictions; i++)
        {
            restrictions.Add(generator.Invoke());
        }
        addedRestrictions.Invoke(restrictions);
        return new CellService<Lesson>(restrictions);
    }

    public static Cell<Lesson>[] CreateCells(int countCell)
    {
        var cells = new Cell<Lesson>[countCell];
        for (var i = 0; i < countCell; i++)
        {
            cells[i] = new Cell<Lesson>(i);
        }

        return cells;
    }


    public static List<Row<Lesson>> CreateListRowLesson(int countDay, int countLesson)
    {
        var result = new List<Row<Lesson>>();
        for (var i = 0; i < countDay; i++)
        {
            var cells = CreateCells(countLesson);
            result.Add(new Row<Lesson>(cells, i + 1));
        }

        return result;
    }


    public static List<Lesson> GenerateLessons(InitLessonsData initLessonsData)
    {
        var teachers = CreateTeachers(initLessonsData.CountLessons, 
            initLessonsData.CountDays, initLessonsData.CountLessonInDay);
        var classrooms = CreateClassrooms(initLessonsData.CountTypeClassroom,
            initLessonsData.CountDays, initLessonsData.CountLessonInDay);
        var listAvailableClassroom = CreateHashSetClassroom(classrooms, 
            initLessonsData.CountListHashSet, initLessonsData.CountListHashSet);
        var subjects = CreateSubjectList(teachers, listAvailableClassroom);
        return CreateLessons(subjects);
    }

    public static Queue<Lesson> CreateQueue(InitLessonsData initLessonsData)
    {
        var lessons = GenerateLessons(initLessonsData);
        var queue = new Queue<Lesson>();
        foreach (var lesson in lessons)
        {
            queue.Enqueue(lesson);
        }

        return queue;
    }
}