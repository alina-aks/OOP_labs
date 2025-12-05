using System;
using System.Collections.Generic;
using UniversitySystem.Interfaces;
using UniversitySystem.Models;

namespace UniversitySystem.Repositories
{
    public class InMemoryTeacherRepository : ITeacherRepository
    {
        private readonly List<Teacher> _teachers = new();

        public void Add(Teacher teacher)
        {
            _teachers.Add(teacher);
        }

        public Teacher? Get(Guid id)
        {
            foreach (var teacher in _teachers)
            {
                if (teacher.Id == id)
                    return teacher;
            }

            return null;
        }

        public IReadOnlyCollection<Teacher> GetAll() => _teachers.AsReadOnly();
    }
}
