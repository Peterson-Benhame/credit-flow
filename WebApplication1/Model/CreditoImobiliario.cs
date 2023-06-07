namespace CreditFlow.Model
{
    public class CreditoImobiliario : Credito
    {
        public CreditoImobiliario(decimal valorCredito, int quantidadeParcelas, DateTime dataPrimeiroVencimento)
        {
            TipoCredito = "CreditoImobiliario";
            ValorCredito = valorCredito;
            QuantidadeParcelas = quantidadeParcelas;
            DataPrimeiroVencimento = dataPrimeiroVencimento;
        }
        public override decimal Taxa => 0.09m;
    }
}
