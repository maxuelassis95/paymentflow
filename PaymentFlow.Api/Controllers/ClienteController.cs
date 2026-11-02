using Microsoft.AspNetCore.Mvc;
using PaymentFlow.Application.Services;
using PaymentFlow.Application.DTOs;
using PaymentFlow.Domain.Commons;
using PaymentFlow.Application.Interfaces;
using PaymentFlow.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using PaymentFlow.Domain.Enums;

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

        public ClienteController(IClienteService clienteService, ILogger<ClienteController> logger)
        {
            _clienteService = clienteService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<ClienteResponseDTO>> PostAsync([FromBody] ClienteRequestDTO request, CancellationToken ct = default)
        {
            if (request is null)
            {
                return BadRequest("Dados incompletos");
            }

            if (String.IsNullOrEmpty(request.Nome))
            {
                return BadRequest("Nome precisa estar preenchido");
            }

            if (String.IsNullOrEmpty(request.Email))
            {
                return BadRequest("E-mail precisa esta preenchido");
            }

            try
            {
                var resposta = await _clienteService.CreateAsync(request, ct);

                if (!resposta.Sucesso)
                {
                    return RetornaTipoErro(resposta.TipoErro ?? TiposErro.Problem, resposta.Erro);
                }

                return StatusCode(201, resposta.Valor);
                
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao criar cliente");
                return Problem("Ocorreu um erro interno no servidor");
            }
        }

        [HttpGet("busca-id/{id}")]
        public async Task<ActionResult<ClienteResponseDTO>> GetByIdAsync([FromRoute] int id, CancellationToken ct)
        {
            try
            {
                var cliente = await _clienteService.GetByIdAsync(id, ct);

                if (cliente.Sucesso is not true)
                {
                    return RetornaTipoErro(cliente.TipoErro ?? TiposErro.Problem, cliente.Erro);
                }

                return Ok(cliente.Valor);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar cliente pelo Id ");
                return Problem("Ocorreu um erro no servidor");
            }
        }

        [HttpGet("busca-email/{email}")]
        public async Task<ActionResult<ClienteResponseDTO>> GetEmailAsync([FromRoute]string email, CancellationToken ct)
        {
            try
            {
                var cliente = await _clienteService.GetByEmailAsync(email, ct);
                
                if(cliente.Sucesso is not true)
                {
                    return RetornaTipoErro(cliente.TipoErro ??TiposErro.Problem, cliente.Erro);
                }

                return Ok(cliente.Valor);
                
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Erro ao buscar cliente por email");
                return Problem("Ocorreu um erro no servidor");
            }

        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ClienteResponseDTO>>> GetAllAsync(CancellationToken ct)
        {
            try
            {
                var clientes = await _clienteService.GetAllAsync(ct);
                return Ok(clientes.Valor);
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Erro ao buscar todos os clientes");
                return Problem("Erro interno do servidor");
            }
        }

        private ActionResult<ClienteResponseDTO> RetornaTipoErro(TiposErro tipoErro, string mensagem)
        {
            switch(tipoErro)
            {
                case TiposErro.BadRequest:
                    return BadRequest(mensagem);

                case TiposErro.NotFound:
                    return NotFound(mensagem);

                case TiposErro.Conflict:
                    return Conflict(mensagem);

                default:
                    return Problem(mensagem);
            }

        }

    }
}
