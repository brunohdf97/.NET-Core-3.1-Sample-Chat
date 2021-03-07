using Chat.Domain.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chat.Infra.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public override User Add(User obj)
        {
            return base.Add(obj);
        }

        public override IEnumerable<User> AddRange(IEnumerable<User> objs)
        {
            return base.AddRange(objs);
        }

        public override List<User> AddRange(List<User> objs)
        {
            return base.AddRange(objs);
        }

        public override void Delete(int id)
        {
            base.Delete(id);
        }

        public override void DeleteRange(IEnumerable<User> objs)
        {
            base.DeleteRange(objs);
        }

        public override void DeleteRange(List<User> objs)
        {
            base.DeleteRange(objs);
        }

        public override User Get(int id)
        {
            return base.Get(id);
        }

        public override User Get(Func<User, bool> exp)
        {
            return base.Get(exp);
        }

        public override IEnumerable<User> GetAsEnumerable(Func<User, bool> exp)
        {
            return base.GetAsEnumerable(exp);
        }

        public override List<User> GetAsList(Func<User, bool> exp)
        {
            return base.GetAsList(exp);
        }

        public override IQueryable<User> GetAsQueryable()
        {
            return base.GetAsQueryable();
        }

        public override User Update(User obj)
        {
            return base.Update(obj);
        }

        public override IEnumerable<User> UpdateRange(IEnumerable<User> objs)
        {
            return base.UpdateRange(objs);
        }

        public override List<User> UpdateRange(List<User> objs)
        {
            return base.UpdateRange(objs);
        }
    }
}
