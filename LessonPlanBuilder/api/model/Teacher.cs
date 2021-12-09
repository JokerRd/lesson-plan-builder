namespace LessonPlanBuilder.api.model
{
	public class Teacher
	{
		public readonly string Name;
		public readonly Schedule Schedule;

		public Teacher(string name, Schedule schedule)
		{
			Schedule = schedule;
			Name = name;
		}
	}
}