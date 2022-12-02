using GenshinChallengeThumbnailGenerator.Blazor.Models;

namespace GenshinChallengeThumbnailGenerator.Blazor.Services;

public class StateService : IStateService
{
    private List<Monster>? _monsters;
    public List<Monster>? Monsters 
    {
        get => _monsters; 
    }

    public bool MonstersIsLoaded => (Monsters is not null && Monsters.Count > 0) ? true : false;

    public void AddMonster(Monster monster)
    {
        if(Monsters is null)
        {
            _monsters = new();
        }

        _monsters.Add(monster);
    }

    public void AddNewMonsterList(List<Monster> monsters)
    {
        if (Monsters is null)
        {
            _monsters = new();
        }

        _monsters = monsters;
    }
}
