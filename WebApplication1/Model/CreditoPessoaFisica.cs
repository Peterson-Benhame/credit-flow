namespace CreditFlow.Model
{
    public class CreditoPessoaFisica : Credito
    {
        public CreditoPessoaFisica()
        {
            TipoCredito = "CreditoPessoaFisica";
        }
        public override decimal Taxa => 0.03m;
    }
}
