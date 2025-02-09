using Newtonsoft.Json;

namespace EverfitExam.Models;

public class MealPlanInfo
{
    [JsonProperty("cover_image")]
    public string CoverImage { get; set; }
    
    [JsonProperty("name")]
    public string MealPlanName { get; set; }
    
    [JsonProperty("number_of_weeks")]
    public int NoOfWeek { get; set; }
    
    [JsonProperty("owner")]
    public string Owner { get; set; }
    
    [JsonProperty("share")]
    public string ShareWithOrg { get; set; }
    
    [JsonProperty("description")]
    public string Description { get; set; }
}