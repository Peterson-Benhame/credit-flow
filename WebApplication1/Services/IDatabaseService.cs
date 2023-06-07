using CreditFlow.Model;

namespace CreditFlow.Services
{
    public interface IDatabaseService
    {
        List<Cliente> GetClientesSPComParcelasPagas();
        List<Cliente> GetClientesComParcelasSemAtraso();
    }

}
