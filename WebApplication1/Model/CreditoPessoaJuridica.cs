namespace CreditFlow.Model
{
    public class CreditoPessoaJuridica : Credito
    {
        public CreditoPessoaJuridica()
        {
            TipoCredito = "CreditoPessoaJuridica";
        }
        public override decimal Taxa => 0.05m;
    }
}
