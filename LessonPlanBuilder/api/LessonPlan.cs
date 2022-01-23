using LessonPlanBuilder.api.model;
using DayOfWeek = LessonPlanBuilder.api.model.enums.DayOfWeek;

namespace LessonPlanBuilder.api;

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

	public ApprovedLesson this[DayOfWeek day, int lesson]
	{
		get => plan[(int)day, lesson];
		set => plan[(int)day, lesson] = value;
	}

	public ApprovedLesson this[int day, int lesson]
	{
		get => plan[day, lesson];
		set => plan[day, lesson] = value;
	}
}