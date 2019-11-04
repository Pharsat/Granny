using System.Collections.Generic;

namespace Granny.DAO.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);

        void Add(List<T> entities);

        void Delete(T entity);

        T Get(long id);

        List<T> getAll();

        void Update(T entity);
    }
}
