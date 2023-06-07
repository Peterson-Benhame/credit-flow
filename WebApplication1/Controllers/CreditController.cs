using CreditFlow.Model;
using CreditFlow.Services;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace CreditApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditController : ControllerBase
    {
        private readonly ICreditoService _creditoService;

        public CreditController(ICreditoService creditoService)
        {
            _creditoService = creditoService;
        }

        // POST api/credit
        [HttpPost]
        public ActionResult Post([FromBody] Credito credito)
        {
            try
            {
                var creditoFactory = new CreditoFactory();
                var creditoProcessado = creditoFactory.CreateCredito(credito.TipoCredito!, 
                                                                        credito.ValorCredito, 
                                                                        credito.QuantidadeParcelas, 
                                                                        credito.DataPrimeiroVencimento);

                ResultadoCredito resultado = _creditoService.ProcessarCredito(creditoProcessado);


                if (resultado.Status == "Aprovado")
                {
                    return Ok($"Crédito válido e processado com sucesso. Valor total com juros: {resultado.ValorTotalComJuros.ToString("C", new CultureInfo("pt-BR"))}. Valor dos juros: {resultado.ValorJuros.ToString("C", new CultureInfo("pt-BR"))}.");

                }
                else
                {
                    return BadRequest(resultado);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
}