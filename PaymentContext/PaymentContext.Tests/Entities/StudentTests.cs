using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentTests
    {
        [TestMethod]
        public void AddSubscription()
        {
            //O que precisamos para ter um Aluno?
            var student = new Student("Deivid", "Veloso", "38749957805", "veloso.deivid@gmail.com");
            // student.Subscriptions.Add(student); Não vai conseguir adcionar assim
            student.AddSubscriptions(student);
        }
    }
}
