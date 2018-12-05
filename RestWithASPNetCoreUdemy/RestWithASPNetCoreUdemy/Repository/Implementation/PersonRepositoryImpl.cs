using System;
using System.Collections.Generic;
using System.Threading;
using RestWithASPNetCoreUdemy.Model;
using RestWithASPNetCoreUdemy.Model.Context;
using System.Linq;
using RestWithASPNetCoreUdemy.Repository.Generic;

namespace RestWithASPNetCoreUdemy.Repository
{
    public class PersonRepositoryImpl : GenericRepository<Person>, IPersonRepository
    {

        public PersonRepositoryImpl(MySQLContext context): base(context){}

        public List<Person> FindByName(string firstName, string lastName)
        {
            if(!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                return _context.Persons.Where(p => p.FirstName.Equals(firstName) && p.LastName.Equals(lastName)).ToList();
            }
            else if(!string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName)) 
            {
                return _context.Persons.Where(p => p.FirstName.Equals(firstName)).ToList();
            }
            else if(string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName)) 
            {
                return _context.Persons.Where(p => p.LastName.Equals(lastName)).ToList();
            }            
            else
            {
                return _context.Persons.ToList();
            }
        }
    }
}
