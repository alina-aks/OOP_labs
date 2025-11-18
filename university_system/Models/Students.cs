using System;

namespace UniversitySystem.Models
{
    public class Student
    {
        private string studentName;
        private string studentId;
        private string studentFaculty;

        public string StudentName
        {
            get => studentName;

            set
            {
                if (studentName.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Поле обязательно для заполнения");
                }
                studentName = value;
            }
        }
        public string StudentId
        {
            get => studentId;

            set
            {
                if (studentId.IsNullOrEmpty(value))
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
                if (studentFaculty.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Поле обязательно для заполнения");
                }
                studentFaculty = value;
            }
        }

        public Student(string studentName, string studentId, string studentFaculty)
        {
            name = studentName;
            id = studentId;
            faculty = studentFaculty;
        }

        public override string ToString()
        {
            return $"{name}(id), факультет - {faculty}";
        }


    }
}