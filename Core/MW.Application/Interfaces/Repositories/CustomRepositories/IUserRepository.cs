using MW.Domain.Entities.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW.Application.Interfaces.Repositories.CustomRepository
{
    public interface IUserRepository : IGenericRepository<User>
    {
    }
}
