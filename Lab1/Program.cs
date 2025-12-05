using System;
using UniversitySystem.Models;
using UniversitySystem.Repositories;
using UniversitySystem.Services;

namespace UniversitySystem
{
    class Program
    {
        private static void Main()
        {
            var courseRepo = new InMemoryCourseRepository();
            var studentRepo = new InMemoryStudentRepository();
            var teacherRepo = new InMemoryTeacherRepository();

            var service = new CourseService(courseRepo, teacherRepo, studentRepo);

            var teacher = new Teacher(Guid.NewGuid(), "Иванов");
            service.AddTeacher(teacher);

            var student = new Student(Guid.NewGuid(), "Петров");
            service.AddStudent(student);

            var onlineCourse = new OnlineCourse(
                Guid.NewGuid(),
                "C# базовый онлайн",
                "Введение в C#",
                "Zoom",
                "https://zoom.us/j/123456"
            );
            service.AddCourse(onlineCourse);

            var offlineCourse = new OfflineCourse(
                Guid.NewGuid(),
                "C# офлайн",
                "Практика по C#",
                "Главный корпус",
                "Аудитория 101"
            );
            service.AddCourse(offlineCourse);

            service.AssignTeacher(onlineCourse.Id, teacher.Id);
            service.AssignTeacher(offlineCourse.Id, teacher.Id);

            service.EnrollStudent(onlineCourse.Id, student.Id);
            service.EnrollStudent(offlineCourse.Id, student.Id);

            var teacherCourses = service.GetCoursesByTeacher(teacher.Id);
            Console.WriteLine("Курсы преподавателя:");
            foreach (var c in teacherCourses)
                Console.WriteLine($"- {c.Name} ({c.CourseType})");

            Console.WriteLine();
            Console.WriteLine("Студенты на курсе \"C# базовый онлайн\":");
            var studentsOfCourse = service.GetStudentsByCourse(onlineCourse.Id);
            foreach (var s in studentsOfCourse)
                Console.WriteLine($"- {s.Name}");

            service.RemoveCourse(offlineCourse.Id);
            Console.WriteLine();
            Console.WriteLine("После удаления офлайн-курса количество курсов преподавателя: " +
                              service.GetCoursesByTeacher(teacher.Id).Count);
        }
    }
}
