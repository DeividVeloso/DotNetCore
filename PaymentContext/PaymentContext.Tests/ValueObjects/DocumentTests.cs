using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class DocumentTests
    {
        [TestMethod]
        public void ShoudReturnErrorWhenCNPJIsInvalid()
        {
            var doc = new Document("97323697000", EDocumentType.CNPJ);
            Assert.IsTrue(doc.Invalid);

        }
         [TestMethod]
        public void ShoudReturnSuccessWhenCNPJIsValid()
        {
            var doc = new Document("97323697000173", EDocumentType.CNPJ);
            Assert.IsTrue(doc.Valid);
            
        }
         [TestMethod]
        public void ShoudReturnErrorWhenCPFIsInvalid()
        {
             var doc = new Document("333489267", EDocumentType.CPF);
            Assert.IsTrue(doc.Invalid);
        }

         [TestMethod]
        public void ShoudReturnSuccessWhenCPFIsValid()
        {
             var doc = new Document("33348926722", EDocumentType.CPF);
            Assert.IsTrue(doc.Valid);
            
        }
    }
}
