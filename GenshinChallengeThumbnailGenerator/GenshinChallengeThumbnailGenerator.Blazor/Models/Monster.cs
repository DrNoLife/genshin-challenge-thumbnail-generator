namespace GenshinChallengeThumbnailGenerator.Blazor.Models;

public class Monster
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Type { get; set; } = String.Empty;
    public string Icon { get; set; } = String.Empty;
    public string Route { get; set; } = String.Empty;
    public bool IsSelected { get; set; } = false;
    public string ImageUrl { get; set; } = String.Empty;
}
