using System;
using System.Collections.Generic;
using System.Linq;
using LessonPlanBuilder.api.model;

namespace LessonPlanBuilder.core.generators
{
    public class GeneratorSequenceItem<TSubject> : IGeneratorSequenceItem<TSubject>
        where TSubject : Subject 
    {
        private Dictionary<TSubject, int> lessonsGroupedBySameGrade = new Dictionary<TSubject, int>();
        private List<int> existGrades = new();
        private List<int> alreadyGeneratedSequences = new();
        private Dictionary<TSubject, int> currentShuffledDict;

        public GeneratorSequenceItem()
        {
            
        }

        public Queue<Lesson> Generate(Dictionary<TSubject, int> gradedLessons)
        {
            var generatedSequence = new Queue<Lesson>();
            //для первого вызова
            if (lessonsGroupedBySameGrade.Count <= 1)
            {
                GroupBySameGrade(gradedLessons);
                TakeAllGradesNoRepeat(lessonsGroupedBySameGrade);
            }

            var foundedSequence = false;
            while (true)
            {
                foundedSequence = ShuffleSameGradeInDict(lessonsGroupedBySameGrade);
                if (foundedSequence)
                    break;
            }

            var i = 0;
            foreach (var e in currentShuffledDict)
            {
                var lesson = new Lesson(e.Key, i);
                generatedSequence.Enqueue(lesson);
                i++;
            }

            return generatedSequence;
        }

        private void GroupBySameGrade(Dictionary<TSubject, int> ungroupedLessons)
        {
            lessonsGroupedBySameGrade =  ungroupedLessons
                .OrderBy(x => x.Value)
                .ToDictionary(pair => pair.Key,
                pair => pair.Value);
        }

        private void TakeAllGradesNoRepeat(Dictionary<TSubject, int> lessonsGroupedBySameGrade)
        {
            foreach (var n in lessonsGroupedBySameGrade.Values.
                Where(n => existGrades.Count == 0 || existGrades.Last() != n))
                existGrades.Add(n);
        }

        private bool ShuffleSameGradeInDict(Dictionary<TSubject, int> lessonsGroupedBySameGrade)
        {
            var rnd = new Random();
            currentShuffledDict = new Dictionary<TSubject, int>();
            foreach (var e in existGrades)
            {
                var field = lessonsGroupedBySameGrade
                    .Where(i => i.Value == e)
                    .OrderBy(s => rnd.NextDouble())
                    .ToDictionary(pair => pair.Key,
                        pair => pair.Value);
                foreach (var k in field)
                {
                    currentShuffledDict.Add(k.Key, k.Value);
                }
            }

            if (!alreadyGeneratedSequences.Contains(currentShuffledDict.GetHashCode()))
            {
                alreadyGeneratedSequences.Add(currentShuffledDict.GetHashCode());
                return true;
            }

            return false;
        }
    }
}