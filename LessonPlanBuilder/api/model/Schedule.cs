using System;
using LessonPlanBuilder.api.model.enums;
using DayOfWeek = LessonPlanBuilder.api.model.enums.DayOfWeek;

namespace LessonPlanBuilder.api.model
{
	public class Schedule
	{
		private readonly ScheduleCell[,] cells;

		public Schedule(ScheduleCell[,] cells)
		{
			this.cells = cells;
		}

		public ScheduleCell this[DayOfWeek day, int lesson]
		{
			get => cells[(int)day, lesson];
			set => cells[(int)day, lesson] = value;
		}

		public ScheduleCell this[int day, int lesson]
		{
			get => cells[day, lesson];
			set => cells[day, lesson] = value;
		}
	}
}