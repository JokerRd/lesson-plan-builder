using LessonPlanBuilder.api.model;
using LessonPlanBuilder.api.model.enums;
using LessonPlanBuilder.core;
using LessonPlanBuilder.core.model;
using LessonPlanBuilder.core.services;
using LessonPlanBuilder.test.utils;
using NUnit.Framework;

namespace LessonPlanBuilder.test;

[TestFixture]
public class TestRowManager
{
    private Shifter<Lesson> shifter;

    private CellService<Lesson> cellService;
    
    private Lesson testLesson;

    private Row<Lesson> row;

    [SetUp]
    public void Init()
    {
        shifter = new Shifter<Lesson>();
        cellService = Utils
            .CreateCellServiceFilledRestrictions(() => new TestTrueRestrictions(),
                10, _ => { });
        testLesson = Utils.CreateLesson(Utils.CreateSubject(0, new Random(),
            Utils.CreateTeacher(0, new ScheduleCell[1, 1]),
            new HashSet<Classroom>()), 0);
        row = new Row<Lesson>(Utils.CreateCells(7), 0);
    }


    [Test]
    public void IsCorrectResultIfPuttItemInLastCellInRow()
    {
        var rowManager = new RowManager<Lesson>(shifter, cellService);
        var result = rowManager.TryPutItemInRow(testLesson, row, 7, () => { });
        Assert.IsFalse(result.IsPut);
    }
    
    [Test]
    public void IsCorrectResultIfPuttItemInFirstCellInRow()
    {
        var rowManager = new RowManager<Lesson>(shifter, cellService);
        var result = rowManager.TryPutItemInRow(testLesson, row, 0, () => { });
        Assert.IsTrue(result.IsPut);
    }
    
    [Test]
    public void IsCorrectResultIfCanNotPutItemInRow()
    {
        var service = Utils
            .CreateCellServiceFilledRestrictions(() => new TestFalseRestrictions(),
                10, _ => { });
        var rowManager = new RowManager<Lesson>(shifter, service);
        var result = rowManager.TryPutItemInRow(testLesson, row, 0, () => { });
        Assert.IsFalse(result.IsPut);
    }
    
}