namespace LessonPlanBuilder.api.model
{
    public class LessonInfo
    {
        public string Name { get; }

        public string NumberRoom { get; }

        public string NameTeacher { get; }

        public LessonInfo(string name, string numberRoom, string nameTeacher)
        {
            Name = name;
            NumberRoom = numberRoom;
            NameTeacher = nameTeacher;
        }
    }
}