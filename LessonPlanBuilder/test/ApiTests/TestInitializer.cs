using LessonPlanBuilder.api.initializer;
using LessonPlanBuilder.api.model.enums;
using NUnit.Framework;
using DayOfWeek = LessonPlanBuilder.api.model.enums.DayOfWeek;

namespace LessonPlanBuilder.test.ApiTests;

[TestFixture]
public class TestInitializer
{
	private static Initializer initializer;

	[SetUp]
	public void Init()
	{
		initializer = new Initializer(new TableParser(8));
	}

	[Test]
	public void Create()
	{
		var subjectsTable = new[,]
		{
			// { "Название предмета", "ФИО Преподавателя", "Количество пар", "Тип аудитории" },
			{ "Математика", "Пётр Петрович", "2", "Обычная" }
		};
		var teachersTable = new[,]
		{
			// { "ФИО Преподавателя", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" },
			{ "Пётр Петрович", "1-2", "2,4", "1-5", "4", null, null, null }
		};
		var classroomsTable = new[,]
		{
			// { "Название аудитории", "Тип аудитории", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" },
			{ "Р-100", "Обычная", "1-3", null, null, null, null, null, null }
		};

		var subjects = initializer.InitializeSubjects(subjectsTable, teachersTable!, classroomsTable!).ToList();
		var mathematics = subjects.First();
		Assert.AreEqual(subjects.Count, 1);
		Assert.AreEqual(mathematics.Teacher.Name, "Пётр Петрович");
		Assert.AreEqual(mathematics.Teacher.Schedule[DayOfWeek.Monday, 0], ScheduleCell.Free);
		Assert.AreEqual(mathematics.Teacher.Schedule[DayOfWeek.Monday, 2], ScheduleCell.Impossible);
		Assert.AreEqual(mathematics.Name, "Математика");
		Assert.AreEqual(mathematics.LessonsCount, 2);
		Assert.AreEqual(mathematics.AvailableClassrooms.First().Name, "Р-100");
		Assert.AreEqual(mathematics.AvailableClassrooms.First().Schedule[DayOfWeek.Monday, 0], ScheduleCell.Free);
		Assert.AreEqual(mathematics.AvailableClassrooms.First().Schedule[DayOfWeek.Monday, 3], ScheduleCell.Impossible);
	}
}