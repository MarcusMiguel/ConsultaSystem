using ConsultaSystem.Domain.Interfaces.Repositories;
using ConsultaSystem.Infra.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace ConsultaSystem.Infra.Repositories
{
    public class GenericRepository<TEntity> : IDisposable, IGenericRepository<TEntity> where TEntity : class
    {
        protected ConsultaSystemContext _db = new ConsultaSystemContext();

        public void Add(TEntity obj)
        {
            _db.Set<TEntity>().Add(obj);
            _db.SaveChanges();
        }
 

        public IEnumerable<TEntity> GetAll()
        {
            return _db.Set<TEntity>().ToList();
        }

        public TEntity GetById(int id)
        {
            return _db.Set<TEntity>().Find(id);
         }

        public void Remove(TEntity obj)
        {
            _db.Set<TEntity>().Remove(obj);
            _db.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            _db.Set<TEntity>().AddOrUpdate(obj);
             _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
    