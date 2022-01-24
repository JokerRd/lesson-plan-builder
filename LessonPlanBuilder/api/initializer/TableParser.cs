using LessonPlanBuilder.api.model;
using LessonPlanBuilder.api.model.enums;
using DayOfWeek = LessonPlanBuilder.api.model.enums.DayOfWeek;

namespace LessonPlanBuilder.api.initializer;

public class TableParser
{
	private readonly int maxLoadOfClassroomPerDay;

	public TableParser(int maxLoadOfClassroomPerDay)
	{
		this.maxLoadOfClassroomPerDay = maxLoadOfClassroomPerDay;
	}

	public static Subject ParseSubject(string[] column, IReadOnlyDictionary<string, Teacher> teachers,
		IReadOnlyDictionary<string, HashSet<Classroom>> classrooms)
	{
		var subjectName = column[0];
		var teacherName = column[1];
		var classroomType = column[3];

		if (!int.TryParse(column[2], out var lessonsCount))
			throw new Exception(
				$"Для предмета \"{subjectName}\" не распознано \"{column[2]}\", как колличество предметов");

		if (!teachers.TryGetValue(teacherName, out var teacher))
			throw new Exception(
				$"Преподаватель с именем \"{teacherName}\" из таблицы предметов не найден в таблице преподавателей");

		if (!classrooms.TryGetValue(classroomType, out var availableClassrooms))
			throw new Exception(
				$"Аудитория с типом \"{classroomType}\" из таблицы предметов не найдена в таблице аудиторий");

		return new Subject(subjectName, lessonsCount, classroomType, teacher, availableClassrooms);
	}

	public Classroom ParseClassroom(string[] column)
	{
		var classroomName = column[0];
		var classroomType = column[1];

		try
		{
			var schedule = ParseSchedule(column[2..]);
			return new Classroom(classroomName, classroomType, schedule);
		}
		catch (Exception e)
		{
			throw new Exception($"Не удалось распознать расписание для аудитории \"{classroomName}\". {e.Message}");
		}
	}

	public Teacher ParseTeacher(string[] column)
	{
		var teacherName = column[0];

		try
		{
			var schedule = ParseSchedule(column[1..]);
			return new Teacher(teacherName, schedule);
		}
		catch (Exception e)
		{
			throw new Exception($"Не удалось распознать расписание для преподавателя \"{teacherName}\". {e.Message}",
				e);
		}
	}

	private Schedule ParseSchedule(IReadOnlyList<string> weeklySchedule)
	{
		var schedule = new Schedule(new ScheduleCell[7, maxLoadOfClassroomPerDay]);

		foreach (var dayOfWeek in Enumerable.Range(0, 7))
		{
			var freeLessons = GetFreeLessons(weeklySchedule[dayOfWeek]);
			foreach (var lesson in Enumerable.Range(0, maxLoadOfClassroomPerDay))
			{
				schedule[(DayOfWeek)dayOfWeek, lesson] =
					freeLessons.Contains(lesson) ? ScheduleCell.Free : ScheduleCell.Impossible;
			}
		}

		return schedule;
	}

	public HashSet<int> GetFreeLessons(string scheduleCell)
	{

		
		var freeCells = new HashSet<int>();
		if (scheduleCell is null || scheduleCell.Trim() == "") return freeCells;
		
		var cellContent = scheduleCell.Split(",", StringSplitOptions.TrimEntries);
		
		foreach (var value in cellContent)
		{
			if (value.Length == 1)
			{
				freeCells.Add(ParseValue(value, scheduleCell));
			}

			else
			{
				var range = value.Split('-').Select(v => ParseValue(v, scheduleCell)).ToArray();

				if (range.Length != 2 || range[1] - range[0] <= 0)
					throw new Exception($"Не удалось распознать значение в ячейке \"{scheduleCell}\"");

				foreach (var c in Enumerable.Range(range[0], range[1] - range[0] + 1))
					freeCells.Add(c);
			}
		}

		return freeCells;
	}

	private int ParseValue(string value, string scheduleCell)
	{
		if (!int.TryParse(value, out var parsedValue))
			throw new Exception($"Не удалось распознать значение в ячейке \"{scheduleCell}\"");
		if (parsedValue < 1 || parsedValue > maxLoadOfClassroomPerDay)
			throw new Exception($"Значение в ячейке \"{scheduleCell}\" недопустимо");
		return parsedValue;
	}
}