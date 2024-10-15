using System.Text.Json.Serialization;

namespace WEB_253503_Kudosh.Domain.Entities
{
    public class TelescopeEntity
    {
        public TelescopeEntity()
        {
        }
        public TelescopeEntity(int id, string name, string description, CategoryEntity category, double price, string imgPath)
        {
            Id = id;
            Name = name;
            Description = description;
            Category = category;
            CategoryId = Category.Id;
            Price = price;
            ImagePath = imgPath;
        }

        public TelescopeEntity(int id, string name, string description, CategoryEntity category, double price, string imgPath, string mimeType)
        {
            Id = id;
            Name = name;
            Description = description;
            Category = category;
            Price = price;
            ImagePath = imgPath;
            MimeType = mimeType;
        }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("categoryId")]
        public int CategoryId { get; set; }

        [JsonPropertyName("category")]
        public CategoryEntity Category { get; set; }

        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("imagePath")]
        public string ImagePath { get; set; }

        [JsonPropertyName("mimeType")]
        public string MimeType { get; set; }
    }
}