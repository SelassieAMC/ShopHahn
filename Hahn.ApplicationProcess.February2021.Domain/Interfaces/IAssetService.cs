using Hahn.ApplicationProcess.February2021.Domain.DTO;
using Hahn.ApplicationProcess.February2021.Domain.ResponseObjects;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Domain.Interfaces
{
    public interface IAssetService
    {
        public Task<UnitResult<AssetDto>> GetAssetById(int Id);
        public Task<UnitResult<AssetDto>> CreateAsset(AssetDto assetDto);
        public Task<UnitResult<AssetDto>> UpdateAsset(AssetDto assetDto);
        public Task<UnitResult<AssetDto>> DeleteAsset(int Id);
    }
}
