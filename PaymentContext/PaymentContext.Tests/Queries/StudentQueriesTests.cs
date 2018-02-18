using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using System;
using PaymentContext.Tests.Mocks;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Queries;
using System.Collections.Generic;
using System.Linq;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentQueriesTests
    {
        private IList<Student> _student;
        public StudentQueriesTests()
        {
            for (var i = 0; i < 10; i++)
            {
                var student = new Student(
                    new Name("Deivid", "Veloso"),
                    new Document("1111111111" + i.ToString(), EDocumentType.CPF),
                    new Email("veloso" + i.ToString() + "@gmail.com")
                    );
                _student.Add(student);
            }
        }

        [TestMethod]
        public void ShoulReturnNullWhenDocumentNotExists()
        {
            var exp = StudentQueries.GetStudentInfo("12345678911");
            var student = _student.AsQueryable().Where(exp);
            Assert.AreEqual(null, student);
        }

          [TestMethod]
        public void ShoulReturnNullWhenDocumentExists()
        {
            var exp = StudentQueries.GetStudentInfo("11111111111");
            var student = _student.AsQueryable().Where(exp);
            Assert.AreNotEqual(null, student);
        }
    }
}
