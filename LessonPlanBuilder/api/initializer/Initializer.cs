using System.Collections.Generic;
using System.Linq;
using LessonPlanBuilder.api.generator;
using LessonPlanBuilder.api.model;

namespace LessonPlanBuilder.api.initializer
{
	public static class Initializer
	{
		public static IEnumerable<Subject> Initialize(string[,] subjectsTable, string[,] teachersTable,
			string[,] classroomsTable)
		{
			var teachers = InitializeTeachers(teachersTable);
			var classrooms = InitializeClassrooms(classroomsTable);
			foreach (var column in GetColumns(subjectsTable, Generator.SubjectsRowsCount))
				yield return TableParser.ParseSubject(column, teachers, classrooms);
		}

		private static Dictionary<string, Teacher> InitializeTeachers(string[,] teachersTable)
		{
			return GetColumns(teachersTable, Generator.TeachersRowsCount)
				.Select(TableParser.ParseTeacher)
				.ToDictionary(teacher => teacher.Name);
		}

		private static Dictionary<string, HashSet<Classroom>> InitializeClassrooms(string[,] classroomsTable)
		{
			return GetColumns(classroomsTable, Generator.ClassroomsRowsCount)
				.Select(TableParser.ParseClassroom)
				.ToDictionary(classroom => classroom.ClassroomType, _ => new HashSet<Classroom>());
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
	}
}