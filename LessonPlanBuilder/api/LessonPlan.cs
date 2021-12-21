using LessonPlanBuilder.api.model;
using LessonPlanBuilder.api.model.enums;

namespace LessonPlanBuilder.api
{
 
    /// <summary>
    /// Класс описывающий выходный параметры
    /// </summary>
    public class LessonPlan
    {
        private readonly ApprovedLesson[,] plan;
        public LessonPlan(ApprovedLesson[,] plan)
        {
            this.plan = plan;
        }
        
        public ApprovedLesson this[DayOfWeek day, LessonNumber lesson]
        {
            get => plan[(int)day, (int)lesson];
            set => plan[(int)day, (int)lesson] = value;
        }
		
        public ApprovedLesson this[int day, int lesson]
        {
            get => plan[day, lesson];
            set => plan[day, lesson] = value;
        }

    }
}