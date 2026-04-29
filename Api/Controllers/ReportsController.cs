using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReportsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("totals-by-person")]
        public async Task<ActionResult<GeneralReportDto>> GetTotalsByPerson()
        {
            // Puxamos as pessoas e suas transações
            var persons = await _context.Persons
                .Include(p => p.Transactions)
                .ToListAsync();

            var report = new GeneralReportDto();

            foreach (var p in persons)
            {
                var totalReceitas = p.Transactions
                    .Where(t => t.Type == TransactionType.Receita)
                    .Sum(t => t.Amount);

                var totalDespesas = p.Transactions
                    .Where(t => t.Type == TransactionType.Despesa)
                    .Sum(t => t.Amount);

                report.Pessoas.Add(new PersonTotalDto
                {
                    Name = p.Name,
                    TotalReceitas = totalReceitas,
                    TotalDespesas = totalDespesas,
                    Saldo = totalReceitas - totalDespesas
                });
            }

            // Cálculos do Total Geral
            report.TotalGeralReceitas = report.Pessoas.Sum(x => x.TotalReceitas);
            report.TotalGeralDespesas = report.Pessoas.Sum(x => x.TotalDespesas);
            report.SaldoLiquidoGeral = report.TotalGeralReceitas - report.TotalGeralDespesas;

            return report;
        }
    }
}