using PaymentFlow.Domain.Enums;

namespace PaymentFlow.Domain.Commons
{
    // Result Patern
    public class Result
    {
        public String? Erro { get; private set; }
        public bool Sucesso { get; private set; }
        public TiposErro? TipoErro { get; private set; }

        public Result(bool sucesso, String erro, TiposErro? tipoErro)
        {
            Erro = erro;
            Sucesso = sucesso;
            TipoErro = tipoErro;
        }

        public static Result Ok()
        {
            return new Result(true, null, null);
        }

        public static Result Fail(string mensagemErro, TiposErro tipoErro)
        {
            return new Result(false, mensagemErro, tipoErro);
        }
    }
}