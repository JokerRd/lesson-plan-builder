using System.Linq;
using LessonPlanBuilder.api.model;
using LessonPlanBuilder.api.model.enums;

namespace LessonPlanBuilder.core.subjectAppraiser
{
	public static class SubjectAppraiser
	{
		/// <summary>
		/// Чем меньше значение, тем сложнее поставить предмет в рассписание
		/// </summary>
		public static double AppraiseSubject(Subject subject)
		{
			return (
				from day in Enumerable.Range(0, 7)
				from lesson in Enumerable.Range(0, 8)
				where subject.Teacher.Schedule[day, lesson] != ScheduleCell.Impossible
				from classroom in subject.AvailableClassrooms
				where classroom.Schedule[day, lesson] != ScheduleCell.Impossible
				select day).Count() / (double)subject.LessonsCount;
		}
	}
}