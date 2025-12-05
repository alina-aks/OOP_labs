using System;
using System.Collections.Generic;
using UniversitySystem.Interfaces;
using UniversitySystem.Models;

namespace UniversitySystem.Repositories
{
    public class InMemoryCourseRepository : ICourseRepository
    {
        private readonly List<Course> _courses = new();

        public void Add(Course course)
        {
            _courses.Add(course);
        }

        public Course? Get(Guid id)
        {
            foreach (var course in _courses)
            {
                if (course.Id == id)
                    return course;
            }

            return null;
        }

        public IReadOnlyCollection<Course> GetAll() => _courses.AsReadOnly();

        public void Remove(Guid id)
        {
            _courses.RemoveAll(c => c.Id == id);
        }
    }
}
