using Microsoft.AspNetCore.Mvc;
using PaymentFlow.Application.Services;
using PaymentFlow.Application.DTOs;
using PaymentFlow.Domain.Commons;

namespace PaymentFlow.Api.Controllers
{

    // herdando Controller herdando ControllerBase pois é api
    // A própria documentação afirma que para apis deve se herdar da ControllerBase pois já tem todo métodos necessários
    // Deve herdar de Controller apenas quando for precisar de suporte a Views.

    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _clienteService;

        // Após testes criar as interfaces das Services e ajustar o DI para consegir usar a interface
        public ClienteController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClienteRequestDTO request, CancellationToken ct)
        {
            if(request is null)
            {
                return BadRequest("Dados incompletos");
            }

            if(String.IsNullOrEmpty(request.Nome))
            {
                return BadRequest("Nome precisa estar preenchido");
            }

            if(String.IsNullOrEmpty(request.Email))
            {
                return BadRequest("E-mail precisa esta preenchido");
            }

            try
            {
                var resposta = await _clienteService.AdicionarAsync(request, ct);
                
                if(!resposta.Sucesso)
                {
                    return BadRequest("Ocorreu um erro ao tentar adicionar um novo cliente");
                }

                return Ok("Cliente cadastrado com sucesso");
                
            }
            catch(Exception e)
            {
                return Problem("Ocorreu um erro interno no servidor");
            }

        }

    }
}
