using Microsoft.EntityFrameworkCore;
using Site.Infrastructure.Repository;
using Site.Models.Entities;
using System;
using System.Threading.Tasks;

namespace Site.Services.Repositories.StructReposotories
{
    public class StructuralDivisionRepository : Repository<StructuralDivision>, IStructuralDivisionRepository
    {
        public StructuralDivisionRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<StructuralDivision> GetByIdWithAllAsync(Guid id)
        {
            StructuralDivision sd = await GetAllFiltered(s => s.Id == id).Include(s => s.Faculties)
                .Include(s => s.DirectionOfTrainings).SingleOrDefaultAsync();

            foreach(var i in sd.Faculties)
            {
                i.StructuralDivision = null;
            }
            foreach (var i in sd.DirectionOfTrainings)
            {
                i.StructuralDivision = null;
            }

            return sd;
        }
    }
}
