namespace CreditFlow.Model
{
    public class CreditoPessoaFisica : Credito
    {
        public CreditoPessoaFisica(decimal valorCredito, int quantidadeParcelas, DateTime dataPrimeiroVencimento)
        {
            TipoCredito = "CreditoPessoaFisica";
            ValorCredito = valorCredito;
            QuantidadeParcelas = quantidadeParcelas;
            DataPrimeiroVencimento = dataPrimeiroVencimento;
        }
        public override decimal Taxa => 0.03m;
    }
}
