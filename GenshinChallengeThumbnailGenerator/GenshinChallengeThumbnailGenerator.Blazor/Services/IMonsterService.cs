using GenshinChallengeThumbnailGenerator.Blazor.Models;

namespace GenshinChallengeThumbnailGenerator.Blazor.Services;

public interface IMonsterService
{
    public Task<List<Monster>> GetAllAsync();

    public Task<List<Monster>> GetAllBigMonsters();

    public Task<string> GetImageUrlAsync(Monster monster);
}
