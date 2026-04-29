namespace Api.Models;

public class Transaction
{
    public int Id { get; set; }
    public string Description { get; set; } = "";
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }

    public int PersonId { get; set; }
    public Person? Person { get; set; } // Aqui ele acha a classe Person

    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}