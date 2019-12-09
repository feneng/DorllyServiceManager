using System;
using System.Threading;
using System.Threading.Tasks;
using DorllyService.Common.Interfaces;

namespace DorllyService.Domain
{
    public class BaseRepository:IBaseRepository
    {
        private readonly DorllyServiceManagerContext _context;
        public BaseRepository(DorllyServiceManagerContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.Collect();
        }
    }
}
