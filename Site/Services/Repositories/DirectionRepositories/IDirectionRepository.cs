using Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Site.Services.Repositories.DirectionRepositories
{
    public interface IDirectionRepository
    {
        Task<DirectionOfTraining> GetByIdWithAllAsync(Guid id);
        Task<List<DirectionOfTraining>> GetByIdFacultyWithAllAsync(Guid id);
    }
}
