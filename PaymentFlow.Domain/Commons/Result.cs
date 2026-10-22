namespace PaymentFlow.Domain.Commons
{
    // Result Patern
    public class Result
    {
        public String? Erro { get; private set; }
        public bool Sucesso { get; private set; }

        public Result(bool sucesso, String erro)
        {
            Erro = erro;
            Sucesso = sucesso;
        }

        public static Result Ok()
        {
            return new Result(true, null);
        }

        public static Result Fail(string mensagemErro)
        {
            return new Result(false, mensagemErro);
        }
    }
}