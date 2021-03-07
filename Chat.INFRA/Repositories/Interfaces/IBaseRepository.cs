using System;
using System.Collections.Generic;
using System.Linq;

namespace Chat.Infra.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        public T Add(T obj);

        public T Update(T obj);

        public T Get(int id);
        public T Get(Func<T, bool> exp);

        public IEnumerable<T> AddRange(IEnumerable<T> objs);

        public List<T> AddRange(List<T> objs);

        public IEnumerable<T> UpdateRange(IEnumerable<T> objs);

        public List<T> UpdateRange(List<T> objs);

        public void Delete(int id);

        public void DeleteRange(IEnumerable<T> objs);

        public void DeleteRange(List<T> objs);

        public IEnumerable<T> GetAsEnumerable(Func<T, bool> exp);

        public List<T> GetAsList(Func<T, bool> exp);

        public IQueryable<T> GetAsQueryable();


    }
}
