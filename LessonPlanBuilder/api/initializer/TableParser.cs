using LessonPlanBuilder.api.model;
using LessonPlanBuilder.api.model.enums;
using LessonPlanBuilder.api.tableHeadingsGenerator;
using DayOfWeek = LessonPlanBuilder.api.model.enums.DayOfWeek;

namespace LessonPlanBuilder.api.initializer;

public static class TableParser
{
	public static Subject ParseSubject(string[] column,
		IReadOnlyDictionary<string, Teacher> teachers,
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

	public static Classroom ParseClassroom(string[] column)
	{
		var classroomName = column[0];
		var classroomType = column[1];

		if (!TryParseSchedule(column[2..], out var schedule))
			throw new Exception($"Не удалось распознать расписание для аудитории \"{classroomName}\"");

		return new Classroom(classroomName, classroomType, schedule);
	}

	public static Teacher ParseTeacher(string[] column)
	{
		var teacherName = column[0];

		if (!TryParseSchedule(column[2..], out var schedule))
			throw new Exception($"Не удалось распознать расписание для преподавателя \"{teacherName}\"");

		return new Teacher(teacherName, schedule);
	}

	private static bool TryParseSchedule(IReadOnlyList<string> weeklySchedule, out Schedule schedule)
	{
		var daysOfWeekCount = Enum.GetValues(typeof(DayOfWeek)).Length;
		schedule = new Schedule(new ScheduleCell[daysOfWeekCount, Generator.MaxLoadOfClassroomPerDay]);
		/*try
		{
			foreach (var dayOfWeek in Enumerable.Range(0, daysOfWeekCount))
			{
				var freeLessons = weeklySchedule[dayOfWeek].GetFreeLessons();
				foreach (var lesson in Enumerable.Range(0, Generator.MaxLoadOfClassroomPerDay))
				{
					schedule[(DayOfWeek)dayOfWeek, lesson] =
						freeLessons.Contains(lesson) ? ScheduleCell.Free : ScheduleCell.Impossible;
				}
			}

			return true;
		}

		catch (Exception e)
		{
			return false;
		}*/
		return true;
	}

	private static HashSet<int> GetFreeLessons(this string scheduleCell)
	{
		var freeCells = new HashSet<int>();
		var cellContent = scheduleCell.Split(",", StringSplitOptions.TrimEntries);
		foreach (var value in cellContent)
		{
			if (value.Length == 1)
			{
				freeCells.Add(int.Parse(value));
			}
			else
			{
				var range = value.Split('-').Select(int.Parse).ToArray();
				if (range.Length != 2) throw new ArgumentException();
				foreach (var c in Enumerable.Range(range[0], range[1] - range[0] + 1))
					freeCells.Add(c);
			}
		}

		return freeCells;
	}
}