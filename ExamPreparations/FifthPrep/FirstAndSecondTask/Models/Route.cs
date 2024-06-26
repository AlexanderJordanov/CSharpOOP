﻿using EDriveRent.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Models
{
    public class Route : IRoute
    {
        private string _startPoint;
        private string _endPoint;
        private double _length;

        public Route(string startPoint, string endPoint, double length, int routeId)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Length = length;
            RouteId = routeId;
            IsLocked = false;
        }

        public string StartPoint
        {
            get { return _startPoint; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("StartPoint cannot be null or whitespace!");
                }
                _startPoint = value;
            }
        }

        public string EndPoint
        {
            get { return _endPoint; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Endpoint cannot be null or whitespace!");
                }
                _endPoint = value;
            }
        }

        public double Length
        {
            get { return _length; }
            private set
            {
                if (value < 1)
                {
                    throw new ArgumentException("Length cannot be less than 1 kilometer.");
                }
                _length = value;
            }
        }

        public int RouteId { get; private set; }

        public bool IsLocked { get; private set; }

        public void LockRoute()
        {
            IsLocked = true;
        }
    }
}
