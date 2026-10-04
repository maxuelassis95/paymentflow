namespace PaymentFlow.Domain.Entities
{
    public class EntidadeBase
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }

        public EntidadeBase(int id, DateTime dataCriacao)
        {
            Id = id;
            DataCriacao = dataCriacao;
        }
    }