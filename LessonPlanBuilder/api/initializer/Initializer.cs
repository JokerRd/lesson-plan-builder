using System.Collections.Generic;
using System.Linq;
using LessonPlanBuilder.api.model;
using LessonPlanBuilder.api.tableHeadingsGenerator;

namespace LessonPlanBuilder.api.initializer
{
	public class Initializer
	{
		private readonly TableParser tableParser;

		public Initializer(TableParser tableParser)
		{
			this.tableParser = tableParser;
		}

		public IEnumerable<Subject> Initialize(string[,] subjectsTable, string[,] teachersTable,
			string[,] classroomsTable)
		{
			var teachers = InitializeTeachers(teachersTable);
			var classrooms = InitializeClassrooms(classroomsTable);
			foreach (var column in GetColumns(subjectsTable, Generator.SubjectsRowsCount))
				yield return TableParser.ParseSubject(column, teachers, classrooms);
		}

		private Dictionary<string, Teacher> InitializeTeachers(string[,] teachersTable)
		{
			return GetColumns(teachersTable, Generator.TeachersRowsCount)
				.Select(tableParser.ParseTeacher)
				.ToDictionary(teacher => teacher.Name);
		}

		private Dictionary<string, HashSet<Classroom>> InitializeClassrooms(string[,] classroomsTable)
		{
			return GetColumns(classroomsTable, Generator.ClassroomsRowsCount)
				.Select(tableParser.ParseClassroom)
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