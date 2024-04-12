using BankLoan.Models.Contracts;
using BankLoan.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Repositories
{
    public class LoanRepository : IRepository<ILoan>
    {
        private List<ILoan> _models;
        public LoanRepository()
        {
            _models = new List<ILoan>();
        }
        public IReadOnlyCollection<ILoan> Models => _models.AsReadOnly();

        public void AddModel(ILoan model)
        {
            _models.Add(model);
        }

        public ILoan FirstModel(string name)
        {
            ILoan loan = _models.FirstOrDefault(l => l.GetType().Name == name);
            if (loan == null)
            {
                return null;
            }
            else
            {
                return loan;
            }
        }

        public bool RemoveModel(ILoan model)
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
