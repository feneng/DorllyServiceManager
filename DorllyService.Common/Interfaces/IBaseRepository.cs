using System;
using System.Threading.Tasks;

namespace DorllyService.Common.Interfaces
{
    public interface IBaseRepository:IDisposable
    {
        void Commit();
        Task CommitAsync();
    }
}
