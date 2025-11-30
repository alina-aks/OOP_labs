using System;
using System.Collections.Generic;
using System.Linq;
using UniversitySystem.Models;

namespace UniversitySystem.Services
{
    public class UniversityManagementService
    {
        private List<Course> courses;
        private List<Teacher> teachers;
        private List<Student> students;

        public UniversityManagementService()
        {
            courses = new List<Course>();
            teachers = new List<Teacher>();
            students = new List<Student>();
        }

        public void AssignTeacherToCourse(int teacherId, string courseId)
        {
            var teacher = FindTeacherById(teacherId);
            var course = FindCourseById(courseId);

            if (teacher == null)
                throw new ArgumentException($"Преподаватель с ID {teacherId} не найден");

            if (course == null)
                throw new ArgumentException($"Курс с ID {courseId} не найден");

            if (course.Teacher != null)
                throw new InvalidOperationException($"На курс '{course.CourseName}' уже назначен преподаватель {course.Teacher.TeacherName}");

            course.Teacher = teacher;
            teacher.Courses.Add(course.CourseName);

            Console.WriteLine($"Преподаватель {teacher.TeacherName} назначен на курс '{course.CourseName}'");
        }

        public void RemoveTeacherFromCourse(int teacherId, string courseId)
        {
            var teacher = FindTeacherById(teacherId);
            var course = FindCourseById(courseId);

            if (teacher == null || course == null)
                throw new ArgumentException("Преподаватель или курс не найдены");

            if (course.Teacher == null || course.Teacher.TeacherId != teacherId)
                throw new InvalidOperationException("Этот преподаватель не назначен на данный курс");

            course.Teacher = null;
            teacher.Courses.Remove(course.CourseName);

            Console.WriteLine($"Преподаватель {teacher.TeacherName} удален с курса '{course.CourseName}'");
        }
        public void EnrollStudentToCourse(int studentId, string courseId)
        {
            var student = FindStudentById(studentId);
            var course = FindCourseById(courseId);

            if (student == null)
                throw new ArgumentException($"Студент с ID {studentId} не найден");

            if (course == null)
                throw new ArgumentException($"Курс с ID {courseId} не найден");

            if (course.EnrolledStudents.Any(s => s.StudentId == studentId))
                throw new InvalidOperationException($"Студент {student.StudentName} уже записан на курс '{course.CourseName}'");

            course.EnrolledStudents.Add(student);
            Console.WriteLine($"Студент {student.StudentName} записан на курс '{course.CourseName}'");
        }

        public void RemoveStudentFromCourse(int studentId, string courseId)
        {
            var student = FindStudentById(studentId);
            var course = FindCourseById(courseId);

            if (student == null || course == null)
                throw new ArgumentException("Студент или курс не найдены");

            var studentOnCourse = course.EnrolledStudents.FirstOrDefault(s => s.StudentId == studentId);
            if (studentOnCourse == null)
                throw new InvalidOperationException("Студент не записан на данный курс");

            course.EnrolledStudents.Remove(studentOnCourse);
            Console.WriteLine($"Студент {student.StudentName} удален с курса '{course.CourseName}'");
        }

        public void StartOnlineSession(string courseId)
        {
            var course = FindCourseById(courseId);
            if (course is OnlineCourse onlineCourse)
            {
                if (string.IsNullOrEmpty(onlineCourse.MeetingLink))
                {
                    Console.WriteLine($"Онлайн-сессия для курса '{onlineCourse.CourseName}' не может быть начата: ссылка не установлена");
                }
                else
                {
                    Console.WriteLine($"Запуск онлайн-сессии для курса '{onlineCourse.CourseName}' по ссылке: {onlineCourse.MeetingLink}");
                }
            }
            else
            {
                throw new InvalidOperationException("Этот курс не является онлайн-курсом");
            }
        }

        public void ConductClass(string courseId)
        {
            var course = FindCourseById(courseId);
            if (course is OfflineCourse offlineCourse)
            {
                Console.WriteLine($"Проведение занятия по курсу '{offlineCourse.CourseName}' в аудитории {offlineCourse.Classroom}");
            }
            else
            {
                throw new InvalidOperationException("Этот курс не является офлайн-курсом");
            }
        }

        public void ShowTeacherInfo(int teacherId)
        {
            var teacher = FindTeacherById(teacherId);
            if (teacher == null)
                throw new ArgumentException($"Преподаватель с ID {teacherId} не найден");

            Console.WriteLine(@$"Информация о преподавателе:
            ФИО: {teacher.TeacherName}
            ID: {teacher.TeacherId}
            Список назначенных курсов:");
            
            for (int i = 0; i < teacher.Courses.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {teacher.Courses[i]}");
            }
        }

        public void ShowCourseInfo(string courseId)
        {
            var course = FindCourseById(courseId);
            if (course == null)
                throw new ArgumentException($"Курс с ID {courseId} не найден");

            Console.WriteLine($"Информация о курсе:");
            Console.WriteLine($"  ID: {course.CourseId}");
            Console.WriteLine($"  Название: {course.CourseName}");
            Console.WriteLine($"  Тип: {course.GetCourseType()}"); 
            Console.WriteLine($"  Описание: {course.Description}");
            Console.WriteLine($"  Преподаватель: {course.Teacher?.TeacherName ?? "Не назначен"}");
            Console.WriteLine($"  Количество студентов: {course.EnrolledStudents.Count}");

            if (course is OnlineCourse onlineCourse)
            {
                Console.WriteLine($"  Платформа: {onlineCourse.Platform}");
                if (!string.IsNullOrEmpty(onlineCourse.MeetingLink))
                    Console.WriteLine($"  Ссылка для подключения: {onlineCourse.MeetingLink}");
            }
            else if (course is OfflineCourse offlineCourse)
            {
                Console.WriteLine($"  Аудитория: {offlineCourse.Classroom}");
                if (!string.IsNullOrEmpty(offlineCourse.Schedule))
                    Console.WriteLine($"  Расписание: {offlineCourse.Schedule}");
            }
        }

        public void ShowStudentsOnCourse(string courseId)
        {
            var course = FindCourseById(courseId);
            if (course == null)
                throw new ArgumentException($"Курс с ID {courseId} не найден");

            Console.WriteLine($"\nСтуденты на курсе '{course.CourseName}':");
            Console.WriteLine("======================================");
            
            if (course.EnrolledStudents.Count == 0)
            {
                Console.WriteLine("На курсе нет студентов");
            }
            else
            {
                for (int i = 0; i < course.EnrolledStudents.Count; i++)
                {
                    var student = course.EnrolledStudents[i];
                    Console.WriteLine($"{i + 1}. {student.StudentName} (ID: {student.StudentId}) - {student.StudentFaculty}");
                }
            }
        }

        public void AddCourse(Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));

            if (courses.Any(c => c.CourseId == course.CourseId))
                throw new InvalidOperationException($"Курс с ID '{course.CourseId}' уже существует");

            courses.Add(course);
            Console.WriteLine($"Курс '{course.CourseName}' добавлен в систему");
        }

        public void RemoveCourse(string courseId)
        {
            var course = FindCourseById(courseId);
            if (course == null)
                throw new ArgumentException($"Курс с ID '{courseId}' не найден");

            courses.Remove(course);
            Console.WriteLine($"Курс '{course.CourseName}' удален из системы");
        }

        public void AddTeacher(Teacher teacher)
        {
            if (teacher == null)
                throw new ArgumentNullException(nameof(teacher));

            if (teachers.Any(t => t.TeacherId == teacher.TeacherId))
                throw new InvalidOperationException($"Преподаватель с ID '{teacher.TeacherId}' уже существует");

            teachers.Add(teacher);
            Console.WriteLine($"Преподаватель '{teacher.TeacherName}' добавлен в систему");
        }

        public void AddStudent(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            if (students.Any(s => s.StudentId == student.StudentId))
                throw new InvalidOperationException($"Студент с ID '{student.StudentId}' уже существует");

            students.Add(student);
            Console.WriteLine($"Студент '{student.StudentName}' добавлен в систему");
        }

        public List<Course> GetCoursesByTeacher(int teacherId)
        {
            return courses.Where(c => c.Teacher?.TeacherId == teacherId).ToList();
        }

        public void ShowAllCourses()
        {
            Console.WriteLine("\n=== ВСЕ КУРСЫ ===");
            foreach (var course in courses)
            {
                ShowCourseInfo(course.CourseId);
                Console.WriteLine();
            }
        }

        public void ShowAllTeachers()
        {
            Console.WriteLine("\n=== ВСЕ ПРЕПОДАВАТЕЛИ ===");
            foreach (var teacher in teachers)
            {
                ShowTeacherInfo(teacher.TeacherId);
                Console.WriteLine();
            }
        }

        public List<Course> GetAllCourses()
        {
            return courses;
        }

        public void ShowAllStudents()
        {
            Console.WriteLine("\n=== ВСЕ СТУДЕНТЫ ===");
            foreach (var student in students)
            {
                Console.WriteLine(student.ToString());
            }
        }

        public Course? FindCourseById(string courseId)
        {
            return courses.FirstOrDefault(c => c.CourseId == courseId);
        }

        public Teacher? FindTeacherById(int teacherId)
        {
            return teachers.FirstOrDefault(t => t.TeacherId == teacherId);
        }

        public Student? FindStudentById(int studentId)
        {
            return students.FirstOrDefault(s => s.StudentId == studentId);
        }
    }
}