using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Domain.Enums;
using System;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentTests
    {
        private readonly Student _student;
        private readonly Subscription _subscription;
        private readonly Name _name;
        private readonly Document _document;
        private readonly Email _email;
        public StudentTests()
        {
            _name = new Name("Bruce", "Wayne");
            _document = new Document("12345678909", EDocumentType.CPF);
            _email = new Email("batman@dc.com");
            _student = new Student(_name, _document, _email);
            _subscription = new Subscription(null);
        }

        [TestMethod]
        public void ShoulReturnErrorWhenHadActiveSubscription()
        {
            var _payment = new PayPalPayment("123456", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Corp", _document, null, _email);
            _subscription.AddPayment(_payment);
            _student.AddSubscriptions(_subscription);
            _student.AddSubscriptions(_subscription);

            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShoulReturnErrorWhenSubscriptionHasNoPayment()
        {
            _student.AddSubscriptions(_subscription);

            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShoulReturnSuccessWhenAddSubscription()
        {
            var _payment = new PayPalPayment("123456", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Corp", _document, null, _email);
            _subscription.AddPayment(_payment);
            _student.AddSubscriptions(_subscription);

            Assert.IsTrue(_student.Valid);
        }
    }
}
