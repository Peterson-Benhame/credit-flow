using CreditFlow.Model;

namespace CreditFlow.Services
{
    public class CreditoService : ICreditoService
    {
        public ResultadoCredito ProcessarCredito(Credito credito)
        {
            var resultado = new ResultadoCredito();

            if (credito.Validar())
            {
                // Calcular o valor total com juros
                resultado.ValorTotalComJuros = credito.CalcularValorTotalComJuros();

                // Calcular o valor dos juros
                resultado.ValorJuros = credito.CalcularValorJuros();

                // O crédito é aprovado
                resultado.Status = "Aprovado";
            }
            else
            {
                // O crédito é recusado
                resultado.Status = "Recusado";
            }

            return resultado;
        }
    }
}
