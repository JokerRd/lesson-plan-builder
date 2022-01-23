using LessonPlanBuilder.api.initializer;
using NUnit.Framework;

namespace LessonPlanBuilder.test;

[TestFixture]
public class TestInitializer
{
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
			{ "Вася", "1-3", "", "", "", "", "", ""}
		};
		var classroomsTable = new[,]
		{
			{
				"Название аудитории", "Тип аудитории", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница",
				"Суббота", "Воскресенье"
			},
			{ "Р-100", "Обычная", "1-3", "1-3", "1-3", "1-3", "1-3", "1", "1" }
		};

		var subjects = Initializer.InitializeSubjects(subjectsTable, teachersTable, classroomsTable).ToList();

		Assert.AreEqual(subjects.Count, 1);
	}
}