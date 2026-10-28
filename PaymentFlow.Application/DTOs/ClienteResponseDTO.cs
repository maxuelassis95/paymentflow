namespace PaymentFlow.Application.DTOs
{
    public class ClienteResponseDTO
    {
        public string Nome { get; set; }
        public String Email { get; set; }
        public DateTime DataCriacao { get; set; }

        public ClienteResponseDTO()
        {

        }

        // permite implementar o ClienteResponseDTO passado ou não os valores no construtor
        public ClienteResponseDTO(string nome, string email, DateTime dataCriacao)
        {
            Nome = nome;
            Email = email;
            DataCriacao = dataCriacao;
        }
    }
}