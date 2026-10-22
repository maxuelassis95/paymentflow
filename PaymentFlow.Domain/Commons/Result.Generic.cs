using PaymentFlow.Domain.Entities;

namespace PaymentFlow.Domain.Commons
{
    // Result Patern
    // Não usamos where : aqui, por <T> pode ser qualquer coisa
    public class Result<T> : Result
    {
        public T? Valor { get; private set; }
        public Result(bool sucesso, string? mensagemErro, T? valor) : base(sucesso, mensagemErro)
        {
            Valor = valor;
        }

        public static Result<T> Ok(T valor)
        {
            return new Result<T>(true, null, valor);
        }

        // Se usássemos null, o compilador reclamaria quando T fosse tipo de valor (int não aceita null).
        // default resolve os dois: vira null para referência, e 0/false/etc para valor.
        public static Result<T> Fail(string mensagemErro)
        {
            return new Result<T>(false, mensagemErro, default);
        }
    }
}