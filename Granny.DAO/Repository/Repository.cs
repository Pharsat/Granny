using Granny.DAO.Context;
using Granny.DAO.Repository.Interface;
using Granny.DAO.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Granny.DAO.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly GrannyContext grannyContext;

        protected DbSet<T> ObjectSet { get; private set; }

        public Repository(IUnitOfWork unitOfWork)
        {
            grannyContext = unitOfWork.grannyContext;
            ObjectSet = grannyContext.Set<T>();
        }

        public void Add(T entity)
        {
            grannyContext.Set<T>().Add(entity);
        }

        public void Add(List<T> entities)
        {
            grannyContext.Set<T>().AddRange(entities);
        }

        public void Delete(T entity)
        {
            grannyContext.Set<T>().Remove(entity);
        }

        public T Get(long id)
        {
            return grannyContext.Set<T>().Find(id);
        }

        public List<T> getAll()
        {
            return grannyContext.Set<T>().ToList<T>();
        }

        public void Update(T entity)
        {
            grannyContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task AddAsync(T entity)
        {
            await grannyContext.AddAsync(entity);
        }
    }
}
