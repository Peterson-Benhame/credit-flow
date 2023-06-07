namespace CreditFlow.Model
{
    public class CreditoConsignado : Credito
    {
        public CreditoConsignado(decimal valorCredito, int quantidadeParcelas, DateTime dataPrimeiroVencimento)
        {
            TipoCredito = "CreditoConsignado";
            ValorCredito = valorCredito;
            QuantidadeParcelas = quantidadeParcelas;
            DataPrimeiroVencimento = dataPrimeiroVencimento;
        }
        public override decimal Taxa => 0.01m;
    }
}
