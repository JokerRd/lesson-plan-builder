using LessonPlanBuilder.api.initializer;
using NUnit.Framework;

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
			{ "Название предмета", "ФИО Преподавателя", "Количество пар", "Тип аудитории" },
			{ "Математика", "Вася", "1", "Обычная" },
		};
		var teachersTable = new[,]
		{
			{ "ФИО Преподавателя", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" },
			{ "Вася", "1", "1", "1", "1", "1", "1", "1" }
		};
		var classroomsTable = new[,]
		{
			{
				"Название аудитории", "Тип аудитории", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница",
				"Суббота", "Воскресенье"
			},
			{ "Р-100", "Обычная", "1", "1", "1", "1", "1", "1", "1" }
		};

		var subjects = initializer.InitializeSubjects(subjectsTable, teachersTable, classroomsTable).ToList();

		Assert.AreEqual(subjects.Count, 1);
	}
}