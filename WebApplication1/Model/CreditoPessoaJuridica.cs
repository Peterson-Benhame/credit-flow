namespace CreditFlow.Model
{
    public class CreditoPessoaJuridica : Credito
    {
        public CreditoPessoaJuridica(decimal valorCredito, int quantidadeParcelas, DateTime dataPrimeiroVencimento)
        {
            TipoCredito = "CreditoPessoaJuridica";
            ValorCredito = valorCredito;
            QuantidadeParcelas = quantidadeParcelas;
            DataPrimeiroVencimento = dataPrimeiroVencimento;
        }
        public override decimal Taxa => 0.05m;
    }
}
