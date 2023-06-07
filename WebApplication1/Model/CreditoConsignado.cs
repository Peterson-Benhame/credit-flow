namespace CreditFlow.Model
{
    public class CreditoConsignado : Credito
    {
        public CreditoConsignado()
        {
            TipoCredito = "CreditoConsignado";
        }
        public override decimal Taxa => 0.01m;
    }
}
