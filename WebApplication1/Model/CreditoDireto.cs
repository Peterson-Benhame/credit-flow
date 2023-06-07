namespace CreditFlow.Model
{
    public class CreditoDireto : Credito
    {
        public CreditoDireto(decimal valorCredito, int quantidadeParcelas, DateTime dataPrimeiroVencimento)
        {
            TipoCredito = "CreditoDireto";
            ValorCredito = valorCredito;
            QuantidadeParcelas = quantidadeParcelas;
            DataPrimeiroVencimento = dataPrimeiroVencimento;
        }
        public override decimal Taxa => 0.02m;
    }
}
