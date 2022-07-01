using Site.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Site.Services.Repositories.UserRepositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllWithGroup();
    }
}
