using PaymentFlow.Domain.Interfaces;
using PaymentFlow.Application.DTOs;
using PaymentFlow.Application.Interfaces;
using PaymentFlow.Domain.Entities;
using PaymentFlow.Domain.Commons;

namespace PaymentFlow.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        // ============================================================
        // ESTUDO FUTURO: Middleware Global de Exceções
        // ============================================================
        // Objetivo: capturar exceções NÃO tratadas (ex: DbUpdateException)
        // em um único ponto, evitando try/catch repetido em Controllers/Services.
        //
        // Como funciona:
        //   - app.UseExceptionHandler() fica no início do pipeline e "envolve"
        //     todo o processamento subsequente em um try/catch.
        //   - Se uma exceção "borbulhar" (não for capturada antes), ele a captura
        //     e delega para um handler registrado.
        //
        // Implementação recomendada (.NET 8+):
        //   1. Criar classe que implementa IExceptionHandler
        //      -> método TryHandleAsync(HttpContext, Exception, CancellationToken)
        //      -> mapear tipo de exceção para StatusCode (switch)
        //      -> logar o erro (ILogger)
        //      -> escrever resposta padronizada (ProblemDetails) em JSON
        //      -> retornar true (tratado) para parar a propagação
        //
        //   2. No Program.cs:
        //      builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        //      builder.Services.AddProblemDetails();
        //      app.UseExceptionHandler();  // colocar cedo no pipeline
        //
        // Regra de ouro:
        //   - Falhas ESPERADAS (regra de negócio) -> Result.Fail(...)
        //   - Falhas INESPERADAS (infra) -> deixar subir -> middleware captura
        //
        // Segurança:
        //   - Em produção, NÃO expor exception.Message / stack trace no Detail.
        //   - Retornar mensagem genérica + logar detalhes internamente.
        //
        // Quando estudar: depois de dominar o fluxo Result na Service/Controller.
        // ============================================================

        public async Task<Result> CreateAsync(ClienteRequestDTO request, CancellationToken ct = default)
        {
            if(request is null)
            {
                return Result.Fail("Cliente não existe");
            }

            // Verifica se possui cliente com o email informado, para chama FindAsync() e passa o lambda que ele espera Expression<Func<Cliente, bool>>
            var cliente = await _clienteRepository.FindAsync(c => c.Email == request.Email, ct);

            // Lembrar: cliente aqui retorna uma collection public async Task<IReadOnlyList<Cliente>>, então a vrifica deve ser consultando se a lista possui itens, como fiz.
            // Quando tentamos if(cliente is not null) {} ele retornou erro, porque lista vazia não é objeto nulo
            if(cliente.Count > 0)
            {
                return Result.Fail("Cliente já cadastrado em nosso sistema");
            }

            Cliente clienteNovo = new Cliente(request.Nome, request.Email);

            if (await _clienteRepository.AddAsync(clienteNovo, ct))
            {
                return Result.Ok();
            }

            return Result.Fail("Erro ao adicionar cliente");
            
        }

        public async Task<Result<IReadOnlyList<Cliente>>> GetAllAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<ClienteResponseDTO>> GetByIdAsync(int id, CancellationToken ct)
        {
            if (!(id > 0))
            {
                return Result<ClienteResponseDTO>.Fail("Id incorreto, digite um id válido");
            }

            var cliente = await _clienteRepository.GetByIdAsync(id, ct);

            if (cliente is null)
            {
                return Result<ClienteResponseDTO>.Fail("Nenhum cliente foi encontrado com esse id");
            }

            var clienteDTO = new ClienteResponseDTO()
            {
                Nome = cliente.Nome,
                Email = cliente.Email,
                DataCriacao = cliente.DataCriacao,
            };

            return Result<ClienteResponseDTO>.Ok(clienteDTO);

        }

        public async Task<Result<ClienteResponseDTO>> GetByEmailAsync(string email, CancellationToken ct)
        {
            if(String.IsNullOrWhiteSpace(email))
            {
                return Result<ClienteResponseDTO>.Fail("Email incorreto");
            }

            var cliente = await _clienteRepository.GetByEmailAsync(email, ct);

            if(cliente is null)
            {
                return Result<ClienteResponseDTO>.Fail("Não exite cliente em nosso sistema com esse email");
            }

            var clienteDTO = new ClienteResponseDTO()
            {
                Nome = cliente.Nome,
                Email = cliente.Email,
                DataCriacao = cliente.DataCriacao
            };
            

            return Result<ClienteResponseDTO>.Ok(clienteDTO);
        }


    }
}