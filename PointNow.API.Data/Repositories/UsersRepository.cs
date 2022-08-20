using Microsoft.Extensions.Logging;
using PointNow.API.Data.Interfaces;
using PointNow.API.Data.Repositories.Base;
using PointNow.API.Domain.Interfaces.Repositories;
using PointNow.API.Domain.Services;

namespace PointNow.API.Data.Repositories
{
    public class UsersRepository : RepositoryBase<User>, IUsersRepository
    {
        private readonly ILogger<UsersRepository> _logger;
        public UsersRepository(IDbContext context, ILogger<UsersRepository> logger) : base(context)
        {
            _logger = logger;
        }
    }
}
