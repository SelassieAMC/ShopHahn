using Hahn.ApplicationProcess.February2021.Domain.Interfaces;
using System;

namespace Hahn.ApplicationProcess.February2021.Data.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IAssetRepository _assetRepo;
        private readonly ApplicationDbContext _context;
        private bool disposed = false;

        public UnitOfWork(IAssetRepository assetRepository, ApplicationDbContext context)
        {
            _assetRepo = assetRepository;
            _context = context;
        }

        public IAssetRepository AssetRepository
        {
            get
            {
                return _assetRepo;
            }
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                _context.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
