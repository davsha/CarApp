﻿
namespace TollFeeCalculator
{
    public class Motorbike : IVehicle
    {
        public string GetVehicleType()
        {
            return typeof(Motorbike).Name;
        }
    }
}
