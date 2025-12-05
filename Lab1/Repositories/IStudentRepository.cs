using System;
using System.Collections.Generic;
using UniversitySystem.Models;

namespace UniversitySystem.Interfaces
{
    public interface IStudentRepository
    {
        void Add(Student student);
        Student? Get(Guid id);
        IReadOnlyCollection<Student> GetAll();
    }
}
