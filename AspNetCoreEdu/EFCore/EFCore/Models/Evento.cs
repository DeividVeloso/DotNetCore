using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.Models
{
    public class Evento
    {
        [Key]
        public int Id { get; set; }

        public DateTime Data { get; set; }

        public string Descricao { get; set; }

        [Required(ErrorMessage ="O nome  é obrigatório")]
        [Display(Name = "Nome Completo")]
        [MaxLength(100)]
        [MinLength(2)]
        public string Nome { get; set; }

        [Range(10,1000)]
        [Required(ErrorMessage="Preencha o Valor")]
        public decimal Valor { get; set; }
         
        public bool Gratuito { get; set; }

    }
}
