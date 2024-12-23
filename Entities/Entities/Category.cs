using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Entities.Entities
{
    public class Category
    {
        public int Id { get; set; } 
        public string Label { get; set; } 
        public string Icon { get; set; } 
        public string Url { get; set; } 
        public int? CategoryId { get; set; }
        public List<Category> Children { get; set; } = new List<Category>();

        public bool IsExpanded { get; set; } = false; 
        public bool IsEditing { get; set; } = false; 
    }
}
