namespace LessonPlanBuilder.api.model
{
	public class Classroom
	{
		public readonly string Name;
		public readonly Schedule Schedule;
		public readonly string ClassroomType;

		public Classroom(string name, string classroomType, Schedule schedule)
		{
			Name = name;
			ClassroomType = classroomType;
			Schedule = schedule;
		}
	}
}