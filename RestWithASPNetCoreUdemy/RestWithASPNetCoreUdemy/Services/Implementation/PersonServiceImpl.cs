using System;
using System.Collections.Generic;
using System.Threading;
using RestWithASPNetCoreUdemy.Model;
using RestWithASPNetCoreUdemy.Model.Context;

namespace RestWithASPNetCoreUdemy.Services.Implementation
{
    public class PersonServiceImpl : IPersonService
    {
        private MySQLContext _context;

        public PersonServiceImpl(MySQLContext context)
        {
            _context = context;
        }


        private volatile int count;

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
            //
        }

        public List<Person> FindAll()
        {
            List<Person> p = new List<Person>();
            for (int i=0; i <8; i++)
            {
                p.Add(MockPerson(i));
            }

            return p;
        }

        public Person FindById(long id)
        {
            return MockPerson((int)id);
        }

        public Person Update(Person person)
        {
            return person;
        }

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
    }
}
