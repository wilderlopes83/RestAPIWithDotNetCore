using System;
using System.Collections.Generic;
using System.Threading;
using RestWithASPNetCoreUdemy.Model;
using RestWithASPNetCoreUdemy.Model.Context;
using System.Linq;

namespace RestWithASPNetCoreUdemy.Services.Implementation
{
    public class PersonServiceImpl : IPersonService
    {
        private MySQLContext _context;

        public PersonServiceImpl(MySQLContext context)
        {
            _context = context;
        }

        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return person;

        }

        public void Delete(long id)
        {
            var result = _context.Persons.SingleOrDefault(p=> p.Id.Equals(id));
            //var result = _context<Person>.SingleOrDefault(p=> p.Id.Equals(id));

            try
            {
                if (result != null)
                { 
                    _context.Persons.Remove(result);
                    _context.SaveChanges();
                }               
                
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        public List<Person> FindAll()
        {
            return _context.Persons.ToList(); 
        }

        public Person FindById(long id)
        {
            //return MockPerson((int)id);
            return _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
        }

        public Person Update(Person person)
        {
            if (!Exist(person.Id)) return new Person();

            var result = _context.Persons.SingleOrDefault(p=> p.Id.Equals(person.Id));

            try
            {
                _context.Entry(result).CurrentValues.SetValues(person);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return person;
        }

        /* 
        private Person MockPerson(int i)
        {
            return new Person
            {
                Id = IncrementAndGet(),
                FirstName = "Person First Name " + i.ToString(),
                LastName = "Person Last Name " + i.ToString(),
                Address = "Some Address" + i.ToString(),
                Gender = "Male"
            };
        }

        private long IncrementAndGet()
        {            
            return Interlocked.Increment(ref count);
        }
        */

        private bool Exist(long? id)
        {
            return _context.Persons.Any(p => p.Id.Equals(id));
        }
    }
}
