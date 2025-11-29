using System;

namespace UniversitySystem.Models
{
    public class Student
    {
        private string studentName;
        private int studentId;
        private string studentFaculty;

        public string StudentName
        {
            get => studentName;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Поле обязательно для заполнения");
                }
                studentName = value;
            }
        }

        public int StudentId
        {
            get => studentId;
            set
            {
                if (value == default(int))
                {
                    throw new ArgumentException("Поле обязательно для заполнения");
                }
                studentId = value;
            }
        }

        public string StudentFaculty
        {
            get => studentFaculty;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Поле обязательно для заполнения");
                }
                studentFaculty = value;
            }
        }

        public Student(string studentName, int studentId, string studentFaculty)
        {
            StudentName = studentName;
            StudentId = studentId;
            StudentFaculty = studentFaculty;
        }

        public override string ToString()
        {
            return $"{studentName}({studentId}), факультет - {studentFaculty}";
        }
    }
}