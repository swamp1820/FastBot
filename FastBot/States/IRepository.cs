using System.Collections.Generic;

namespace FastBot.States
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Update(T entity);
        IEnumerable<T> GetAll();
        T Get(long id);
    }
}