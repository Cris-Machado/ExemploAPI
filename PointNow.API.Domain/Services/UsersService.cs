

using PointNow.API.Domain.Interfaces.Base;
using PointNow.API.Domain.Interfaces.Repositories;
using PointNow.API.Domain.Services;

namespace PointNow.API.Domain.Interfaces.Services
{
    public class UsersService : ServiceBase<User>, IUsersService
    {
        public UsersService(IUnitOfWork unitOfWork, IRepositoryBase<User> repository) : base(unitOfWork, repository)
        {
        }

        #region Methods
        public string Calculo()
        {
            return "teste";
        }
        #endregion
    }
}
