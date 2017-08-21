using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Domain.Core.Models
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            //Conhecido como Magic number, é usado para gerar um HashCode único para classe.
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        //Usado para quando eu der um ToString identificar qual classe e Id estou chamando.
        public override string ToString()
        {
            return GetType().Name + "[Id = ]" + Id + "]";
        }
    }
}
