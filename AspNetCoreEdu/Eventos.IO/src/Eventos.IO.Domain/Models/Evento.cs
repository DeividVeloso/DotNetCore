using Eventos.IO.Domain.Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eventos.IO.Domain.Models
{
    public class Evento : Entity<Evento>
    {
        public Evento(
            string nome,
            DateTime dataInicio,
            DateTime dataFim,
            bool gratuito,
            decimal valor,
            bool online,
            string nomeEmpresa)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            DataIncio = dataInicio;
            DataFim = dataFim;
            Gratuito = gratuito;
            Valor = valor;
            Online = online;
            NomeEmpresa = nomeEmpresa;
        }

        public string Nome { get; private set; }
        public string DescricaoCurta { get; private set; }
        public string DescricaoLonga { get; private set; }
        public DateTime DataIncio { get; private set; }
        public DateTime DataFim { get; private set; }
        public bool Gratuito { get; private set; }
        public decimal Valor { get; private set; }
        public bool Online { get; private set; }
        public string NomeEmpresa { get; private set; }
        public Categoria Categoria { get; private set; }
        public ICollection<Tags> Tags { get; private set; }
        public Endereco Endereco { get; private set; }
        public Organizador Organizador { get; private set; }

        public override bool EhValido()
        {
            Validar();
            return ValidationResult.IsValid;
        }

        #region Validações
        private void Validar()
        {
            ValidaNome();
            ValidaValor();
            ValidaEndereco();
            //Chamo o método Validate para validar minha propria classe que está chamando this
            ValidationResult = Validate(this);
        }

        private void ValidaNome()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome precisa ser preenchido")
                .Length(2, 150).WithMessage("Nome deve ter no minimo 2 caracteres e no máximo 150");
        }

        private void ValidaValor()
        {
            RuleFor(c => c.Valor)
                .ExclusiveBetween(10, 3000).When(d => !d.Gratuito)
                .WithMessage("O valor do evento deve ser entre R$10 e R$3000");

            RuleFor(c => c.Valor)
                .ExclusiveBetween(0, 0).When(s => s.Gratuito)
                .WithMessage("O Valor deve ser 0 para um evento gratuito");
        }

        private void ValidaEndereco()
        {
            RuleFor(c => c.Endereco)
                .NotEmpty().When(x => !x.Online)
                .WithMessage("O evento precisa ter um endereço, pois é presencial");

            RuleFor(c => c.Endereco)
                .Empty().When(x => x.Online)
                .WithMessage("O endereço deve ser vazio para eventos online");
        }
        #endregion
    }
}
