using LessonPlanBuilder.api.initializer;
using NUnit.Framework;

namespace LessonPlanBuilder.test.ApiTests;

[TestFixture]
public class TestTableParser
{
	private static TableParser parser;

	[SetUp]
	public void Init()
	{
		parser = new TableParser(8);
	}

	[TestCase("1")]
	[TestCase("3")]
	[TestCase("5")]
	[TestCase(" 1")]
	[TestCase("3 ")]
	[TestCase("	5 ")]
	[TestCase("		7	 ")]
	public void GetFreeLessons_OnOneValueWithCorrectData(string cell)
	{
		var actual = parser.GetFreeLessons(cell).ToArray();
		Assert.AreEqual(new[] { int.Parse(cell.Trim()) }, actual);
	}

	[TestCase("1,2")]
	[TestCase("2,4,5")]
	[TestCase("5, 6")]
	[TestCase("2, 1,  5,3")]
	public void GetFreeLessons_OnLotOfSinglesValueWithCorrectData(string cell)
	{
		var actual = parser.GetFreeLessons(cell).ToArray();
		var expected = cell.Split(",").Select(int.Parse).ToArray();
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void GetFreeLessons_OnSingleRangeWithCorrectData()
	{
		var actual = parser.GetFreeLessons("1-3").ToArray();
		Assert.AreEqual(new[] { 1, 2, 3 }, actual);
	}

	[TestCase("4-1")]
	[TestCase("0-1")]
	[TestCase("7-10")]
	[TestCase("3-2")]
	[TestCase("-1-2")]
	[TestCase("10-12")]
	public void GetFreeLessons_OnSingleRangeWithIncorrectData(string cell)
	{
		Assert.Throws<Exception>(() => parser.GetFreeLessons(cell), "Некорректные значения");
	}

	[Test]
	public void GetFreeLessons_OnRepeatedRangeWithCorrectData()
	{
		var actual = parser.GetFreeLessons("2-3, 5-7").ToArray();
		Assert.AreEqual(new[] { 2, 3, 5, 6, 7 }, actual);
	}

	[TestCase("-1")]
	[TestCase("0")]
	[TestCase("9")]
	[TestCase("10000000")]
	public void GetFreeLessons_OnOneValueWithIncorrectData(string cell)
	{
		Assert.Throws<Exception>(() => parser.GetFreeLessons(cell),
			"Значения, меньше нуля, или больше максимального количества проводимых уроков в день в аудитории недопустимы");
	}

	[TestCase("word")]
	[TestCase("1--2")]
	public void GetFreeLessons_OnIncorrectValue(string cell)
	{
		Assert.Throws<Exception>(() => parser.GetFreeLessons(cell), "Некорректные значения");
	}

	[TestCase("")]
	[TestCase(" ")]
	[TestCase("		")]
	[TestCase("    ")]
	[TestCase(null)]
	public void GetFreeLessons_OnEmptyData(string cell)
	{
		var actual = parser.GetFreeLessons(cell).ToArray();
		Assert.AreEqual(Array.Empty<int>(), actual);
	}
}