using System;
using System.Collections.Generic;
using System.Linq;
using LessonPlanBuilder.api.generator;
using LessonPlanBuilder.api.model;
using LessonPlanBuilder.api.model.enums;

namespace LessonPlanBuilder.api.initializer
{
	public static class TableParser
	{
		public static Subject ParseSubject(string[] column, IReadOnlyDictionary<string, Teacher> teachers,
			IReadOnlyDictionary<string, HashSet<Classroom>> classrooms)
		{
			if (column.Length != Generator.SubjectsRowsCount) throw new ArgumentException();
			return new Subject(column[0], int.Parse(column[2]), column[3], teachers[column[1]],
				classrooms[column[3]]);
		}

		public static Classroom ParseClassroom(string[] column)
		{
			if (column.Length != Generator.ClassroomsRowsCount) throw new ArgumentException();
			return new Classroom(column[0], column[1], ParseSchedule(column[2..]));
		}

		public static Teacher ParseTeacher(string[] column)
		{
			if (column.Length != Generator.TeachersRowsCount) throw new ArgumentException();
			return new Teacher(column[0], ParseSchedule(column[1..]));
		}

		private static Schedule ParseSchedule(string[] weeklySchedule)
		{
			var schedule = new Schedule(new ScheduleCell[7, 8]);

			foreach (var dayOfWeek in Enumerable.Range(1, 7))
			{
				foreach (var lessonNumber in ParseDailySchedule(weeklySchedule[dayOfWeek - 1]))
				{
					schedule[(DayOfWeek)dayOfWeek, lessonNumber] = ScheduleCell.Free;
				}
			}

			return schedule;
		}

		private static HashSet<LessonNumber> ParseDailySchedule(string cell)
		{
			var dailySchedule = new HashSet<LessonNumber>();
			var cellContent = cell.Split(",", StringSplitOptions.TrimEntries);
			foreach (var value in cellContent)
			{
				if (value.Length == 1)
				{
					dailySchedule.Add((LessonNumber)int.Parse(value));
				}
				else
				{
					var range = value.Split('-').Select(int.Parse).ToArray();
					if (range.Length != 2) throw new ArgumentException();
					foreach (var c in Enumerable.Range(range[0], range[1] - range[0]))
						dailySchedule.Add((LessonNumber)c);
				}
			}

			return dailySchedule;
		}
	}
}