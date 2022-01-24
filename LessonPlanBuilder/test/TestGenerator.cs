using System;
using System.Collections.Generic;
using LessonPlanBuilder.api.model;
using LessonPlanBuilder.core;
using LessonPlanBuilder.core.generators;
using LessonPlanBuilder.core.subjectAppraiser;
using NUnit.Framework;

namespace LessonPlanBuilder.test
{
    [TestFixture]
    public class TestGenerator
    {
        private SubjectAppraiser appraiser;
        private List<Subject> subjects;
        private GeneratorSequenceItem generator;
        
        [SetUp]
        public void Init()
        {
            appraiser = new SubjectAppraiser(5, 4);
            var teachers = Utils.CreateTeachers(8, 6, 7);
            var classrooms = Utils.CreateClassrooms(10, 6, 7);
            var listAvailableClassroom = Utils.CreateHashSetClassroom(classrooms, 4, 8);
            subjects = Utils.CreateSubjectList(teachers, listAvailableClassroom);
            generator = new GeneratorSequenceItem(subjects);
        }
        
        [Test]
        public void IsNotEmptyOutputQueue()
        {
            Assert.IsNotEmpty(generator.Generate(appraiser));
        }
        
        [Test]
        public void IsSeveralSchedulesAreNotEqual()
        {
            var queues = new List<Queue<Lesson>>();
            var rnd = new Random();
            var count = rnd.Next(1, 10);
            for (var i = 0; i < count; i++)
            {
                var queue = generator.Generate(appraiser);
                queues.Add(queue);
            }
            
            Assert.IsTrue(IsDifferentQueues(queues));
        }

        private bool IsDifferentQueues(List<Queue<Lesson>> queues)
        {
            for (var i = 0; i < queues.Count - 1; i++)
            {
                for (var j = i + 1; j < queues.Count; j++)
                {
                    if (queues[i].Equals(queues[j]))
                        return false;
                }
            }

            return true;
        }
    }
}