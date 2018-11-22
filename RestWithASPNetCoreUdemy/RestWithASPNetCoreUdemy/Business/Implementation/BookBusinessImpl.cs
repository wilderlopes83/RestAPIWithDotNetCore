using System.Collections.Generic;
using RestWithASPNetCoreUdemy.Repository.Generic;
using RestWithASPNetCoreUdemy.Data.Converters;
using RestWithASPNetCoreUdemy.Data.VO;
using RestWithASPNetCoreUdemy.Model;

namespace RestWithASPNetCoreUdemy.Business
{
    public class BookBusinessImpl : IBookBusiness
    {
        private IRepository<Book> _repository;

        private readonly BookConverter _converter;

        public BookBusinessImpl(IRepository<Book> repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }

        public BookVO Create(BookVO book)
        {
            //recebe o VO e converte para a entidade a ser persistida
            var bookEntity = _converter.Parse(book);
            //recebe a entidade persistida
            bookEntity = _repository.Create(bookEntity);
            //converte de volta para VO
            return _converter.Parse(bookEntity);            
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<BookVO> FindAll()
        {
            return _converter.ParseList(_repository.FindAll());
        }

        public BookVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public BookVO Update(BookVO book)
        {
            //recebe o VO e converte para a entidade a ser persistida
            var bookEntity = _converter.Parse(book);
            //recebe a entidade persistida
            bookEntity = _repository.Update(bookEntity);
            //converte de volta para VO
            return _converter.Parse(bookEntity);            
        }
    }
}
