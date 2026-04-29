using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public CategoryFinality Finality { get; set; }

        [JsonIgnore]
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }

    public enum CategoryFinality
    {
        Receita = 1,
        Despesa = 2,
        Ambas = 3
    }
}