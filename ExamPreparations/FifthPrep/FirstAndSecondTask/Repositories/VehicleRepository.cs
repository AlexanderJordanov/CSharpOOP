using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Repositories
{
    public class VehicleRepository : IRepository<IVehicle>
    {
        private List<IVehicle> _vehicles;
        public VehicleRepository()
        {
            _vehicles = new List<IVehicle>();
        }
        public void AddModel(IVehicle model)
        {
            _vehicles.Add(model);
        }

        public IVehicle FindById(string identifier)
        {
            IVehicle vehicle = _vehicles.FirstOrDefault(v => v.LicensePlateNumber == identifier);
            if (vehicle == null)
            {
                return null;
            }
            else
            {
                return vehicle;
            }
        }

        public IReadOnlyCollection<IVehicle> GetAll()
        {
            return _vehicles.AsReadOnly();
        }

        public bool RemoveById(string identifier)
        {
            IVehicle vehicle = _vehicles.FirstOrDefault(v => v.LicensePlateNumber == identifier);
            if (vehicle == null)
            {
                return false;
            }
            else
            {
                _vehicles.Remove(vehicle);
                return true;
            }
        }
    }
}
