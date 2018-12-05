using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using RestWithASPNetCoreUdemy.Model;
using RestWithASPNetCoreUdemy.Model.Base;
using RestWithASPNetCoreUdemy.Repository.Generic;

namespace RestWithASPNetCoreUdemy.Repository
{
    public interface IPersonRepository: IRepository<Person>
    {
        List<Person> FindByName(string firstName, string lastName);
    }
}