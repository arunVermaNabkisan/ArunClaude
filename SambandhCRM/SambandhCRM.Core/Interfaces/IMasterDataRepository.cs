using SambandhCRM.Core.Models;

namespace SambandhCRM.Core.Interfaces;

public interface IMasterDataRepository : IRepository<MasterData>
{
    Task<IEnumerable<MasterData>> GetByCategoryAsync(string category);
    Task<bool> DeleteByCategoryAndValueAsync(string category, string value);
}
