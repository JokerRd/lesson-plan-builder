using System.Linq;
using LessonPlanBuilder.api.model;
using LessonPlanBuilder.api.model.enums;

namespace LessonPlanBuilder.core.subjectAppraiser
{
	public class SubjectAppraiser
	{
		private readonly int schoolDaysPerWeek;
		private readonly int lessonsPerDay;

		public SubjectAppraiser(int schoolDaysPerWeek, int lessonsPerDay)
		{
			this.schoolDaysPerWeek = schoolDaysPerWeek;
			this.lessonsPerDay = lessonsPerDay;
		}

		/// <summary>
		/// Чем меньше значение, тем сложнее поставить предмет в рассписание
		/// </summary>
		public double AppraiseSubject(Subject subject)
		{
			return (
				from day in Enumerable.Range(0, schoolDaysPerWeek)
				from lesson in Enumerable.Range(0, lessonsPerDay)
				where subject.Teacher.Schedule[day, lesson] != ScheduleCell.Impossible
				from classroom in subject.AvailableClassrooms
				where classroom.Schedule[day, lesson] != ScheduleCell.Impossible
				select day).Count() / (double)subject.LessonsCount;
		}
	}
}