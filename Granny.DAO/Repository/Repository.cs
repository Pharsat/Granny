using Granny.DAO.Context;
using Granny.DAO.Repository.Interface;
using Granny.DAO.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Granny.DAO.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly GrannyContext grannyContext;

        protected DbSet<T> ObjectSet { get; private set; }


        public Repository(IUnitOfWork unitOfWork)
        {
            this.grannyContext = unitOfWork.grannyContext;
            this.ObjectSet = this.grannyContext.Set<T>();
        }

        public void Add(T entity)
        {
            this.grannyContext.Set<T>().Add(entity);
        }

        public void Add(List<T> entities)
        {
            this.grannyContext.Set<T>().AddRange(entities);
        }

        public void Delete(T entity)
        {
            this.grannyContext.Set<T>().Remove(entity);
        }

        public T Get(long id)
        {
            return this.grannyContext.Set<T>().Find(id);
        }

        public List<T> getAll()
        {
            return this.grannyContext.Set<T>().ToList<T>();
        }

        public void Update(T entity)
        {
            this.grannyContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
