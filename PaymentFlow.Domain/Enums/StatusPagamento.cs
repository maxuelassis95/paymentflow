using System.ComponentModel.DataAnnotations;

namespace PaymentFlow.Domain.Enums
{
    public enum StatusPagamento
    {
        [Display(Name = "Pendente")]
        Pendente = 0,
        [Display(Name = ("Aprovado"))]
        Aprovado = 1,
        [Display(Name = ("Recusado"))]
        Recusado = 2,
        [Display(Name = ("Cancelado"))]
        Cancelado = 3

    }
}