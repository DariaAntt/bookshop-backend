public class BookFilterDTO
{
    public List<string>? Categories { get; set; }
    public double? MinPrice { get; set; }
    public double? MaxPrice { get; set; }
    public List<string>? Authors { get; set; }
    public List<string>? Publishers { get; set; }
    public string? BindingType { get; set; }
    public int? MinYear { get; set; }
    public int? MaxYear { get; set; }
    public List<int>? AgeLimits { get; set; }
}