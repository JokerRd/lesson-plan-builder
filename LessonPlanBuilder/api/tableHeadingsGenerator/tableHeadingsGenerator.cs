namespace LessonPlanBuilder.api.tableHeadingsGenerator;

public static class Generator
{
	// TODO Эти константы должны быть в настройках, их использует "api/initializer/Initializer.cs"
	public const int TeachersRowsCount = 8;
	public const int ClassroomsRowsCount = 9;
	public const int SubjectsRowsCount = 4;

	// TODO Эту константу должен задавать пользователь
	public const int MaxLoadOfClassroomPerDay = 8;

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
		return new[] { "Название предмета", "ФИО Преподавателя", "Количество занятий", "Тип аудитории" };
	}
}