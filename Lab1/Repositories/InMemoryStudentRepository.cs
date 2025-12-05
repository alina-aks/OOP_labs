using System;
using System.Collections.Generic;
using UniversitySystem.Interfaces;
using UniversitySystem.Models;

namespace UniversitySystem.Repositories
{
    public class InMemoryStudentRepository : IStudentRepository
    {
        private readonly List<Student> _students = new();

        public void Add(Student student)
        {
            _students.Add(student);
        }

        public Student? Get(Guid id)
        {
            foreach (var student in _students)
            {
                if (student.Id == id)
                    return student;
            }

            return null;
        }

        public IReadOnlyCollection<Student> GetAll() => _students.AsReadOnly();
    }
}
