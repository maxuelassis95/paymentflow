using Microsoft.AspNetCore.Mvc;
using PaymentFlow.Application.Services;
using PaymentFlow.Application.DTOs;
using PaymentFlow.Domain.Commons;
using PaymentFlow.Application.Interfaces;
using PaymentFlow.Domain.Entities;

namespace PaymentFlow.Api.Controllers
{

    // herdando Controller herdando ControllerBase pois é api
    // A própria documentação afirma que para apis deve se herdar da ControllerBase pois já tem todo métodos necessários
    // Deve herdar de Controller apenas quando for precisar de suporte a Views.

    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        
        // Sempre é bom loggar os erros e tomar cuidado com o try/catch para não engolir o erro customizado
        private readonly ILogger<ClienteController> _logger;

        // Após testes criar as interfaces das Services e ajustar o DI para consegir usar a interface
        public ClienteController(IClienteService clienteService, ILogger<ClienteController> logger)
        {
            _clienteService = clienteService;
            _logger = logger;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClienteRequestDTO request, CancellationToken ct = default)
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
                var resposta = await _clienteService.CreateAsync(request, ct);
                
                if(!resposta.Sucesso)
                {
                    return BadRequest($"Ocorreu um erro: {resposta.Erro}");
                }

                return Ok("Cliente cadastrado com sucesso");
                
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Erro ao criar cliente");
                return Problem("Ocorreu um erro interno no servidor");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetEmailAsync([FromQuery] string email, CancellationToken ct)
        {
            try
            {
                var cliente = await _clienteService.GetByEmailAsync(email, ct);
                
                if(cliente.Sucesso is not true)
                {
                    return BadRequest("Ocorreu um erro na busca, confere o email e tenta novamente mais tarde");
                }

                return Ok(cliente.Valor);
                
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Erro ao buscar cliente por email");
                return BadRequest("Ocorreu um erro no servidor");
            }

        }

        

    }
}
