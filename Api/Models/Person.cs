using System.Text.Json.Serialization; // Importante para o JsonIgnore

namespace Api.Models;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Age { get; set; }

    [JsonIgnore] // Isso corta o ciclo infinito e resolve o Erro 500
    public List<Transaction> Transactions { get; set; } = new();
}