using LessonPlanBuilder.api.model;
using LessonPlanBuilder.api.tableHeadingsGenerator;

namespace LessonPlanBuilder.api.initializer;

public class Initializer
{
	private readonly TableParser parser;

	public Initializer(TableParser parser)
	{
		this.parser = parser;
	}

	public IEnumerable<Subject> InitializeSubjects(string[,] subjectsTable, string[,] teachersTable,
		string[,] classroomsTable)
	{
		CheckIntegrity(subjectsTable, teachersTable, classroomsTable);
		var teachers = InitializeTeachers(teachersTable);
		var classrooms = InitializeClassrooms(classroomsTable);
		return GetColumns(subjectsTable).Select(column => TableParser.ParseSubject(column, teachers, classrooms));
	}

	private Dictionary<string, Teacher> InitializeTeachers(string[,] teachersTable)
	{
		return GetColumns(teachersTable)
			.Select(parser.ParseTeacher)
			.ToDictionary(teacher => teacher.Name);
	}

	private Dictionary<string, HashSet<Classroom>> InitializeClassrooms(string[,] classroomsTable)
	{
		return GetColumns(classroomsTable)
			.Select(parser.ParseClassroom)
			.ToDictionary(classroom => classroom.ClassroomType, _ => new HashSet<Classroom>());
	}

	private static IEnumerable<string[]> GetColumns(string[,] table)
	{
		var columnsCount = table.GetLength(0);
		var rowsCount = table.GetLength(1);

		if (columnsCount < 2)
			throw new Exception("В одной из входных таблиц нет данных");

		foreach (var columnIndex in Enumerable.Range(1, columnsCount - 1))
			yield return Enumerable.Range(0, rowsCount).Select(rowIndex => table[columnIndex, rowIndex]).ToArray();

		/* Если входная талица не содержит оглавление
		 if (columnsCount < 1)
			throw new Exception("В одной из входных таблиц нет данных");

		foreach (var columnIndex in Enumerable.Range(0, columnsCount)) // Если входная талица не содержит оглавление
			yield return Enumerable.Range(0, rowsCount).Select(rowIndex => table[columnIndex, rowIndex]).ToArray();
		*/
	}

	private static void CheckIntegrity(string[,] subjectsTable, string[,] teachersTable, string[,] classroomsTable)
	{
		if (subjectsTable.GetLength(1) != Generator.SubjectsRowsCount)
			throw new Exception(
				"Таблица предметов должна содержать 4 столбца: название предмета, ФИО преподавателя, количество занятий и тип аудитории");

		if (teachersTable.GetLength(1) != Generator.TeachersRowsCount)
			throw new Exception(
				"Таблица преподавателей должна содержать 8 столбцов: ФИО преподавателя и расписание на 7 дней недели");

		if (classroomsTable.GetLength(1) != Generator.ClassroomsRowsCount)
			throw new Exception(
				"Таблица аудиторий должна содержать 9 столбцов: название, тип и расписание на 7 дней недели");
	}
}