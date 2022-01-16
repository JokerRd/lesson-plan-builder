using LessonPlanBuilder.api.model;
using LessonPlanBuilder.core;
using LessonPlanBuilder.core.restrictions;
using LessonPlanBuilder.core.services;
using LessonPlanBuilder.test.utils;
using NUnit.Framework;

namespace LessonPlanBuilder.test;

[TestFixture]
public class TestCellService
{
    [Test]
    public void IsExceptionIfRestrictionsIsNullInCellService()
    {
        Assert.Throws(typeof(ArgumentException), () => new CellService<Lesson>(null));
    }

    [Test]
    public void IsExceptionIfRestrictionsIsEmptyInCellService()
    {
        Assert.Throws(typeof(ArgumentException), () =>
            new CellService<Lesson>(new List<IRestrictionOnCell<Lesson>>()));
    }

    [Test]
    public void IsTrueIfAllRestrictionsReturnTrue()
    {
        var cellService = Utils.CreateCellServiceFilledRestrictions(() => new TestTrueRestrictions(), 
            10, _ => { });
        Assert.IsTrue(cellService.IsPutInCell(null, null, 0));
    }

    [Test]
    public void IsFalseIfOneRestrictionsReturnFalse()
    {
        var cellService = Utils.CreateCellServiceFilledRestrictions(() => new TestTrueRestrictions(), 
            10, list => list.Add(new TestFalseRestrictions()));
        Assert.IsFalse(cellService.IsPutInCell(null, null, 0));
    }
    
    [Test]
    public void IsFalseIfAllRestrictionsReturnFalse()
    {
        var cellService = Utils.CreateCellServiceFilledRestrictions(() => new TestFalseRestrictions(), 
            10, _ => { });
        Assert.IsFalse(cellService.IsPutInCell(null, null, 0));
    }
    

}