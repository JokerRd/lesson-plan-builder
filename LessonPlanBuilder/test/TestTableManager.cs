using LessonPlanBuilder.api.model;
using LessonPlanBuilder.core;
using LessonPlanBuilder.core.managers;
using LessonPlanBuilder.core.model;
using LessonPlanBuilder.core.restrictions;
using LessonPlanBuilder.core.services;
using LessonPlanBuilder.test.utils;
using NUnit.Framework;

namespace LessonPlanBuilder.test;

[TestFixture]
public class TestTableManager
{
    private Shifter<Lesson> shifter;

    private CellService<Lesson> cellService;

    private RowService<Lesson> rowService;

    private RowManager<Lesson> rowManager;

    private TableManager<Lesson> tableManager;

    private List<Row<Lesson>> table;
    
    private Queue<Lesson> queueLesson;
    
    [SetUp]
    public void Init()
    {
        shifter = new Shifter<Lesson>();
        cellService = new CellService<Lesson>(new List<IRestrictionOnCell<Lesson>>()
        {
            new TestTrueRestrictions()
        });
        rowService = new RowService<Lesson>(new List<IRestrictionOnRow<Lesson>>()
        {
            new TestTrueRestrictionRow()
        });
        rowManager = new RowManager<Lesson>(shifter, cellService);
        tableManager = new TableManager<Lesson>(rowManager, rowService);
        table = Utils.CreateListRowLesson(4, 4);
        queueLesson = Utils.CreateQueue(new InitLessonsData(4, 4, 4,
            4, 4, 4));
    }

    [Test]
    public void IsTrueIfLessonMatchForRestrictions()
    {
        Assert.IsTrue(tableManager.PutItemsInTable(table, queueLesson));
    }

    [Test]
    public void IsFalseIfLessonNotMatchForRestrictions()
    {
        var newTableManager = new TableManager<Lesson>(rowManager,
            new RowService<Lesson>(new List<IRestrictionOnRow<Lesson>>()
            {
                new TestFalseRestrictionRow()
            }));
        Assert.IsFalse(newTableManager.PutItemsInTable(table, queueLesson));
    }


}