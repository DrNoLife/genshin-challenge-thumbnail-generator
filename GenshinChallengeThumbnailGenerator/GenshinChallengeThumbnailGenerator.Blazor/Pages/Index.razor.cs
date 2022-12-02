using GenshinChallengeThumbnailGenerator.Blazor.Models;
using GenshinChallengeThumbnailGenerator.Blazor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace GenshinChallengeThumbnailGenerator.Blazor.Pages;

public partial class Index
{
    [Inject]
    public IMonsterService MonsterService { get; set; }

    public List<Monster> Monsters { get; set; }

    public string BackgroundImageBase64 { get; set; } = String.Empty;

    public string Title { get; set; } = "Challenge Title";

    protected override async Task OnInitializedAsync()
    {
        Monsters = await MonsterService.GetAllBigMonsters();
    }

    private async Task LoadFiles(InputFileChangeEventArgs e)
    {
        // Set max upload size.
        long maxFileSize = 1024 * 1024 * 50; 

        var readStream = e.File.OpenReadStream(maxFileSize);
        var buf = new byte[readStream.Length];
        var ms = new MemoryStream(buf);
        await readStream.CopyToAsync(ms);
        var buffer = ms.ToArray();

        BackgroundImageBase64 = Convert.ToBase64String(buffer);
    }
}
