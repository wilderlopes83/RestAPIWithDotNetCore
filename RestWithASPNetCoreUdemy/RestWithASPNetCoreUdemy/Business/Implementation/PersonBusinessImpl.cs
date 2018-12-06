using System.Collections.Generic;
using RestWithASPNetCoreUdemy.Repository.Generic;
using RestWithASPNetCoreUdemy.Data.VO;
using RestWithASPNetCoreUdemy.Data.Converters;
using RestWithASPNetCoreUdemy.Model;
using RestWithASPNetCoreUdemy.Repository;
using Tapioca.HATEOAS.Utils;
using System.Text;

namespace RestWithASPNetCoreUdemy.Business
{
    public class PersonBusinessImpl : IPersonBusiness
    {
        //private IRepository<Person> _repository;
        private IPersonRepository _repository;

        private readonly PersonConverter _converter;

        public PersonBusinessImpl(IPersonRepository repository)
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

        public List<PersonVO> FindByName(string firstName, string lastName)
        {
            return _converter.ParseList(_repository.FindByName(firstName, lastName));
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

        public PagedSearchDTO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            //tratamento em relação ao banco mysql, que considera a página 1 (offset) = 0
            var offset = ((page-1) * pageSize);

            StringBuilder query = new StringBuilder();
            query.Append(@" select * from persons p where 1 = 1 ");
            if (!string.IsNullOrEmpty(name)) query.Append($"   and p.FirstName like '%{name}%' ");
            query.Append($"  order by p.FirstName {sortDirection} limit {pageSize} offset {offset} ");

            StringBuilder queryCount = new StringBuilder();
            queryCount.Append(@" select 1 from persons p where 1 = 1 ");
            if (!string.IsNullOrEmpty(name)) queryCount.Append($"   and p.FirstName like '%{name}%' ");            

            var persons = _converter.ParseList(_repository.FindyWithPagedSearch(query.ToString()));
            var totalResults = _repository.GetCount(queryCount.ToString());

            return new PagedSearchDTO<PersonVO>
            {
                CurrentPage = page,
                List = persons,
                PageSize = pageSize,
                SortDirections = sortDirection,
                TotalResults = totalResults
            };
        }
    }
}
