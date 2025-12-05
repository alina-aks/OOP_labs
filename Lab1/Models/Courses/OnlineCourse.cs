using System;

namespace UniversitySystem.Models
{
    public class OnlineCourse : Course
    {
        public string Platform { get; }
        public string MeetingLink { get; }

        public OnlineCourse(Guid id, string name, string description, string platform, string meetingLink)
            : base(id, name, description)
        {
            Platform = platform;
            MeetingLink = meetingLink;
        }

        public override string CourseType => "Online";
    }
}
