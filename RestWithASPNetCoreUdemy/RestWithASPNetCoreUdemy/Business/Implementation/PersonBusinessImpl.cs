using System.Collections.Generic;
using RestWithASPNetCoreUdemy.Repository.Generic;
using RestWithASPNetCoreUdemy.Data.VO;
using RestWithASPNetCoreUdemy.Data.Converters;
using RestWithASPNetCoreUdemy.Model;

namespace RestWithASPNetCoreUdemy.Business
{
    public class PersonBusinessImpl : IPersonBusiness
    {
        private IRepository<Person> _repository;

        private readonly PersonConverter _converter;

        public PersonBusinessImpl(IRepository<Person> repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public PersonVO Create(PersonVO person)
        {
            //recebe o VO e converte para a entidade a ser persistida
            var personEntity = _converter.Parse(person);
            //recebe a entidade persistida
            personEntity = _repository.Create(personEntity);
            //converte de volta para VO
            return _converter.Parse(personEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<PersonVO> FindAll()
        {
            return _converter.ParseList(_repository.FindAll());
        }

        public PersonVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public PersonVO Update(PersonVO person)
        {
            //recebe o VO e converte para a entidade a ser persistida
            var personEntity = _converter.Parse(person);
            //recebe a entidade persistida
            personEntity = _repository.Update(personEntity);
            //converte de volta para VO
            return _converter.Parse(personEntity);
        }
    }
}
