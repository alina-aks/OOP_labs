using System;
using System.Collections.Generic;

namespace UniversitySystem.Models
{
    public abstract class Course
    {
        private string courseId;
        private string courseName;
        private string description;

        public string CourseId
        {
            get => courseId;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("ID курса обязательно для заполнения");
                }
                courseId = value;
            }
        }

        public string CourseName
        {
            get => courseName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Название курса обязательно для заполнения");
                }
                courseName = value;
            }
        }

        public string Description
        {
            get => description;
            set => description = value ?? string.Empty;
        }

        public Teacher Teacher { get; set; }
        public List<Student> EnrolledStudents { get; private set; }

        protected Course(string courseId, string courseName, string description = "")
        {
            CourseId = courseId;
            CourseName = courseName;
            Description = description;
            EnrolledStudents = new List<Student>();
            Teacher = null;
        }

        public abstract string GetCourseType();

        public override string ToString()
        {
            return $"{CourseName} ({CourseId}) - {GetCourseType()}";
        }
    }
}