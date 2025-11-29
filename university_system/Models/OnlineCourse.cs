using System;

namespace UniversitySystem.Models
{
    public class OnlineCourse : Course
    {
        private string platform;
        private string meetingLink;

        public string Platform
        {
            get => platform;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Платформа обязательна для онлайн-курса");
                }
                platform = value;
            }
        }

        public string MeetingLink
        {
            get => meetingLink;
            set => meetingLink = value ?? string.Empty;
        }

        public OnlineCourse(string courseId, string courseName, string platform, string meetingLink = "", string description = "")
            : base(courseId, courseName, description)
        {
            Platform = platform;
            MeetingLink = meetingLink;
        }

        public override string GetCourseType()
        {
            return "Онлайн-курс";
        }
    }
}