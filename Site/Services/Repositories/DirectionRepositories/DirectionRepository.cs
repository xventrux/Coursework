using Microsoft.EntityFrameworkCore;
using Site.Infrastructure.Repository;
using Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Services.Repositories.DirectionRepositories
{
    public class DirectionRepository : Repository<DirectionOfTraining>, IDirectionRepository
    {
        public DirectionRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<DirectionOfTraining> GetByIdWithAllAsync(Guid id)
        {
            DirectionOfTraining dt = await GetAllFiltered(d => d.Id == id).Include(d => d.Groups).ThenInclude(g => g.Students)
                .ThenInclude(g => g.Courses).SingleOrDefaultAsync();

            dt.StructuralDivision = null;
            dt.Faculty = null;

            foreach(Group i in dt.Groups)
            {
                i.DirectionOfTraining = null;
                foreach(User j in i.Students)
                {
                    j.Group = null;
                }

                if (i.Courses != null)
                {
                    foreach (Course j in i.Courses)
                    {
                        j.Group = null;
                        j.Students = null;
                        j.Materials = null;
                    }
                }
                else
                {
                    i.Courses = new List<Course>();
                }

            }

            return dt;
        }
        
        public async Task<List<DirectionOfTraining>> GetByIdFacultyWithAllAsync(Guid id)
        {
            List<DirectionOfTraining> dt = await GetAllFiltered(d => d.FacultyId == id).Include(d => d.Groups).ThenInclude(g => g.Students).ToListAsync();

            foreach(var d in dt)
            {
                d.StructuralDivision = null;
                d.Faculty = null;

                foreach (Group i in d.Groups)
                {
                    i.DirectionOfTraining = null;
                    foreach (User j in i.Students)
                    {
                        j.Group = null;
                    }
                }
            }
            

            return dt;
        }
    }
}
