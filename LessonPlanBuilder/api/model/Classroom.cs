using LessonPlanBuilder.api.model.enums;

namespace LessonPlanBuilder.api.model
{
	public class Classroom
	{
		public readonly string Name;
		public readonly Schedule Schedule;
		public readonly ClassroomType Type;

		public Classroom(string name, ClassroomType type, Schedule schedule)
		{
			Name = name;
			Type = type;
			Schedule = schedule;
		}
	}
}