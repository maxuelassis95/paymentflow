using PaymentFlow.Domain.Interfaces;
using PaymentFlow.Application.DTOs;
using PaymentFlow.Application.Interfaces;
using PaymentFlow.Domain.Entities;
using PaymentFlow.Domain.Commons;
using System.Reflection.Metadata.Ecma335;

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

        public async Task<Result> CreateAsync(ClienteRequestDTO request, CancellationToken ct)
        {
            if(request is null)
            {
                return Result.Fail("Cliente não existe");
            }

            Cliente cliente = new Cliente(request.Nome, request.Email);

            if (await _clienteRepository.AddAsync(cliente, ct))
            {
                return Result.Ok();
            }

            return Result.Fail("Erro ao adicionar cliente");
            
        }

    }
}