namespace PaymentFlow.Domain.Entities
{
    public class EntidadeBase
    {
        // Id e DataCriacao são gerados pelo BANCO (auto-incremento e CURRENT_TIMESTAMP).
        // Por isso a aplicação NÃO precisa preenchê-los ao criar uma entidade nova.
        public int Id { get; protected set; }
        public DateTime DataCriacao { get;protected set; }

        // Construtor vazio: NÃO é para o seu código usar.
        // É para o EF Core conseguir criar o objeto quando LÊ do banco
        // (ex.: um ToList() ou FirstOrDefault()). Sem ele, o EF quebra em runtime.
        // "protected" só evita que alguém de fora faça "new EntidadeBase()".
        protected EntidadeBase()
        {
        }

        // Construtor usado quando você QUER passar Id e DataCriacao manualmente.
        // Serve para testes, seeds ou reconstrução de uma entidade já existente.
        // Não use ele no fluxo de "criar cliente novo via DTO".
        public EntidadeBase(int id, DateTime dataCriacao)
        {
            Id = id;
            DataCriacao = dataCriacao;
        }
    }
}