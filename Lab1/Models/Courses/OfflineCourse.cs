using System;

namespace UniversitySystem.Models
{
    public class OfflineCourse : Course
    {
        public string Campus { get; }
        public string Room { get; }

        public OfflineCourse(Guid id, string name, string description, string campus, string room)
            : base(id, name, description)
        {
            Campus = campus;
            Room = room;
        }

        public override string CourseType => "Offline";
    }
}
