using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RestWithASPNetCoreUdemy.Model.Base;
using RestWithASPNetCoreUdemy.Model.Context;

namespace RestWithASPNetCoreUdemy.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {

        protected readonly MySQLContext _context;
        private DbSet<T> dataset; 
        public GenericRepository(MySQLContext context)
        {
            this._context = context;
            this.dataset = this._context.Set<T>();
        }

        public T Create(T item)
        {
            try
            {
                dataset.Add(item);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return item;
        }

        public void Delete(long id)
        {
            var result = dataset.SingleOrDefault(p=> p.Id.Equals(id));

            try
            {
                if (result != null)
                { 
                    dataset.Remove(result);
                    _context.SaveChanges();
                }               
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool Exist(long? id)
        {
            return dataset.Any(p => p.Id.Equals(id));
        }

        public List<T> FindAll()
        {
            return dataset.ToList(); 
        }

        public T FindById(long id)
        {
            return dataset.SingleOrDefault(p => p.Id.Equals(id));
        }

        public T Update(T item)
        {
            if (!Exist(item.Id)) return null;

            var result = dataset.SingleOrDefault(p=> p.Id.Equals(item.Id));

            try
            {
                _context.Entry(result).CurrentValues.SetValues(item);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return item;
        }

        public List<T> FindyWithPagedSearch(string query)
        {
            return this.dataset.FromSql(query).ToList();
        }

        public int GetCount(string query)
        {
            return this.dataset.FromSql(query).Count();
        }
    }
}