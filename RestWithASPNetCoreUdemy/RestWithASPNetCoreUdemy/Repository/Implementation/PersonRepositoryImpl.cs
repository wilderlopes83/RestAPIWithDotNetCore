using System;
using System.Collections.Generic;
using System.Threading;
using RestWithASPNetCoreUdemy.Model;
using RestWithASPNetCoreUdemy.Model.Context;
using System.Linq;

namespace RestWithASPNetCoreUdemy.Repository
{
    public class UserRepositoryImpl : IUserRepository
    {
        private MySQLContext _context;

        public UserRepositoryImpl(MySQLContext context)
        {
            _context = context;
        }

        public User FindByLogin(string login)
        {
            //return MockPerson((int)id);
            return _context.Users.SingleOrDefault(p => p.Login.Equals(login));
        }

    }
}
