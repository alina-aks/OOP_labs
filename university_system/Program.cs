using System;
using UniversitySystem.Models;
using UniversitySystem.Services;

namespace UniversitySystem
{
    class Program
    {
        static UniversityManagementService service = new UniversityManagementService();

        static void Main(string[] args)
        {
            // Добавляем тестовые данные
            AddTestData();
            
            // Показываем меню
            ShowMenu();
        }

        static void AddTestData()
        {
            // Преподаватели
            var teacher1 = new Teacher("Иванов Петр", 1);
            var teacher2 = new Teacher("Сидорова Мария", 2);
            
            // Студенты
            var student1 = new Student("Алексеев Алексей", 101, "Информатика");
            var student2 = new Student("Петрова Анна", 102, "Экономика");
            var student3 = new Student("Смирнов Дмитрий", 103, "Информатика");
            
            // Добавляем в систему
            service.AddTeacher(teacher1);
            service.AddTeacher(teacher2);
            service.AddStudent(student1);
            service.AddStudent(student2);
            service.AddStudent(student3);
            
            // Создаем курсы
            var course1 = new OnlineCourse("C1", "Программирование C#", "Zoom", "zoom.com/csharp");
            var course2 = new OfflineCourse("C2", "Базы данных", "Аудитория 301", "Пн, Ср 10:00");
            
            service.AddCourse(course1);
            service.AddCourse(course2);
            
            // Назначаем преподавателей
            service.AssignTeacherToCourse(1, "C1");
            service.AssignTeacherToCourse(2, "C2");
            
            // Записываем студентов
            service.EnrollStudentToCourse(101, "C1");
            service.EnrollStudentToCourse(102, "C1");
            service.EnrollStudentToCourse(103, "C2");
        }

        static void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("УНИВЕРСИТЕТ - ГЛАВНОЕ МЕНЮ");
                Console.WriteLine("1. Все курсы");
                Console.WriteLine("2. Все преподаватели");
                Console.WriteLine("3. Добавить курс");
                Console.WriteLine("4. Назначить преподавателя");
                Console.WriteLine("5. Удалить преподавателя с курса");
                Console.WriteLine("6. Записать студента");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите: ");
                
                string choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1": ShowCourses(); break;
                    case "2": ShowTeachers(); break;
                    case "3": AddCourse(); break;
                    case "4": AssignTeacher(); break;
                    case "5": RemoveTeacherFromCourse(); break;
                    case "6": EnrollStudent(); break;
                    case "0": return;
                    default: Console.WriteLine("Неверный выбор!"); Wait(); break;
                }
            }
        }

        static void ShowCourses()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("СПИСОК КУРСОВ:\n");
                
                var courses = service.GetAllCourses();
                foreach (var course in courses)
                {
                    Console.WriteLine($"ID: {course.CourseId}");
                    Console.WriteLine($"Название: {course.CourseName}");
                    Console.WriteLine($"Тип: {course.GetCourseType()}");
                    Console.WriteLine($"Преподаватель: {course.Teacher?.TeacherName ?? "Не назначен"}");
                    Console.WriteLine("---");
                }
                
                Console.WriteLine("\n1. Подробнее о курсе");
                Console.WriteLine("2. Студенты курса"); 
                Console.WriteLine("0. Назад");
                Console.Write("Выберите: ");
                
                string choice = Console.ReadLine();
                if (choice == "1") ShowCourseDetails();
                else if (choice == "2") ShowCourseStudents();
                else if (choice == "0") return;
                else Console.WriteLine("Неверный выбор!");
            }
        }

        static void ShowCourseDetails()
        {
            Console.Write("Введите ID курса: ");
            string id = Console.ReadLine();
            
            try
            {
                service.ShowCourseInfo(id);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
            Wait();
        }

        static void ShowCourseStudents()
        {
            Console.Write("Введите ID курса: ");
            string id = Console.ReadLine();
            
            try
            {
                service.ShowStudentsOnCourse(id);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
            Wait();
        }

        static void ShowTeachers()
        {
            Console.Clear();
            Console.WriteLine("СПИСОК ПРЕПОДАВАТЕЛЕЙ:\n");
            service.ShowAllTeachers();
            Wait();
        }

        static void AddCourse()
        {
            Console.Clear();
            Console.WriteLine("ДОБАВЛЕНИЕ КУРСА");
            
            try
            {
                Console.Write("ID курса: ");
                string id = Console.ReadLine();
                
                Console.Write("Название: ");
                string name = Console.ReadLine();
                
                Console.Write("Описание: ");
                string desc = Console.ReadLine();
                
                Console.WriteLine("Тип курса (1-Онлайн, 2-Офлайн): ");
                string type = Console.ReadLine();
                
                Course course;
                if (type == "1")
                {
                    Console.Write("Платформа: ");
                    string platform = Console.ReadLine();
                    
                    Console.Write("Ссылка: ");
                    string link = Console.ReadLine();
                    
                    course = new OnlineCourse(id, name, platform, link, desc);
                }
                else
                {
                    Console.Write("Аудитория: ");
                    string room = Console.ReadLine();
                    
                    Console.Write("Расписание: ");
                    string schedule = Console.ReadLine();
                    
                    course = new OfflineCourse(id, name, room, schedule, desc);
                }
                
                service.AddCourse(course);
                Console.WriteLine("Курс добавлен!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
            Wait();
        }

        static void AssignTeacher()
        {
            Console.Clear();
            Console.WriteLine("НАЗНАЧЕНИЕ ПРЕПОДАВАТЕЛЯ");
            
            Console.WriteLine("Список преподавателей:");
            service.ShowAllTeachers();
            
            Console.Write("ID преподавателя: ");
            if (!int.TryParse(Console.ReadLine(), out int teacherId))
            {
                Console.WriteLine("Ошибка ввода ID!");
                Wait();
                return;
            }
            
            Console.WriteLine("Список курсов:");
            var courses = service.GetAllCourses();
            foreach (var c in courses)
            {
                Console.WriteLine($"ID: {c.CourseId} - {c.CourseName}");
            }
            
            Console.Write("ID курса: ");
            string courseId = Console.ReadLine();
            
            try
            {
                service.AssignTeacherToCourse(teacherId, courseId);
                Console.WriteLine("Преподаватель назначен!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
            Wait();
        }

        static void RemoveTeacherFromCourse()
        {
            Console.Clear();
            Console.WriteLine("УДАЛЕНИЕ ПРЕПОДАВАТЕЛЯ С КУРСА");
            
            Console.WriteLine("Список преподавателей:");
            service.ShowAllTeachers();
            
            Console.Write("ID преподавателя: ");
            if (!int.TryParse(Console.ReadLine(), out int teacherId))
            {
                Console.WriteLine("Ошибка ввода ID!");
                Wait();
                return;
            }
            
            Console.WriteLine("Список курсов:");
            var courses = service.GetAllCourses();
            foreach (var c in courses)
            {
                Console.WriteLine($"ID: {c.CourseId} - {c.CourseName}");
            }
            
            Console.Write("ID курса: ");
            string courseId = Console.ReadLine();
            
            try
            {
                service.RemoveTeacherFromCourse(teacherId, courseId);
                Console.WriteLine("Преподаватель удален с курса!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
            Wait();
        }

        static void EnrollStudent()
        {
            Console.Clear();
            Console.WriteLine("ЗАПИСЬ СТУДЕНТА");
            
            Console.WriteLine("Список студентов:");
            service.ShowAllStudents();
            
            Console.Write("ID студента: ");
            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                Console.WriteLine("Ошибка ввода ID!");
                Wait();
                return;
            }
            
            Console.WriteLine("Список курсов:");
            var courses = service.GetAllCourses();
            foreach (var c in courses)
            {
                Console.WriteLine($"ID: {c.CourseId} - {c.CourseName}");
            }
            
            Console.Write("ID курса: ");
            string courseId = Console.ReadLine();
            
            try
            {
                service.EnrollStudentToCourse(studentId, courseId);
                Console.WriteLine("Студент записан!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
            Wait();
        }

        static void Wait()
        {
            Console.WriteLine("\nНажмите Enter...");
            Console.ReadLine();
        }
    }
}