namespace CreditFlow.Model
{
    public class CreditoImobiliario : Credito
    {
        public CreditoImobiliario()
        {
            TipoCredito = "CreditoImobiliario";
        }
        public override decimal Taxa => 0.09m;
    }
}
