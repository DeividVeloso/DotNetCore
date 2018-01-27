using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        private IList<Subscription> _subscription;
        public Student(Name name, Document document, Email email, Address address) 
        {
            Name = name;
            Document = document;
            Email = email;
            Address = address;
            _subscription = new List<Subscription>();
        }

        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }
        public IReadOnlyCollection<Subscription> Subscriptions { get { return _subscription.ToArray(); } }

        public void AddSubscriptions(Subscription subscription)
        {
            // Se já tiver uma assinatura ativa, cencela.

            //Cancela todas as outras assinaturas, e coloca esta como principal
            foreach (var sub in Subscriptions)
            {
                sub.Inactivate();
            }

            _subscription.Add(subscription);
        }
    }
}