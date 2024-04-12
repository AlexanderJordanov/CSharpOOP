using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Repositories
{
    public class UserRepository : IRepository<IUser>
    {
        private List<IUser> _users;
        public UserRepository() 
        { 
            _users = new List<IUser>();
        }

        public void AddModel(IUser model)
        {
            _users.Add(model);
        }

        public IUser FindById(string identifier)
        {
            IUser user = _users.FirstOrDefault(x => x.DrivingLicenseNumber == identifier);
            if (user == null)
            {
                return null;
            }
            else
            {
                return user;
            }
        }

        public IReadOnlyCollection<IUser> GetAll()
        {
            return _users.AsReadOnly();
        }

        public bool RemoveById(string identifier)
        {
            IUser user = _users.FirstOrDefault(x => x.DrivingLicenseNumber == identifier);
            if (user != null)
            {
                _users.Remove(user);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
