using BankLoan.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Models
{
    public abstract class Bank : IBank
    {
        private string _name;
        private List<ILoan> _loans;
        private List<IClient> _clients;

        protected Bank(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            _loans = new List<ILoan>();
            _clients = new List<IClient>();
        }

        public string Name
        {
            get => _name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Bank name cannot be null or empty.");
                }
                _name = value;
            }
        }

        public int Capacity { get; private set; }

        public IReadOnlyCollection<ILoan> Loans => _loans.AsReadOnly();

        public IReadOnlyCollection<IClient> Clients => _clients.AsReadOnly();

        public void AddClient(IClient Client)
        {
            if (_clients.Count >= Capacity)
            {
                throw new ArgumentException("Not enough capacity for this client.");
            }
            else
            {
                _clients.Add(Client);
            }
        }

        public void AddLoan(ILoan loan)
        {
            _loans.Add(loan);
        }

        public string GetStatistics()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Name: {this.Name}, Type: {this.GetType().Name}");
            if (_clients.Count > 0)
            {
                sb.AppendLine($"Clients: {string.Join(", ", _clients.Select(c => c.Name))}");
            }
            else
            {
                sb.AppendLine($"Clients: none");
            }
            sb.AppendLine($"Loans: {_loans.Count}, Sum of Rates: {this.SumRates()}");
            return sb.ToString().TrimEnd();
        }

        public void RemoveClient(IClient Client)
        {
            _clients.Remove(Client);
        }

        public double SumRates() => _loans.Sum(l => l.InterestRate);
        
    }
}
