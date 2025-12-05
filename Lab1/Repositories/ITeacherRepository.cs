using System;
using System.Collections.Generic;
using UniversitySystem.Models;

namespace UniversitySystem.Interfaces
{
    public interface ITeacherRepository
    {
        void Add(Teacher teacher);
        Teacher? Get(Guid id);
        IReadOnlyCollection<Teacher> GetAll();
    }
}
