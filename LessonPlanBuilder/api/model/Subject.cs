using System.Collections.Generic;
using LessonPlanBuilder.api.model.enums;

namespace LessonPlanBuilder.api.model
{
	public class Subject
	{
		public readonly string Name;
		public readonly int LessonsCount;
		public readonly Teacher Teacher;
		public readonly string ClassroomType;
		public readonly HashSet<Classroom> AvailableClassrooms;

		public Subject(string name, int lessonsCount, string classroomType, Teacher teacher,
			HashSet<Classroom> availableClassrooms)
		{
			Name = name;
			LessonsCount = lessonsCount;
			ClassroomType = classroomType;
			Teacher = teacher;
			AvailableClassrooms = availableClassrooms;
		}
	}
}