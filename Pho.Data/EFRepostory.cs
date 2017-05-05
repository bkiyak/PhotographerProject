using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pho.Data
{
    public class Repository<TObject> : IRepository<TObject>

         where TObject : class
    {
        protected PhotoContext _Context;
      
        private bool shareContext = false;

        public Repository()
        {
            _Context=new PhotoContext();
        }

        public Repository(PhotoContext context)
        {

            _Context = context;
            shareContext = true;
        }

        protected DbSet<TObject> DbSet
        {
            get
            {
                return _Context.Set<TObject>();
            }
        }

        public void Dispose()
        {
            if (shareContext && (_Context != null))
                _Context.Dispose();
        }
        
        public virtual IQueryable<TObject> All()
        {
            return DbSet.AsQueryable();
        }

        public virtual  IQueryable<TObject>   Filter(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.Where(predicate).AsQueryable<TObject>();
        }


        public virtual IQueryable<TObject> Filter(Expression<Func<TObject, bool>> filter,
          int index = 0, int size = 50)
        {
            int skipCount = index * size;
            var _resetSet = filter != null ? DbSet.Where(filter).AsQueryable() :
                DbSet.AsQueryable();
            _resetSet = skipCount == 0 ? _resetSet.Take(size) :
                _resetSet.Skip(skipCount).Take(size);
            return _resetSet.AsQueryable();
        }

        public bool Contains(Expression<Func<TObject, bool>> predicate)
        {
            return  DbSet.Count(predicate) > 0;
        }

        public virtual TObject Find(params object[] keys)
        {
            return DbSet.Find(keys);
        }

        public virtual TObject Find(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public virtual TObject Create(TObject TObject)
        {
            var newEntry = DbSet.Add(TObject);
            if (!shareContext)
                _Context.SaveChanges();
            return newEntry;
        }

       

        public virtual int Count
        {
            get
            {
                return DbSet.Count();
            }
        }

        public virtual int Delete(TObject TObject)
        {
            DbSet.Remove(TObject);
            if (!shareContext)
                return _Context.SaveChanges();
            return 0;
        }

        public virtual int Update(TObject TObject)
        {
            var entry = _Context.Entry(TObject);
            DbSet.Attach(TObject);
            entry.State = EntityState.Modified;
            if (!shareContext)
                return _Context.SaveChanges();
            return 0;
        }

        public virtual int Delete(Expression<Func<TObject, bool>> predicate)
        {
            var objects = Filter(predicate);
            foreach (var obj in objects)
                DbSet.Remove(obj);
            if (!shareContext)
                return _Context.SaveChanges();
            return 0;
        }


    }
    }

