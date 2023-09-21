
namespace CarApp
{
    public class TollFeeConfig
    {
        public List<DayOfWeek> TollFreeDaysOfWeek { get; set; }
        public List<DateTime> TollFreeDates { get; set; }
        public List<string> TollFreeVehicleTypes { get; set; }
        public List<TollInterval> TollIntervals { get; set; }
        public int MaxTimeIntervalMinutes { get; set; }
        public int MaxDailyFee { get; set; }

        public TollFeeConfig(List<DayOfWeek> dayOfWeeks, List<DateTime> tollFreeDates, List<string> tollFreeVehicleTypes, 
            List<TollInterval> tollIntervals, int maxTimeIntervalMinutes, int maxDailyFee)
        {
            TollFreeDaysOfWeek = dayOfWeeks;
            TollFreeDates = tollFreeDates;
            TollFreeVehicleTypes = tollFreeVehicleTypes;
            TollIntervals = tollIntervals;
            MaxTimeIntervalMinutes = maxTimeIntervalMinutes;
            MaxDailyFee = maxDailyFee;

        }

    }

   
}
