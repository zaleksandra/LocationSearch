using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repository
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly LocationDatabaseContext _context;
        private DbSet<T> entities;
        public Repository(LocationDatabaseContext context)
        {
            _context = context;
            entities = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }
        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return entities.Where(expression).AsEnumerable();
        }
        public T Get(long id)
        {
            return entities.SingleOrDefault(s => s.Id == id);
        }
        public long Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            _context.SaveChanges();
        }
    }

}
