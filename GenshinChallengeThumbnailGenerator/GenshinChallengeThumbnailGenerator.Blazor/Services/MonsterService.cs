using GenshinChallengeThumbnailGenerator.Blazor.Models;
using Newtonsoft.Json.Linq;

namespace GenshinChallengeThumbnailGenerator.Blazor.Services;

public class MonsterService : IMonsterService
{
    private readonly IStateService _stateService;

    public MonsterService(IStateService stateService) => _stateService = stateService;

    public async Task<List<Monster>> LoadMonstersFromApi()
    {
        HttpClient client = new();

        var response = await client.GetAsync("https://api.ambr.top/v2/en/monster");
        JObject json = JObject.Parse(await response.Content.ReadAsStringAsync());
        var monstersFromJson = json["data"]["items"];

        var stringformat = monstersFromJson.ToString();

        List<Monster> monsters = new();

        foreach(var x in monstersFromJson)
        {
            foreach(var child in x.Children())
            {
                monsters.Add(new()
                {
                    Id = Convert.ToInt32(child["id"].ToString()),
                    Name = child["name"].ToString(),
                    Type = child["type"].ToString(),
                    Icon = child["icon"].ToString(),
                    Route = child["route"].ToString(),
                });
            }
        }

        // Add image url to all monsters.
        for (int i = 0; i < monsters.Count; i++)
        {
            monsters[i].ImageUrl = await GetImageUrlAsync(monsters[i]);
        }

        // Add the new list to the persistence thing.
        _stateService.AddNewMonsterList(monsters);

        return monsters;
    }

    public async Task<List<Monster>> GetAllAsync()
    {
        if(!_stateService.MonstersIsLoaded)
        {
            await LoadMonstersFromApi();
        }

        return _stateService.Monsters;
    }

    public async Task<List<Monster>> GetAllBigMonsters()
    {
        if (!_stateService.MonstersIsLoaded)
        {
            await LoadMonstersFromApi();
        }

        return _stateService.Monsters
            .Where(x => x.Type == "BOSS"
                || x.Type == "ELEMENTAL"
                || x.Type == "BEAST")
            .Where(x => !x.Name.Contains("Slime")
                && !x.Name.Contains("Cicin")
                && !x.Name.Contains("Fungus")
                && !x.Name.Contains("Specter")
                && !x.Name.Contains("Hatchling")
                && !x.Name.Contains("Eye of the Storm")
                && !x.Name.Contains("Whopperflower"))
            .OrderBy(x => x.Name)
            .ToList(); ;
    }

    public async Task<string> GetImageUrlAsync(Monster monster)
    {
        string imageUrl = $"https://api.ambr.top/assets/UI/monster/{monster.Icon}.png";

        return imageUrl;
    }
}
