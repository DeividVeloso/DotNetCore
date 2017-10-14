using Eventos.IO.Domain.Core.Models;
using Eventos.IO.Domain.Organizadores;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace Eventos.IO.Domain.Eventos
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

        //Usado para constrir minha classe Factory aqui dentro do Evento
        private Evento()
        {

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
        public bool Excluido { get; private set; }
        public ICollection<Tags> Tags { get; private set; }

        //Usado para Foreign Key com Evento
        public Guid? CategoriaId { get; private set; }
        public Guid? EnderecoId { get; private set; }
        public Guid OrganizadorId { get; private set; }

        //EF propriedades de navegação, são novas tabelas
        public virtual Categoria Categoria { get; private set; }
        public virtual Endereco Endereco { get; private set; }
        public virtual Organizador Organizador { get; private set; }

        //ADHOC Setter
        public void AtribuirEntereco(Endereco endereco)
        {
            if (!endereco.ehValid)
            {

            }
        }

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
            ValidarData();
            ValidarNomeEmpresa();
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

        private void ValidarData()
        {
            RuleFor(c => c.DataIncio)
                .GreaterThan(c => c.DataFim)
                .WithMessage("A data de início deve ser maior que a data do final do evento");

            RuleFor(c => c.DataIncio)
              .LessThan(DateTime.Now)
              .WithMessage("A data de início não deve ser menor que a data atual");
        }

        private void ValidarNomeEmpresa()
        {
            RuleFor(c => c.NomeEmpresa)
                .NotEmpty().WithMessage("O nome do organizador precisa ser preenchido")
                .Length(2, 150).WithMessage("O nome do organizador precisa ter entre 2 e 150 caracteres");
        }
        #endregion

        public class EventoFactory
        {
            public static Evento NovoEventoCompleto(
            Guid id,
            string nome,
            string descricaoCurta,
            string descricaoLonga,
            DateTime dataInicio,
            DateTime dataFim,
            bool gratuito,
            decimal valor,
            bool online,
            string nomeEmpresa,
            Guid? organizadorId)
            {
                var evento = new Evento()
                {
                    Id = id,
                    DescricaoCurta = descricaoCurta,
                    DescricaoLonga = descricaoLonga,
                    Nome = nome,
                    DataIncio = dataInicio,
                    DataFim = dataFim,
                    Gratuito = gratuito,
                    Valor = valor,
                    Online = online,
                    NomeEmpresa = nomeEmpresa
                };

                if (organizadorId != null)
                {
                    evento.Organizador = new Organizador(organizadorId.Value);
                }

                return evento;
            }
        }
    }
}
