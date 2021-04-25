using System.Collections.Generic;

namespace SqlCommandExample.Repositories
{
    public interface IRepository<T>
    {
        T Add(T entity);
        T Update(T entity);
        bool Remove(T entity);
        T Get(int id);
        IEnumerable<T> GetAll();
    }
}
