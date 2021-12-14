using LessonPlanBuilder.api.model.enums;

namespace LessonPlanBuilder.api.model
{
	public struct ScheduleCell
	{
		public ScheduleCell(ScheduleCellStatus status)
		{
			Status = status;
		}

		public ScheduleCellStatus Status { get; set; }

		public override string ToString()
		{
			return $"{Status}";
		}
	}
}