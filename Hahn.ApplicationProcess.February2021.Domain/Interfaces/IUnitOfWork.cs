using Hahn.ApplicationProcess.February2021.Domain.Models;
using System;

namespace Hahn.ApplicationProcess.February2021.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IAssetRepository AssetRepository { get; }
        public void Commit();
    }
}
