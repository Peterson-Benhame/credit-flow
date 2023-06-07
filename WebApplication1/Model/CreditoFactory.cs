namespace CreditFlow.Model
{
    public class CreditoFactory
    {
        public Credito CreateCredito(string tipoCredito, decimal valorCredito, int quantidadeParcelas, DateTime primeiroVencimento)
        {
            switch (tipoCredito)
            {
                case "CreditoConsignado":
                    return new CreditoConsignado
                    (
                        valorCredito,
                        quantidadeParcelas,
                        primeiroVencimento
                    );
                case "CreditoDireto":
                    return new CreditoDireto
                    (
                        valorCredito,
                        quantidadeParcelas,
                        primeiroVencimento
                    );
                case "CreditoImobiliario":
                    return new CreditoImobiliario
                    (
                        valorCredito,
                        quantidadeParcelas,
                        primeiroVencimento
                    );
                case "CreditoPessoaFisica":
                    return new CreditoPessoaFisica
                    (
                        valorCredito,
                        quantidadeParcelas,
                        primeiroVencimento
                    );
                case "CreditoPessoaJuridica":
                    return new CreditoPessoaJuridica
                    (
                        valorCredito,
                        quantidadeParcelas,
                        primeiroVencimento
                    );
                default:
                    throw new ArgumentException("Tipo de crédito inválido.", nameof(tipoCredito));
            }
        }
    }

}
