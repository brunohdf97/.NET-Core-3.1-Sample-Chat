//using Chat.Infra.Repositories.Interfaces;
using Chat.INFRA.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chat.Infra.Repositories
{
    public abstract class BaseRepository<T> where T : class/*, IBaseRepository<T>*/
    {
        protected MainContext bd = new MainContext();

        public virtual T Add(T obj)
        {
            bd.Set<T>().Add(obj);
            bd.SaveChanges();
            return obj;
        }

        public virtual IEnumerable<T> AddRange(IEnumerable<T> objs)
        {
            foreach (var obj in objs)
            {
                bd.Set<T>().Add(obj);
            }
            bd.SaveChanges();
            return objs;
        }

        public virtual List<T> AddRange(List<T> objs)
        {
            foreach (var obj in objs)
            {
                bd.Set<T>().Add(obj);
            }
            bd.SaveChanges();
            return objs;
        }

        public virtual void Delete(int id)
        {
            var fro = Get(id);

            bd.Set<T>().Remove(fro);
            bd.SaveChanges();
        }

        public virtual void DeleteRange(IEnumerable<T> objs)
        {
            foreach (var obj in objs)
            {
                bd.Set<T>().Remove(obj);
            }
            bd.SaveChanges();
        }

        public virtual void DeleteRange(List<T> objs)
        {
            foreach (var obj in objs)
            {
                bd.Set<T>().Remove(obj);
            }
            bd.SaveChanges();
        }

        public virtual T Get(int id)
        {
            return bd.Set<T>().Find(id);
        }

        public virtual T Get(Func<T, bool> exp)
        {
            return bd.Set<T>().Where(exp).FirstOrDefault();
        }

        public virtual IEnumerable<T> GetAsEnumerable(Func<T, bool> exp)
        {
            return bd.Set<T>().Where(exp).AsEnumerable();
        }

        public virtual List<T> GetAsList(Func<T, bool> exp)
        {
            return bd.Set<T>().Where(exp).ToList();
        }

        public virtual IQueryable<T> GetAsQueryable()
        {
            return bd.Set<T>().AsQueryable();
        }

        public virtual T Update(T obj)
        {
            bd.Entry<T>(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            bd.SaveChanges();
            return obj;
        }

        public virtual IEnumerable<T> UpdateRange(IEnumerable<T> objs)
        {
            foreach (var obj in objs)
            {
                bd.Entry<T>(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            bd.SaveChanges();

            return objs;
        }

        public virtual List<T> UpdateRange(List<T> objs)
        {
            foreach (var obj in objs)
            {
                bd.Set<T>().Update(obj);
            }
            bd.SaveChanges();

            return objs;
        }
    }
}
