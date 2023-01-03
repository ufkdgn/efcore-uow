using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.DataOperation.Framework
{
    public class BaseUnitOfWork<T> : IUnitOfWork, IDisposable where T : BaseDbContext
    {
        public HttpContext HttpContext { get; private set; }
        public BaseDbContext BaseContext { get { return Context; } }
        public T Context { get; private set; }

        private BaseUnitOfWork()
        {
        }

        public BaseUnitOfWork(T context)
        {
            this.Context = context;
        }

        public BaseUnitOfWork(T context, HttpContext httpContext) : this(context)
        {
            this.HttpContext = httpContext;
        }

        public void SaveChanges()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException concurrencyException)
            {
                throw concurrencyException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            Context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
