using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransactionsController(AppDbContext context)
        {
            _context = context;
        }

        // ROTA PARA LISTAR (GET)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            // O Include garante que a transação traga os dados da Pessoa e Categoria, não apenas os IDs
            return await _context.Transactions
                .Include(t => t.Person)
                .Include(t => t.Category)
                .ToListAsync();
        }

        // ROTA PARA CRIAR (POST) COM AS REGRAS DE NEGÓCIO
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
        {
            // 1. Buscamos a Pessoa e a Categoria no banco para validar as regras
            var person = await _context.Persons.FindAsync(transaction.PersonId);
            var category = await _context.Categories.FindAsync(transaction.CategoryId);

            if (person == null || category == null)
            {
                return BadRequest("Pessoa ou Categoria inválida.");
            }

            // 2. REGRA DO MENOR DE IDADE
            if (person.Age < 18 && transaction.Type == TransactionType.Receita)
            {
                return BadRequest("Atenção: Menores de 18 anos só podem registrar despesas.");
            }

            // 3. REGRA DA CATEGORIA x TIPO DA TRANSAÇÃO
            if (transaction.Type == TransactionType.Despesa && category.Finality == CategoryFinality.Receita)
            {
                return BadRequest("Atenção: Você não pode usar uma categoria de Receita para lançar uma Despesa.");
            }

            if (transaction.Type == TransactionType.Receita && category.Finality == CategoryFinality.Despesa)
            {
                return BadRequest("Atenção: Você não pode usar uma categoria de Despesa para lançar uma Receita.");
            }

            // Se passou por todas as regras, nós salvamos!
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTransactions), new { id = transaction.Id }, transaction);
        }
    }
}