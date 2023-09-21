
namespace TollFeeCalculator
{
    public class Car : IVehicle
    {
        public string GetVehicleType()
        {
            return typeof(Car).Name;
        }
    }
}