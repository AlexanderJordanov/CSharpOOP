using BankLoan.Models.Contracts;
using BankLoan.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Repositories
{
    public class BankRepository : IRepository<IBank>
    {
        private List<IBank> _models;
        public BankRepository()
        {
            _models = new List<IBank>();
        }
        public IReadOnlyCollection<IBank> Models => _models.AsReadOnly();

        public void AddModel(IBank model)
        {
            _models.Add(model);
        }

        public IBank FirstModel(string name)
        {
            IBank bank = _models.FirstOrDefault(l => l.Name == name);
            if (bank == null)
            {
                return null;
            }
            else
            {
                return bank;
            }
        }

        public bool RemoveModel(IBank model)
        {
            if (_models.Contains(model))
            {
                _models.Remove(model);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
