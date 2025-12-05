using System;
using System.Collections.Generic;
using UniversitySystem.Models;

namespace UniversitySystem.Interfaces
{
    public interface ICourseRepository
    {
        void Add(Course course);
        Course? Get(Guid id);
        IReadOnlyCollection<Course> GetAll();
        void Remove(Guid id);
    }
}
