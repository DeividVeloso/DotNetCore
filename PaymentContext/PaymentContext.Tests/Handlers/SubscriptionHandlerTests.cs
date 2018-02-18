using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using System;
using PaymentContext.Tests.Mocks;
using PaymentContext.Domain.Commands;

namespace PaymentContext.Tests
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        [TestMethod]
        public void ShoulReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();
            command.FirstName = "Deivid";
            command.LastName = "Veloso";
            command.Document = "99999999999";
            command.Email = "raj@gmail.com";
            command.BarCode = "123456789";
            command.BoletoNumber = "123456789356";
            command.PaymentNumber = "12354";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 60;
            command.TotalPaid = 60;
            command.Payer = "Veloso Corp";
            command.PayerDocument = "12345678911";
            command.PayerDocumentType = EDocumentType.CPF;
            command.PayerEmail = "veloso.corp@gmail.com";
            command.Street = "Rua das Dores";
            command.Number = "1963";
            command.Neighborhood = "Mataram";
            command.City = "Rio de Janeiro";
            command.State = "RJ";
            command.Country = "RJ";
            command.ZipCode = "02854693";
            handler.Handle(command);
            Assert.AreEqual(false, handler.Valid);
        }
    }
}
