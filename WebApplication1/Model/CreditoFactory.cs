namespace CreditFlow.Model
{
    public class CreditoFactory
    {
        public Credito CreateCredito(string tipoCredito, decimal valorCredito, int quantidadeParcelas, DateTime primeiroVencimento)
        {
            switch (tipoCredito)
            {
                case "CreditoDireto":
                    return new CreditoDireto
                    (
                        valorCredito,
                        quantidadeParcelas,
                        primeiroVencimento
                    );
                // Adicione casos para os outros tipos de crédito aqui...
                default:
                    throw new ArgumentException("Tipo de crédito inválido.", nameof(tipoCredito));
            }
        }
    }

}
