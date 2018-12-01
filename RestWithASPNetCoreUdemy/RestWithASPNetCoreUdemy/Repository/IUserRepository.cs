using RestWithASPNetCoreUdemy.Model;
using System.Collections.Generic;

namespace RestWithASPNetCoreUdemy.Repository
{
    public interface IUserRepository
    {
        User FindByLogin(string login);
        
    }
}
