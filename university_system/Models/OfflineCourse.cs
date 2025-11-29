using System;

namespace UniversitySystem.Models
{
    public class OfflineCourse : Course
    {
        private string classroom;
        private string schedule;

        public string Classroom
        {
            get => classroom;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Аудитория обязательна для офлайн-курса");
                }
                classroom = value;
            }
        }

        public string Schedule
        {
            get => schedule;
            set => schedule = value ?? string.Empty;
        }

        public OfflineCourse(string courseId, string courseName, string classroom, string schedule = "", string description = "")
            : base(courseId, courseName, description)
        {
            Classroom = classroom;
            Schedule = schedule;
        }

        public override string GetCourseType()
        {
            return "Офлайн-курс";
        }
    }
}