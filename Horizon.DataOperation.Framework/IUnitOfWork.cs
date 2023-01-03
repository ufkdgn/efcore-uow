using Microsoft.AspNetCore.Http;

namespace Horizon.DataOperation.Framework
{
    public interface IUnitOfWork
    {
        HttpContext HttpContext { get; }
        BaseDbContext BaseContext { get; }
    }
}
