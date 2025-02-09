using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EverfitExam.Models;

public class CreateMealPlanResponse
{
    public class Author
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("is_trainer")]
        public bool? IsTrainer { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }
    }

    public class Data
    {
        [JsonProperty("author")]
        public Author Author { get; set; }

        [JsonProperty("team")]
        public string Team { get; set; }

        [JsonProperty("share")]
        public int? Share { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("is_edit_mode")]
        public bool? IsEditMode { get; set; }

        [JsonProperty("jobs_running")]
        public List<object> JobsRunning { get; set; }

        [JsonProperty("version")]
        public int? Version { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("name_lowercase")]
        public string NameLowercase { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("cover_image")]
        public string CoverImage { get; set; }

        [JsonProperty("number_of_clients")]
        public int? NumberOfClients { get; set; }

        [JsonProperty("number_of_weeks")]
        public int? NumberOfWeeks { get; set; }

        [JsonProperty("weeks")]
        public List<string> Weeks { get; set; }

        [JsonProperty("last_edit_by")]
        public string LastEditBy { get; set; }

        [JsonProperty("last_used")]
        public DateTime? LastUsed { get; set; }

        [JsonProperty("is_deleted")]
        public bool? IsDeleted { get; set; }

        [JsonProperty("from_template")]
        public bool? FromTemplate { get; set; }

        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("__v")]
        public int? V { get; set; }
    }

    public class Root
    {
        [JsonProperty("data")]
        public Data Data { get; set; }
    }
}