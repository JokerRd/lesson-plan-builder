using System;
using LessonPlanBuilder.api.model.enums;

namespace LessonPlanBuilder.api.model
{
	public class Schedule
	{
		private readonly ScheduleCell[,] cells;

		public Schedule(ScheduleCell[,] cells)
		{
			this.cells = cells;
		}

		public ScheduleCell this[DayOfWeek day, LessonNumber lesson]
		{
			get => cells[(int)day, (int)lesson];
			set => cells[(int)day, (int)lesson] = value;
		}
	}
}