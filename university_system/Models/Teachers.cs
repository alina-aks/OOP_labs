using System;
using System.Collections.Generic;

namespace UniversitySystem.Models
{
    public class Teacher
    {
        private string teacherName;
        private int teacherId;
        private List<string> courses;

        public string TeacherName 
        {
            get => teacherName;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Поле обязательно для заполнения");
                }
                teacherName = value;
            }
        }

        public int TeacherId 
        {
            get => teacherId;
            set
            {
                if (value == default(int))
                {
                    throw new ArgumentException("Поле обязательно для заполнения");
                }
                teacherId = value;
            }
        }

        public List<string> Courses
        {
            get { return courses; }
        }

        public Teacher(string teacherName, int teacherId) 
        {
            TeacherName = teacherName;
            TeacherId = teacherId;
            courses = new List<string>();
        }


        public override string ToString()
        {
            return $"{teacherName} ({teacherId})";
        }
    }
}