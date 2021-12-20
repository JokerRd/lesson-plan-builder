using System;
using System.Collections.Generic;
using System.Linq;
using LessonPlanBuilder.api.model;
using LessonPlanBuilder.api.model.enums;
using LessonPlanBuilder.api.tableHeadingsGenerator;

namespace LessonPlanBuilder.api.initializer
{
	public class TableParser
	{
		public readonly int SchoolDaysPerWeek;
		public readonly int LessonsPerDay;

		public TableParser(int schoolDaysPerWeek, int lessonsPerDay)
		{
			SchoolDaysPerWeek = schoolDaysPerWeek;
			LessonsPerDay = lessonsPerDay;
		}
		
		public static Subject ParseSubject(string[] column, IReadOnlyDictionary<string, Teacher> teachers,
			IReadOnlyDictionary<string, HashSet<Classroom>> classrooms)
		{
			if (column.Length != Generator.SubjectsRowsCount) throw new ArgumentException();
			return new Subject(column[0], int.Parse(column[2]), column[3], teachers[column[1]],
				classrooms[column[3]]);
		}

		public Classroom ParseClassroom(string[] column)
		{
			if (column.Length != Generator.ClassroomsRowsCount) throw new ArgumentException();
			return new Classroom(column[0], column[1], ParseSchedule(column[2..]));
		}

		public Teacher ParseTeacher(string[] column)
		{
			if (column.Length != Generator.TeachersRowsCount) throw new ArgumentException();
			return new Teacher(column[0], ParseSchedule(column[1..]));
		}

		private Schedule ParseSchedule(string[] weeklySchedule)
		{
			var schedule = new Schedule(new ScheduleCell[SchoolDaysPerWeek, LessonsPerDay]);

			foreach (var dayOfWeek in Enumerable.Range(1, SchoolDaysPerWeek))
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