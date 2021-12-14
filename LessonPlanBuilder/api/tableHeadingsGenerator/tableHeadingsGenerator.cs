using System.Linq;

namespace LessonPlanBuilder.api.generator
{
	public static class Generator
	{
		public const int TeachersRowsCount = 8;
		public const int ClassroomsRowsCount = 9;
		public const int SubjectsRowsCount = 4;
		
		private static readonly string[] DaysOfWeek =
			{ "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" };

		public static string[] GenerateFirstColumnForTeachers()
		{
			return new[] { "ФИО Преподавателя" }.Concat(DaysOfWeek).ToArray();
		}

		public static string[] GenerateFirstColumnForClassrooms()
		{
			return new[] { "Название аудитории", "Тип аудитории" }.Concat(DaysOfWeek).ToArray();
		}
		
		public static string[] GenerateFirstColumnForSubjects()
		{
			return new[] { "Название предмета", "ФИО Преподавателя", "Количество пар", "Тип аудитории" };
		}
	}
}