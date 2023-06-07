using CreditFlow.Model;
using CreditFlow.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreditFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;

        public ClienteController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpGet("sp/parcelas-pagas")]
        public ActionResult<List<Cliente>> GetClientesSPComParcelasPagas()
        {
            return _databaseService.GetClientesSPComParcelasPagas();
        }

        [HttpGet("parcelas-sem-atraso")]
        public ActionResult<List<Cliente>> GetClientesComParcelasSemAtraso()
        {
            return _databaseService.GetClientesComParcelasSemAtraso();
        }
    }

}
