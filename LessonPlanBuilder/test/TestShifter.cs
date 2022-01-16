using LessonPlanBuilder.api.model;
using LessonPlanBuilder.api.model.enums;
using LessonPlanBuilder.core;
using LessonPlanBuilder.core.model;
using NUnit.Framework;

namespace LessonPlanBuilder.test
{
    [TestFixture]
    public class TestShifter
    {
        private Shifter<Lesson> testShifter;
        private Lesson testLesson;

        [SetUp]
        public void SetUp()
        {
            testShifter = new Shifter<Lesson>();
            testLesson = Utils.CreateLesson(Utils.CreateSubject(0, new Random(),
                Utils.CreateTeacher(0, new ScheduleCell[1, 1]),
                new HashSet<Classroom>()), 0);
        }

        [Test]
        public void IsShifterPutInCell()
        {
            var cell = new Cell<Lesson>(1);
            testShifter.PutInCell(testLesson, cell);
            Assert.IsFalse(cell.IsEmpty);
        }

        [Test]
        public void IsShifterTakeFromCell()
        {
            var cell = new Cell<Lesson>(testLesson,1);
            testShifter.TakeFromCell(cell);
            Assert.IsTrue(cell.IsEmpty);
        }
    }
}