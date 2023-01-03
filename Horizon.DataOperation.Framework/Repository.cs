using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Horizon.DataOperation.Framework
{
    public class Repository<T> where T : DbModel, new()
    {
        private DbSet<T> dbSet;
        private DbContext context;

        public Repository(DbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        /// <summary>
        /// Removes matching entities
        /// </summary>
        /// <param name="predicate"></param>
        public void Delete(Expression<Func<T, bool>> predicate)
        {
            // get all matching entries as Queryable
            var entries = dbSet.Where(predicate).AsQueryable();
            // then delete each one
            foreach (var item in entries) Delete(item);
        }

        /// <summary>
        /// Removes entity which matches with given id
        /// </summary>
        /// <param name="id"></param>
        public void Delete(object id)
        {
            // find entry by using its Id/PK
            T? entry = dbSet?.Find(id);
            // then delete if exists
            if (entry != null) Delete(entry);
        }

        /// <summary>
        /// Removes given entity
        /// </summary>
        /// <param name="entry"></param>
        public void Delete(T entry)
        {
            // Attach entry if it is detached
            if (context.Entry(entry).State == EntityState.Detached) dbSet.Attach(entry);
            // then delete
            dbSet.Remove(entry);
        }

        /// <summary>
        /// Determines whether any entity exists
        /// </summary>
        public bool Any()
        {
            return dbSet.Any();
        }

        /// <summary>
        /// Determines whether entity with given id exists
        /// </summary>
        /// <param name="id"></param>
        public bool Any(object id)
        {
            return dbSet.Find(id) != null;
        }

        /// <summary>
        /// Returns DbSet instance in IQueryable form
        /// </summary>
        public IQueryable<T> Get()
        {
            return dbSet.AsQueryable();
        }

        /// <summary>
        /// Returns filtered sequence in IQueryable form
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition</param>
        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate).AsQueryable();
        }

        /// <summary>
        /// Returns included and filtered sequence in IQueryable form
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition</param>
        public IQueryable<T> Get(Expression<Func<T, bool>> predicate, params string[] include)
        {
            // aggregate all includes on dbSet
            IQueryable<T> body = include.Aggregate(dbSet as IQueryable<T>, (set, inc) => set.Include(inc));
            // apply where predicate
            return body.Where(predicate);
        }

        /// <summary>
        /// Returns entity with given id
        /// </summary>
        /// <param name="id">Primary Key</param>
        public T? GetById(object id)
        {
            // find entry by using its Id/PK
            return dbSet?.Find(id);
        }

        /// <summary>
        /// Inserts given entity.
        /// </summary>
        /// <param name="entry">The entity to add.</param>
        public void Insert(T entry)
        {
            this.dbSet.Add(entry);
        }

        /// <summary>
        /// Updates given entity
        /// </summary>
        /// <param name="entry">The entity to update.</param>
        public void Update(T entry)
        {
            this.dbSet.Attach(entry);
            context.Entry(entry).State = EntityState.Modified;
        }
    }
}
