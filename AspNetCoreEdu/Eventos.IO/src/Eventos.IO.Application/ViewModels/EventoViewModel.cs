using System;
using System.ComponentModel.DataAnnotations;

namespace Eventos.IO.Application.ViewModels
{
    public class EventoViewModel
    {
        public EventoViewModel()
        {
            Id = Guid.NewGuid();
            Endereco = new EnderecoViewModel();
            Categoria = new CategoriaViewModel();
        }

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O Nome é requerido")]//Requirido
        [MinLength(2, ErrorMessage = "O tamanho minimo do Nome é {1}")]//Tamanho Minimo
        [MaxLength(150, ErrorMessage = "O tamanho minimo do Nome é {1}")]//Tamanho Maximo
        [Display(Name = "Nome do Evento")]//Descrição melhor da propriedade
        public string Nome { get; set; }

        [Display(Name = "Descrição curta do Evento")]
        public string DescricaoCurta { get; set; }

        [Display(Name = "Descrição longa do Evento")]
        public string DescricaoLonga { get; set; }

        [Display(Name = "Início do Evento")]
        [DataType(DataType.Date)]//Informando que é do Tipo DateTime
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]//Padronizando conforme RFC do HTML
        public DateTime DataInicio { get; set; }

        [Display(Name = "Fim do Evento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataFim { get; set; }

        [Display(Name = "Será gratuito")]
        public bool Gratuito { get; set; }

        [Display(Name = "Valor")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Valor { get; set; }

        [Display(Name = "Será Online")]
        public bool Online { get; set; }

        [Display(Name = "Empresa / Grupo Organizador")]
        public string NomeEmpresa { get; set; }

        public EnderecoViewModel Endereco { get; set; }
        public CategoriaViewModel Categoria { get; set; }

        public Guid CategoriaId { get; set; }
    }
}
