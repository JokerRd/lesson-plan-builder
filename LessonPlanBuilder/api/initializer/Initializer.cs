using System.Collections.Generic;
using System.Linq;
using LessonPlanBuilder.api.generator;
using LessonPlanBuilder.api.model;

namespace LessonPlanBuilder.api.initializer
{
	public static class Initializer
	{
		public static IEnumerable<Subject> Initialize(string[,] subjectsTable, string[,] teachersTable, string[,] classroomsTable)
		{
			var teachers = InitializeTeachers(teachersTable);
			var classrooms = InitializeClassrooms(classroomsTable);

			foreach (var column in GetColumns(subjectsTable, Generator.SubjectsRowsCount))
			{
				var subjectName = column[0];
				var teacherName = column[1];
				var lessonsCount = int.Parse(column[2]);
				var classroomType = column[3];

				yield return new Subject(subjectName, lessonsCount, classroomType, teachers[teacherName],
					classrooms[classroomType]);
			}
		}

		private static Dictionary<string, Teacher> InitializeTeachers(string[,] teachersTable)
		{
			return GetColumns(teachersTable, Generator.TeachersRowsCount)
				.Select(column => new Teacher(column[0], ParseSchedule(column[1..])))
				.ToDictionary(teacher => teacher.Name);
		}

		private static Dictionary<string, HashSet<Classroom>> InitializeClassrooms(string[,] classroomsTable)
		{
			return GetColumns(classroomsTable, Generator.ClassroomsRowsCount)
				.Select(column => new Classroom(column[0], column[1], ParseSchedule(column[2..])))
				.ToDictionary(classroom => classroom.ClassroomType, x => new HashSet<Classroom>());
		}

		private static IEnumerable<string[]> GetColumns(string[,] table, int rowsCount)
		{
			foreach (var columnIndex in Enumerable.Range(table.GetLength(1) - 1, 1))
			{
				var column = new string[rowsCount];
				foreach (var rowIndex in Enumerable.Range(rowsCount, 0))
				{
					column[rowIndex] = table[columnIndex, rowIndex];
				}

				yield return column;
			}
		}

		private static Schedule ParseSchedule(string[] column)
		{
			//TODO дописать парсеры, мб их в класс
			//TODO пробросать исключения не некорректный ввод
			return new Schedule(new ScheduleCell[1,1]);
		}
	}
}