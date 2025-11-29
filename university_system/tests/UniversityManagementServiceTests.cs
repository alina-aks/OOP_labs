using System;
using Xunit;
using UniversitySystem.Models;
using UniversitySystem.Services;

namespace UniversitySystem.Tests
{
    public class UniversityManagementServiceTests
    {
        private readonly UniversityManagementService _service;

        public UniversityManagementServiceTests()
        {
            _service = new UniversityManagementService();
        }

        [Fact]
        public void AddTeacher_ShouldAddTeacher()
        {
            // Arrange
            var teacher = new Teacher("Иванов Петр", 1);

            // Act
            _service.AddTeacher(teacher);

            // Assert
            var foundTeacher = _service.FindTeacherById(1);
            Assert.NotNull(foundTeacher);
            Assert.Equal("Иванов Петр", foundTeacher.TeacherName);
            Assert.Equal(1, foundTeacher.TeacherId);
        }

        [Fact]
        public void AddTeacher_WithDuplicateId_ShouldThrowException()
        {
            // Arrange
            var teacher1 = new Teacher("Иванов Петр", 1);
            var teacher2 = new Teacher("Сидорова Мария", 1);

            // Act
            _service.AddTeacher(teacher1);

            // Assert
            Assert.Throws<InvalidOperationException>(() => _service.AddTeacher(teacher2));
        }

        [Fact]
        public void AddStudent_ShouldAddStudent()
        {
            // Arrange
            var student = new Student("Алексеев Алексей", 101, "Информатика");

            // Act
            _service.AddStudent(student);

            // Assert
            var foundStudent = _service.FindStudentById(101);
            Assert.NotNull(foundStudent);
            Assert.Equal("Алексеев Алексей", foundStudent.StudentName);
            Assert.Equal(101, foundStudent.StudentId);
        }

        [Fact]
        public void AddCourse_ShouldAddCourse()
        {
            // Arrange
            var course = new OnlineCourse("C1", "Программирование", "Zoom", "zoom.com");

            // Act
            _service.AddCourse(course);

            // Assert
            var foundCourse = _service.FindCourseById("C1");
            Assert.NotNull(foundCourse);
            Assert.Equal("Программирование", foundCourse.CourseName);
            Assert.Equal("C1", foundCourse.CourseId);
        }

        [Fact]
        public void AssignTeacherToCourse_ShouldAssignTeacher()
        {
            // Arrange
            var teacher = new Teacher("Иванов Петр", 1);
            var course = new OnlineCourse("C1", "Программирование", "Zoom", "zoom.com");
            _service.AddTeacher(teacher);
            _service.AddCourse(course);

            // Act
            _service.AssignTeacherToCourse(1, "C1");

            // Assert
            var foundCourse = _service.FindCourseById("C1");
            Assert.NotNull(foundCourse.Teacher);
            Assert.Equal(1, foundCourse.Teacher.TeacherId);
            Assert.Equal("Иванов Петр", foundCourse.Teacher.TeacherName);
        }

        [Fact]
        public void AssignTeacherToCourse_WhenTeacherNotFound_ShouldThrowException()
        {
            // Arrange
            var course = new OnlineCourse("C1", "Программирование", "Zoom", "zoom.com");
            _service.AddCourse(course);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _service.AssignTeacherToCourse(999, "C1"));
        }

        [Fact]
        public void AssignTeacherToCourse_WhenCourseNotFound_ShouldThrowException()
        {
            // Arrange
            var teacher = new Teacher("Иванов Петр", 1);
            _service.AddTeacher(teacher);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _service.AssignTeacherToCourse(1, "INVALID"));
        }

        [Fact]
        public void AssignTeacherToCourse_WhenCourseAlreadyHasTeacher_ShouldThrowException()
        {
            // Arrange
            var teacher1 = new Teacher("Иванов Петр", 1);
            var teacher2 = new Teacher("Сидорова Мария", 2);
            var course = new OnlineCourse("C1", "Программирование", "Zoom", "zoom.com");
            
            _service.AddTeacher(teacher1);
            _service.AddTeacher(teacher2);
            _service.AddCourse(course);
            _service.AssignTeacherToCourse(1, "C1");

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _service.AssignTeacherToCourse(2, "C1"));
        }

        [Fact]
        public void RemoveTeacherFromCourse_ShouldRemoveTeacher()
        {
            // Arrange
            var teacher = new Teacher("Иванов Петр", 1);
            var course = new OnlineCourse("C1", "Программирование", "Zoom", "zoom.com");
            _service.AddTeacher(teacher);
            _service.AddCourse(course);
            _service.AssignTeacherToCourse(1, "C1");

            // Act
            _service.RemoveTeacherFromCourse(1, "C1");

            // Assert
            var foundCourse = _service.FindCourseById("C1");
            Assert.Null(foundCourse.Teacher);
        }

        [Fact]
        public void EnrollStudentToCourse_ShouldEnrollStudent()
        {
            // Arrange
            var student = new Student("Алексеев Алексей", 101, "Информатика");
            var course = new OnlineCourse("C1", "Программирование", "Zoom", "zoom.com");
            _service.AddStudent(student);
            _service.AddCourse(course);

            // Act
            _service.EnrollStudentToCourse(101, "C1");

            // Assert
            var foundCourse = _service.FindCourseById("C1");
            Assert.Single(foundCourse.EnrolledStudents);
            Assert.Equal(101, foundCourse.EnrolledStudents[0].StudentId);
        }

        [Fact]
        public void EnrollStudentToCourse_WhenStudentAlreadyEnrolled_ShouldThrowException()
        {
            // Arrange
            var student = new Student("Алексеев Алексей", 101, "Информатика");
            var course = new OnlineCourse("C1", "Программирование", "Zoom", "zoom.com");
            _service.AddStudent(student);
            _service.AddCourse(course);
            _service.EnrollStudentToCourse(101, "C1");

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _service.EnrollStudentToCourse(101, "C1"));
        }

        [Fact]
        public void RemoveStudentFromCourse_ShouldRemoveStudent()
        {
            // Arrange
            var student = new Student("Алексеев Алексей", 101, "Информатика");
            var course = new OnlineCourse("C1", "Программирование", "Zoom", "zoom.com");
            _service.AddStudent(student);
            _service.AddCourse(course);
            _service.EnrollStudentToCourse(101, "C1");

            // Act
            _service.RemoveStudentFromCourse(101, "C1");

            // Assert
            var foundCourse = _service.FindCourseById("C1");
            Assert.Empty(foundCourse.EnrolledStudents);
        }

        [Fact]
        public void GetCoursesByTeacher_ShouldReturnCorrectCourses()
        {
            // Arrange
            var teacher = new Teacher("Иванов Петр", 1);
            var course1 = new OnlineCourse("C1", "Программирование", "Zoom", "zoom.com");
            var course2 = new OnlineCourse("C2", "Базы данных", "Teams", "teams.com");
            
            _service.AddTeacher(teacher);
            _service.AddCourse(course1);
            _service.AddCourse(course2);
            _service.AssignTeacherToCourse(1, "C1");

            // Act
            var courses = _service.GetCoursesByTeacher(1);

            // Assert
            Assert.Single(courses);
            Assert.Equal("C1", courses[0].CourseId);
        }

        [Fact]
        public void RemoveCourse_ShouldRemoveCourse()
        {
            // Arrange
            var course = new OnlineCourse("C1", "Программирование", "Zoom", "zoom.com");
            _service.AddCourse(course);

            // Act
            _service.RemoveCourse("C1");

            // Assert
            var foundCourse = _service.FindCourseById("C1");
            Assert.Null(foundCourse);
        }

        [Fact]
        public void RemoveCourse_WhenCourseNotFound_ShouldThrowException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _service.RemoveCourse("INVALID"));
        }
    }
}