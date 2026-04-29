namespace Api.Models // Verifique se o "namespace" é o mesmo do seu projeto
{
    public enum TransactionType
    {
        Receita = 1,
        Despesa = 2
    }

    public enum CategoryFinalityEnum
    {
        Receita = 1,
        Despesa = 2,
        Ambas = 3
    }
}