using LessonPlanBuilder.api.model;
using LessonPlanBuilder.core;
using LessonPlanBuilder.core.services;
using NUnit.Framework;

namespace LessonPlanBuilder.test;

[TestFixture]
public class TestTableManager
{
    private Shifter<Lesson> shifter;

    private CellService<Lesson> cellService;

    private RowService<Lesson> rowService;

    [SetUp]
    public void Init()
    {
        
    }
}