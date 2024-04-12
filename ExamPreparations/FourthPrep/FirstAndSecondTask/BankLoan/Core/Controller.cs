using BankLoan.Core.Contracts;
using BankLoan.Models;
using BankLoan.Models.Contracts;
using BankLoan.Repositories;
using BankLoan.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Core
{
    public class Controller : IController
    {
        private IRepository<ILoan> loans;
        private IRepository<IBank> banks;
        public Controller()
        {
            loans = new LoanRepository();
            banks = new BankRepository();
        }
        public string AddBank(string bankTypeName, string name)
        {
            if (bankTypeName != "BranchBank" &&  bankTypeName != "CentralBank")
            {
                throw new ArgumentException("Invalid bank type.");
            }
            IBank bank = null;
            if (bankTypeName == "BranchBank")
            {
                bank = new BranchBank(name);
            }
            else if (bankTypeName == "CentralBank")
            {
                bank = new CentralBank(name);
            }
            banks.AddModel(bank);
            return $"{bankTypeName} is successfully added.";
        }

        public string AddLoan(string loanTypeName)
        {
            if (loanTypeName != "MortgageLoan" && loanTypeName != "StudentLoan")
            {
                throw new ArgumentException("Invalid loan type.");
            }
            ILoan loan = null;
            if (loanTypeName == "MortgageLoan")
            {
                loan = new MortgageLoan();
            }
            else if (loanTypeName == "StudentLoan")
            {
                loan = new StudentLoan();
            }
            loans.AddModel(loan);
            return $"{loanTypeName} is successfully added.";
        }




        public string ReturnLoan(string bankName, string loanTypeName)
        {
            if (loanTypeName != "MortgageLoan" && loanTypeName != "StudentLoan")
            {
                throw new ArgumentException($"Loan of type {loanTypeName} is missing.");
            }
            ILoan loan = null;
            if (loanTypeName == "MortgageLoan")
            {
                loan = new MortgageLoan();
            }
            else if (loanTypeName == "StudentLoan")
            {
                loan = new StudentLoan();
            }
            IBank bank = banks.Models.FirstOrDefault(b => b.Name == bankName);
            if (bank.GetType().Name == "CentralBank" && loan.GetType().Name == "StudentLoan")
            {
                throw new ArgumentException($"Loan of type {loanTypeName} is missing.");
            }
            bank.AddLoan(loan);
            loans.RemoveModel(loan);
            return $"{loanTypeName} successfully added to {bankName}.";
        }

        public string AddClient(string bankName, string clientTypeName, string clientName, string id, double income)
        {
            if (clientTypeName != "Student" && clientTypeName != "Adult")
            {
                throw new ArgumentException("Invalid client type.");
            }
            IBank bank = banks.Models.FirstOrDefault(b => b.Name == bankName);
            if ((bank.GetType().Name == "BranchBank" && clientTypeName != "Student") || (bank.GetType().Name == "CentralBank" && clientTypeName != "Adult"))
            {
                return "Unsuitable bank.";
            }
            IClient client = null;
            if (clientTypeName == "Student")
            {
                client = new Student(clientName, id, income);
            }
            else if (clientTypeName == "Adult")
            {
                client = new Adult(clientName, id, income);
            }
            bank.AddClient(client);
            return $"{clientTypeName} successfully added to {bankName}.";
        }

       
        public string FinalCalculation(string bankName)
        {
            IBank bank = banks.Models.FirstOrDefault(b => b.Name == bankName);
            double funds = 0;
            foreach(var client in bank.Clients)
            {
                funds += client.Income;
            }
            foreach(var loan in bank.Loans)
            {
                funds += loan.Amount;
            }
            return $"The funds of bank {bankName} are {funds:f2}.";
        }

        

        public string Statistics()
        {
            StringBuilder sb = new StringBuilder();
            foreach(var bank in banks.Models)
            {
                sb.AppendLine(bank.GetStatistics());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
