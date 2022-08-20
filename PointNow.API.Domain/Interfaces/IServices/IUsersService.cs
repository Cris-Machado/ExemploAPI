using PointNow.API.Domain.Services;


namespace PointNow.API.Domain.Interfaces.Repositories
{
    public interface IUsersService : IServiceBase<User>
    {
        string Calculo();
    }
}
