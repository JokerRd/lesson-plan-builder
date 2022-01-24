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
	public void CreateSimpleData()
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

	[Test]
	public void CreateComplicatedData()
	{
		var subjectsTable = new[,]
		{
			{ "Математика", "Поторочина К.С.", "12", "Обычная" },
			{ "Программирование", "Куликов E.Д.", "4", "Компьютерная" },
			{ "Физика", "Резник П.Л.", "6", "Лабаратория" },
			{ "Базы Данных", "Мельник К.В.", "3", "Компьютерная" }
		};
		var teachersTable = new[,]
		{
			{ "Поторочина К.С.", "1-6", "1-6", "1,2,3", null, "4-8", "1,2,5,6", null },
			{ "Куликов E.Д.", "3-7", null, null, null, null, null, null },
			{ "Резник П.Л.", "1-6", null, "1-6", "1-6", "1-6", null, null },
			{ "Мельник К.В.", null, null, null, "4,5", "4-5", "2-5", null }
		};
		var classroomsTable = new[,]
		{
			{ "Р-101", "Обычная", "1-8", "1-8", "1-8", " ", "1-6", "2-4", " " },
			{ "Р-102", "Компьютерная", "1-8", "1-8", "1-8", null, "1-6", null, null },
			{ "Р-103", "Компьютерная", "1-8", "1-8", "1-8", null, "1-6", null, null },
			{ "Р-104", "Обычная", "1-8", "1-8", "1-8", null, "1-6", "2-4", null },
			{ "Р-105", "Обычная", "1-8", "1-8", "1-8", null, "1-6", "2-4", null },
			{ "Р-106", "Лабаратория", "1-6", "1-6", "", "", "", "", "" },
			{ "Р-107", "Обычная", "1-8", "1-8", "1-8", "", "1-6", "2-4", "" },
		};

		var subjects = initializer.InitializeSubjects(subjectsTable, teachersTable!, classroomsTable!).ToList();
		Assert.AreEqual(subjects.Count, 4);
		Assert.AreEqual(subjects[0].Name, "Математика");
		Assert.AreEqual(subjects[1].Name, "Программирование");
		Assert.AreEqual(subjects[2].Name, "Физика");
		Assert.AreEqual(subjects[3].Name, "Базы Данных");
		Assert.AreEqual(subjects[0].Teacher.Name, "Поторочина К.С.");
		Assert.AreEqual(subjects[1].Teacher.Name, "Куликов E.Д.");
		Assert.AreEqual(subjects[2].Teacher.Name, "Резник П.Л.");
		Assert.AreEqual(subjects[3].Teacher.Name, "Мельник К.В.");
		Assert.AreEqual(subjects[0].LessonsCount, 12);
		Assert.AreEqual(subjects[1].LessonsCount, 4);
		Assert.AreEqual(subjects[2].LessonsCount, 6);
		Assert.AreEqual(subjects[3].LessonsCount, 3);
		Assert.AreEqual(subjects[0].ClassroomType, "Обычная");
		Assert.AreEqual(subjects[1].ClassroomType, "Компьютерная");
		Assert.AreEqual(subjects[2].ClassroomType, "Лабаратория");
		Assert.AreEqual(subjects[3].ClassroomType, "Компьютерная");
		Assert.AreEqual(subjects[0].AvailableClassrooms.Count, 4);
		Assert.AreEqual(subjects[1].AvailableClassrooms.Count, 2);
		Assert.AreEqual(subjects[2].AvailableClassrooms.Count, 1);
		Assert.AreEqual(subjects[3].AvailableClassrooms.Count, 2);
	}
}