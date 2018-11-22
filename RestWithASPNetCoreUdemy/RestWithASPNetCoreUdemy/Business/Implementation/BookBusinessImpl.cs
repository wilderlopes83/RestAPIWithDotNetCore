﻿using System;
using System.Collections.Generic;
using System.Threading;
using RestWithASPNetCoreUdemy.Model;
using RestWithASPNetCoreUdemy.Model.Context;
using System.Linq;
using RestWithASPNetCoreUdemy.Repository;
using RestWithASPNetCoreUdemy.Repository.Generic;

namespace RestWithASPNetCoreUdemy.Business
{
    public class BookBusinessImpl : IBookBusiness
    {
        private IRepository<Book> _repository;

        public BookBusinessImpl(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public Book Create(Book book)
        {
            return _repository.Create(book);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<Book> FindAll()
        {
            return _repository.FindAll();
        }

        public Book FindById(long id)
        {
            return _repository.FindById(id);
        }

        public Book Update(Book book)
        {
            return _repository.Update(book);
        }
    }
}
