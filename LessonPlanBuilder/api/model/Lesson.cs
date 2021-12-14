﻿namespace LessonPlanBuilder.api.model
{
	public class Lesson
	{
		public readonly Subject Subject;
		public readonly int Id;

		public Lesson(Subject subject, int id)
		{
			Subject = subject;
			Id = id;
		}
	}
}