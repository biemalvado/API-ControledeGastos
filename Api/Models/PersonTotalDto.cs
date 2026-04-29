namespace Api.Models
{
    public class PersonTotalDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }
        public decimal Saldo { get; set; }
    }

    public class GeneralReportDto
    {
        public List<PersonTotalDto> Pessoas { get; set; } = new();
        public decimal TotalGeralReceitas { get; set; }
        public decimal TotalGeralDespesas { get; set; }
        public decimal SaldoLiquidoGeral { get; set; }
    }
}