using RestWithASPNetCoreUdemy.Model;
using System.Collections.Generic;

namespace RestWithASPNetCoreUdemy.Business
{
    public interface ILoginBusiness
    {
        object FindByLogin(User user);
    }
}
