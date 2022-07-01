using Microsoft.EntityFrameworkCore;
using Site.Infrastructure.Repository;
using Site.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Site.Services.Repositories.UserRepositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<User>> GetAllWithGroup()
        {
            List<User> l = await GetAllFiltered(u => u.GroupId != null).Include(u => u.Group).ToListAsync();

            foreach(var i in l)
            {
                i.Group.Students = null;
            }

            return l;
        }
    }
}
