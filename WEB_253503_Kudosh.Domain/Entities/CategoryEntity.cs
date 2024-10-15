using System.Text.Json.Serialization;

namespace WEB_253503_Kudosh.Domain.Entities
{
    public class CategoryEntity
    {
        public CategoryEntity() { }
        public CategoryEntity(CategoryEntity entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            NormalizedName = entity.NormalizedName;
        }
        public CategoryEntity(int id, string name, string normalizedName)
        {
            Id = id;
            Name = name;
            NormalizedName = normalizedName;
        }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("normalizedName")]
        public string NormalizedName { get; set; }
    }
}
