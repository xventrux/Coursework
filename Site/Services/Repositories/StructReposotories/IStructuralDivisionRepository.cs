using Site.Models.Entities;
using System.Threading.Tasks;
using System;

namespace Site.Services.Repositories.StructReposotories
{
    public interface IStructuralDivisionRepository
    {
        Task<StructuralDivision> GetByIdWithAllAsync(Guid id);
    }
}
