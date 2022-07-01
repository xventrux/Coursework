using Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Site.Services.Repositories.СourseRepositories
{
    public interface ICourseRepository
    {
        Task<Course> GetByIdWithAll(Guid id);
        Task<List<Course>> GetAllWithAll();
        Task<List<Course>> GetAllForGroupWithAll(Guid id);
        Task<List<Course>> GetAllForTeacherWithAll(string id);
    }
}
