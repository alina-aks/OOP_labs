using System;

namespace UniversitySystem.Models
{
    public class Teacher
    {
        private string teacherName;
        private string teacherId;
        private List<string> courses;

        public string TeacherName //валидация имени преподователя
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
        public string TeacherId //валидация айди преподавателя
        {
            get => teacherId;

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Поле обязательно для заполнения");
                }
                teacherId = value;
            }
        }

        public List<string> courses
        {
            get {return courses;}
        }

        public Teacher (string teacherName, string teacherId) //конструктор класса
        {
            TeacherName = teacherName;
            TeacherId = teacherId;
            courses = new List<string>();
        }

        public void AssignToCourse(string courseName) //назначение курсе преподу
        {
            if (string.IsNullOrEmpty(courseName))
                {
                    throw new ArgumentException("Поле обязательно для заполнения");
                }

            if (!courses.Contains(courseName))
            {
                courses.Add(courseName);
                console.log($"Теперь преподаватель {teacherName} назначен на курс '{courseName}' ")
            }

            else
            {
                console.log($"Ошибка. {teacherName} уже назначен на курс '{courseName}'");
            }
        }

        public void RemoveFromCource(string courseName) //удаление курса у препода
        {
            if (string.IsNullOrEmpty(courseName))
                {
                    throw new ArgumentException("Поле обязательно для заполнения");
                }

            if (course.Contains(courseName))
            {
                courses.Remove(courseName);
                console.log($"Преподаватель {teacherName} больше не ведет курс '{courseName}' ");
            }

            else
            {
                console.log($"Ошибка. {teacherName} не ведет курс '{courseName}'");
            }
        }

        public List<string> TeacherCources() //список курсов
        {
            return new List<string>(courseName);
        }

        public void ShowTeacherInfo() //вывести инфу о преподе
        {
            console.WriteLine(@$"Информация о преподавателе:
            ФИО: {teacherName}
            ID: {teacherId}
            список назначенных курсов:");
            for (int i = 0; i < courses.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {courses[i]}");
            }
        }

        public override string ToString()
        {
            return $"{teacherName} ({studentId})";
        }

    }
}