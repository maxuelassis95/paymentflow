using System.ComponentModel.DataAnnotations.Schema;
using PaymentFlow.Domain.Enums;

namespace PaymentFlow.Domain.Entities
{
    public class Pagamento : EntidadeBase
    {
        public decimal Valor { get; set; }
        public StatusPagamento Status { get; set; }
        public int ClienteId { get; set; }
        [ForeignKey("ClienteId")]
        public virtual Cliente Cliente { get; set; }

        public Pagamento(int id, DateTime dataCriacao, decimal valor, StatusPagamento status) : base(id, dataCriacao)  
        {
            Valor = valor;
            Status = status;
        }
    }
}