using ConsultaSystem.Domain.Interfaces.Repositories;
using ConsultaSystem.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace ConsultaSystem.Domain.Services
{
    public class GenericService<TEntity> : IDisposable, IGenericService<TEntity> where TEntity : class
    {

        private readonly IGenericRepository<TEntity> _repository;

        public GenericService(IGenericRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public void Add(TEntity obj)
        {
            _repository.Add(obj);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _repository.GetAll();

        }

        public TEntity GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Remove(TEntity obj)
        {
            _repository.Remove(obj);
        }

        public void Update(TEntity obj)
        {
            _repository.Update(obj);
        }


        public void Dispose()
        {
            _repository.Dispose();
        }

    }
}
