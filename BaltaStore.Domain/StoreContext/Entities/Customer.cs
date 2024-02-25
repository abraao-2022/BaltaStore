using System.Collections.Generic;
using System.Linq;
using BaltaStore.Domain.StoreContext.ValueObjects;
using BaltaStore.Shared.Entities;
using FluentValidator;

namespace BaltaStore.Domain.StoreContext.Entites
{
    public class Customer : Entity
    {
        private readonly IList<Adress> _adresses;
        public Customer(Name name,
                        Document document,
                        Email email,
                        string phone)
        {
            Name = name;
            Document = document;
            Email = email;
            Phone = phone;
            _adresses = new List<Adress>();

        }
        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public string Phone { get; private set; }
        public IReadOnlyCollection<Adress> Addresses => _adresses.ToArray();

        public void AddAddress(Adress address)
        {
            _adresses.Add(address);
        }

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}