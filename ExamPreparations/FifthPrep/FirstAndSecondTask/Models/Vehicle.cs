using EDriveRent.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Models
{
    public abstract class Vehicle : IVehicle
    {
        private string _brand;
        private string _model;
        private string _licensePlateNumber;

        protected Vehicle(string brand, string model, double maxMileage, string licensePlateNumber)
        {
            Brand = brand;
            Model = model;
            MaxMileage = maxMileage;
            LicensePlateNumber = licensePlateNumber;
            BatteryLevel = 100;
            IsDamaged = false;
        }

        public string Brand
        {
            get => _brand;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Brand cannot be null or whitespace!");
                }
                _brand = value;
            }
        }

        public string Model
        {
            get => _model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Model cannot be null or whitespace!");
                }
                _model = value;
            }
        }

        public double MaxMileage { get; private set; }

        public string LicensePlateNumber
        {
            get => _licensePlateNumber;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("License plate number is required!");
                }
                _licensePlateNumber = value;
            }
        }

        public int BatteryLevel { get; private set; }

        public bool IsDamaged { get; private set; }
        public void ChangeStatus()
        {
            IsDamaged = !IsDamaged;
        }

        public virtual void Drive(double mileage)
        {
            double percentageToReduce = (mileage / MaxMileage) * 100;
            if (this.GetType().Name == "CargoVan")
            {
                percentageToReduce += 5;
            }
            int roundedPercentage = (int)Math.Round(percentageToReduce);
            BatteryLevel -= (BatteryLevel * roundedPercentage) / 100;
        }

        public void Recharge()
        {
            BatteryLevel = 100;
        }
        public override string ToString()
        {
            if (IsDamaged)
            {
                return $"{Brand} {Model} License plate: {LicensePlateNumber} Battery: {BatteryLevel}% Status: damaged";
            }
            else
            {
                return $"{Brand} {Model} License plate: {LicensePlateNumber} Battery: {BatteryLevel}% Status: OK";
            }
        }
    }
}
