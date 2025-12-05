using System;
using System.Collections.Generic;

namespace UniversitySystem.Models
{
    public abstract class Course
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public Guid? TeacherId { get; private set; }

        private readonly List<Guid> _studentIds = new();
        public IReadOnlyCollection<Guid> StudentIds => _studentIds.AsReadOnly();

        public Course(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public void AssignTeacher(Guid teacherId)
        {
            TeacherId = teacherId;
        }

        public void EnrollStudent(Guid studentId)
        {
            if (!_studentIds.Contains(studentId))
                _studentIds.Add(studentId);
        }

        public void RemoveStudent(Guid studentId)
        {
            _studentIds.Remove(studentId);
        }

        public abstract string CourseType { get; }
    }
}
