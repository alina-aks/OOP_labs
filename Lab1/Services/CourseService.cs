using System;
using System.Collections.Generic;
using UniversitySystem.Interfaces;
using UniversitySystem.Models;

namespace UniversitySystem.Services
{
    public class CourseService
    {
        private readonly ICourseRepository _courses;
        private readonly ITeacherRepository _teachers;
        private readonly IStudentRepository _students;

        public CourseService(
            ICourseRepository courses,
            ITeacherRepository teachers,
            IStudentRepository students)
        {
            _courses = courses;
            _teachers = teachers;
            _students = students;
        }

        public void AddCourse(Course course)
        {
            _courses.Add(course);
        }

        public void RemoveCourse(Guid courseId)
        {
            _courses.Remove(courseId);
        }

        public void AddTeacher(Teacher teacher)
        {
            _teachers.Add(teacher);
        }

        public void AddStudent(Student student)
        {
            _students.Add(student);
        }

        public void AssignTeacher(Guid courseId, Guid teacherId)
        {
            var course = _courses.Get(courseId)
                         ?? throw new Exception("Course not found");

            var teacher = _teachers.Get(teacherId)
                          ?? throw new Exception("Teacher not found");

            course.AssignTeacher(teacher.Id);
        }

        public void EnrollStudent(Guid courseId, Guid studentId)
        {
            var course = _courses.Get(courseId)
                         ?? throw new Exception("Course not found");

            var student = _students.Get(studentId)
                          ?? throw new Exception("Student not found");

            course.EnrollStudent(student.Id);
        }

        public List<Course> GetCoursesByTeacher(Guid teacherId)
        {
            var result = new List<Course>();

            foreach (var course in _courses.GetAll())
            {
                if (course.TeacherId == teacherId)
                    result.Add(course);
            }

            return result;
        }

        public List<Student> GetStudentsByCourse(Guid courseId)
        {
            var course = _courses.Get(courseId)
                         ?? throw new Exception("Course not found");

            var result = new List<Student>();

            foreach (var studentId in course.StudentIds)
            {
                var student = _students.Get(studentId);
                if (student != null)
                    result.Add(student);
            }

            return result;
        }
    }
}
