using System;
using System.Collections.Generic;
using System.Threading;
using RestWithASPNetCoreUdemy.Model;
using RestWithASPNetCoreUdemy.Model.Context;
using System.Linq;
using RestWithASPNetCoreUdemy.Repository;

namespace RestWithASPNetCoreUdemy.Business
{
    public class PersonBusinessImpl : IPersonBusiness
    {
        private IPersonRepository _repository;

        public PersonBusinessImpl(IPersonRepository repository)
        {
            _repository = repository;
        }

        public Person Create(Person person)
        {
            return _repository.Create(person);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<Person> FindAll()
        {
            return _repository.FindAll();
        }

        public Person FindById(long id)
        {
            return _repository.FindById(id);
        }

        public Person Update(Person person)
        {
            return _repository.Update(person);
        }
    }
}
