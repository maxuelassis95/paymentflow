namespace PaymentFlow.Domain.Entities
{
    public class Cliente : EntidadeBase
    {
        public string Nome { get; set; }
        public string Email { get; set; } 
        

        // Navigation Property: Propriedade que permite navegar entre os Pagamentos do Cliente        
        public ICollection<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();


        // Para o EF Core conseguir materializar
        protected Cliente()
        {

        }

        // Para os DTOs
        public Cliente(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }
        public Cliente(int id, DateTime dataCriacao, string nome, string email) : base (id, dataCriacao)
        {
            Nome = nome;
            Email = email;
        }


    }
}