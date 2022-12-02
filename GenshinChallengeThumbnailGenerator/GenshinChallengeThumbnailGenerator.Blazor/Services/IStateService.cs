using GenshinChallengeThumbnailGenerator.Blazor.Models;

namespace GenshinChallengeThumbnailGenerator.Blazor.Services;

public interface IStateService
{
    public List<Monster>? Monsters { get; }

    public bool MonstersIsLoaded { get; }

    public void AddMonster(Monster monster);
    public void AddNewMonsterList(List<Monster> monsters);
}
