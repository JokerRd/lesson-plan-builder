
namespace LessonPlanBuilder.api.model
{
    public class Lesson
    {
        public string Name { get; }

        public int Count { get; }

        public Teacher Teacher { get; }

        public Lesson(string name, int count, Teacher teacher)
        {
            Name = name;
            Count = count;
            Teacher = teacher;
        }

        public Lesson()
        {
        }
    }
}