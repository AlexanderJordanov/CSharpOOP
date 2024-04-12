using EDriveRent.Core.Contracts;
using EDriveRent.Models;
using EDriveRent.Models.Contracts;
using EDriveRent.Repositories;
using EDriveRent.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Core
{
    public class Controller : IController
    {
        private IRepository<IUser> users;
        private IRepository<IVehicle> vehicles;
        private IRepository<IRoute> routes;
        public Controller()
        {
            users = new UserRepository();
            vehicles = new VehicleRepository();
            routes = new RouteRepository();
        }

        public string RegisterUser(string firstName, string lastName, string drivingLicenseNumber)
        {
            IUser user = users.FindById(drivingLicenseNumber);
            if (user != null) 
            {
                return $"{drivingLicenseNumber} is already registered in our platform.";
            }
            else
            {
                user = new User(firstName, lastName, drivingLicenseNumber);
                users.AddModel(user);
                return $"{firstName} {lastName} is registered successfully with DLN-{drivingLicenseNumber}";
            }
        }

        public string UploadVehicle(string vehicleType, string brand, string model, string licensePlateNumber)
        {
            if (vehicleType != "PassengerCar" &&  vehicleType != "CargoVan")
            {
                return $"{vehicleType} is not accessible in our platform.";
            }
            IVehicle vehicle = vehicles.FindById(licensePlateNumber);
            if (vehicle != null)
            {
                return $"{licensePlateNumber} belongs to another vehicle.";
            }
            else
            {
                if (vehicleType == "PassengerCar")
                {
                    vehicle = new PassengerCar(brand, model, licensePlateNumber);
                }
                else if (vehicleType == "CargoVan")
                {
                    vehicle = new CargoVan(brand, model, licensePlateNumber);
                }
                vehicles.AddModel(vehicle);
                return $"{brand} {model} is uploaded successfully with LPN-{licensePlateNumber}";
            }
        }

        public string AllowRoute(string startPoint, string endPoint, double length)
        {
            if (routes.GetAll().Any(r => r.StartPoint == startPoint && r.EndPoint == endPoint && r.Length == length))
            {
                return $"{startPoint}/{endPoint} - {length} km is already added in our platform.";
            }
            if (routes.GetAll().Any(r => r.StartPoint == startPoint && r.EndPoint == endPoint && r.Length < length))
            {
                return $"{startPoint}/{endPoint} shorter route is already added in our platform.";
            }
            IRoute route = new Route(startPoint, endPoint, length, routes.GetAll().Count + 1);
            routes.AddModel(route);
            IRoute longerRoute = routes.GetAll().FirstOrDefault(r => r.StartPoint == startPoint && r.EndPoint == endPoint && r.Length > length);
            if (longerRoute != null)
            {
                longerRoute.LockRoute();
            }
            return $"{startPoint}/{endPoint} - {length} km is unlocked in our platform.";
        }


        public string MakeTrip(string drivingLicenseNumber, string licensePlateNumber, string routeId, bool isAccidentHappened)
        {
            IUser user = users.FindById(drivingLicenseNumber);
            IVehicle vehicle = vehicles.FindById(licensePlateNumber);
            IRoute route = routes.FindById(routeId);
            if (user.IsBlocked == true)
            {
                return $"User {drivingLicenseNumber} is blocked in the platform! Trip is not allowed.";
            }
            if (vehicle.IsDamaged == true)
            {
                return $"Vehicle {licensePlateNumber} is damaged! Trip is not allowed.";
            }
            if (route.IsLocked == true)
            {
                return $"Route {routeId} is locked! Trip is not allowed.";
            }
            vehicle.Drive(route.Length);
            if (isAccidentHappened == true)
            {
                vehicle.ChangeStatus();
                user.DecreaseRating();
            }
            else
            {
                user.IncreaseRating();
            }
            if (vehicle.IsDamaged == true)
            {
                return $"{vehicle.Brand} {vehicle.Model} License plate: {vehicle.LicensePlateNumber} Battery: {vehicle.BatteryLevel}% Status: damaged";
            }
            else
            {
                return $"{vehicle.Brand} {vehicle.Model} License plate: {vehicle.LicensePlateNumber} Battery: {vehicle.BatteryLevel}% Status: OK";
            }
        }

        public string RepairVehicles(int count)
        {
            var damagedVehicles = vehicles.GetAll()
                .Where(v => v.IsDamaged == true)
                .OrderBy(v => v.Brand)
                .ThenBy(v => v.Model)
                .Take(count)
                .ToList();
            foreach(var vehicle in damagedVehicles)
            {
                vehicle.ChangeStatus();
                vehicle.Recharge();
            }
            return $"{damagedVehicles.Count} vehicles are successfully repaired!";
        }

        public string UsersReport()
        {
            var usersToReport = users.GetAll()
                .OrderByDescending(u => u.Rating)
                .ThenBy(u => u.LastName)
                .ThenBy(u => u.FirstName);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("*** E-Drive-Rent ***");
            foreach (var user in usersToReport)
            {
                sb.AppendLine(user.ToString());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
