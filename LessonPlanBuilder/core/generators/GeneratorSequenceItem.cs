using System;
using System.Collections.Generic;
using System.Linq;
using LessonPlanBuilder.api.model;
using LessonPlanBuilder.core.subjectAppraiser;

namespace LessonPlanBuilder.core.generators
{
    public class GeneratorSequenceItem : IGeneratorSequenceItem<Subject, Lesson>
    {
        private readonly List<Subject> subjects;
        private Dictionary<Subject, double> sortedSubsWithGrades;
        private double averageGradeDifference;
        private List<Dictionary<Subject, double>> groupedSubjectWithLowDifference = new();
        private List<Subject> currentShuffledList;
        private List<int> alreadyGeneratedSequences = new();
        private Queue<Lesson> generatedSequence;


        public GeneratorSequenceItem(List<Subject> subjects)
        {
            this.subjects = subjects;
        }

        public Queue<Lesson> Generate(Appraiser<Subject> appraiser)
        {
            // For first call
            if (sortedSubsWithGrades == null)
            {
                SortSubjectsByGrades(appraiser);
                GetAverageSubjectGradeDifference();
                GroupSubjectsWithDifferenceLessThanAverage();
            }
            
            var foundSequence = false;
            while (true)
            {
                foundSequence = ShuffleSameGradeInDict();
                if (foundSequence)
                    break;
            }

            generatedSequence = new Queue<Lesson>();
            var i = 0;
            foreach (var e in currentShuffledList)
            {
                var lesson = new Lesson(e, i);
                generatedSequence.Enqueue(lesson);
                i++;
            }

            return generatedSequence;
        }

        private void SortSubjectsByGrades(Appraiser<Subject> appraiser)
        {
            var dict = subjects.ToDictionary(e => e, appraiser.AppraiseItem);
            sortedSubsWithGrades = dict.OrderBy(x => x.Value)
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        private void GetAverageSubjectGradeDifference()
        {
            var allGrades = sortedSubsWithGrades.Values.ToList();
            var allDiffs = 0.0;
            for (var i = 0; i < allGrades.Count - 1; i++)
                allDiffs += Math.Abs(allGrades[i] - allGrades[i + 1]);
            averageGradeDifference = allDiffs / (allGrades.Count - 1);
        }

        private void GroupSubjectsWithDifferenceLessThanAverage()
        {
            var dict = new Dictionary<Subject, double>();
            foreach (var e in sortedSubsWithGrades)
            {
                if (dict.Count == 0 || 
                    e.Value - dict.First().Value <= averageGradeDifference)
                {
                    dict.Add(e.Key, e.Value);
                    if (e.Equals(sortedSubsWithGrades.Last()))
                    {
                        var lastDict = dict
                            .ToDictionary(k => k.Key, k => k.Value);
                        groupedSubjectWithLowDifference.Add(lastDict);
                        dict.Clear();
                    }
                }
                else
                {
                    var newDict = dict
                        .ToDictionary(k => k.Key, k => k.Value);
                    groupedSubjectWithLowDifference.Add(newDict);
                    dict.Clear();
                    dict.Add(e.Key, e.Value);
                    if (e.Equals(sortedSubsWithGrades.Last()))
                    {
                        var lastDict = dict
                            .ToDictionary(k => k.Key, k => k.Value);
                        groupedSubjectWithLowDifference.Add(lastDict);
                        dict.Clear();
                    }
                }
            }
        }
        
        private bool ShuffleSameGradeInDict()
        {
            var rnd = new Random();
            currentShuffledList = new List<Subject>();
            foreach (var e in groupedSubjectWithLowDifference)
            {
                var groupedWithRepeatItems = new List<Subject>();
                foreach (var j in e)
                {
                    for (var i = 0; i < j.Key.LessonsCount; i++)
                    {
                        groupedWithRepeatItems.Add(j.Key);
                    }
                }

                var field = groupedWithRepeatItems
                    .OrderBy(s => rnd.NextDouble())
                    .ToList();

                foreach (var k in field)
                {
                    currentShuffledList.Add(k);
                }
            }

            if (!alreadyGeneratedSequences.Contains(currentShuffledList.GetHashCode()))
            {
                alreadyGeneratedSequences.Add(currentShuffledList.GetHashCode());
                return true;
            }

            return false;
        }
    }
}