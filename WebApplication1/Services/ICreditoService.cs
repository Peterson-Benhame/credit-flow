using CreditFlow.Model;

namespace CreditFlow.Services
{
    public interface ICreditoService
    {
        ResultadoCredito ProcessarCredito(Credito credito);
    }
}
