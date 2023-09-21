using TollFeeCalculator;

namespace CarApp
{
    public class Tractor : IVehicle
    {
        public string GetVehicleType()
        {
            return typeof(Tractor).Name;
        }
    }
}
