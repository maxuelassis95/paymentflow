namespace PaymentFlow.Domain.Entities
{
    public class Cliente : EntidadeBase
    {
        public string Nome { get; set; }
        public string Email { get; set; }

        public Cliente(int id, DateTime dataCriacao, string nome, string email) : base (id, dataCriacao)
        {
            Nome = nome;
            Email = email;
        }

    }
}