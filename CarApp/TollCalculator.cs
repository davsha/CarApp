using CarApp;
using TollFeeCalculator;

public class TollCalculator
{

    private readonly TollFeeConfig _config;

    public TollCalculator(TollFeeConfig config)
    {
        _config = config;
    }


    public int GetTollFee(IVehicle vehicle, DateTime[] dates)
    {
        if (IsTollFreeVehicle(vehicle))
            return 0;

        var orderedDates = dates.OrderBy(x => x.TimeOfDay).ToList();
        DateTime previousDate = orderedDates[0];
        int totalFee = 0;

        foreach (DateTime date in orderedDates)
        {
            int nextFee = GetTollFee(date);
            int tempFee = GetTollFee(previousDate);
            

            TimeSpan timeDifference = date - previousDate;
            double minutes = timeDifference.TotalMinutes;

            if (minutes <= _config.MaxTimeIntervalMinutes)
            {
                if (totalFee > 0)
                    totalFee -= tempFee;

                if (nextFee >= tempFee)
                    tempFee = nextFee;

                totalFee += tempFee;
            }
            else
            {
                totalFee += nextFee;
            }

            previousDate = date;
        }

        if (totalFee > _config.MaxDailyFee)
            totalFee = _config.MaxDailyFee;
        
        return totalFee;
    }
   
    public int GetTollFee(DateTime date)
    {
        if (IsTollFreeDate(date))
            return 0;

        int hour = date.Hour;
        int minute = date.Minute;

        foreach (var interval in _config.TollIntervals)
        {
            if (hour >= interval.StartHour && hour <= interval.EndHour &&
                minute >= interval.StartMinute && minute <= interval.EndMinute)
            {
                return interval.Fee;
            }
        }

        return 0;

    }

    public bool IsTollFreeVehicle(IVehicle vehicle)
    {
        return (vehicle == null) ? false : _config.TollFreeVehicleTypes.Contains(vehicle.GetVehicleType());
    }

    public bool IsTollFreeDate(DateTime date)
    {
       return (_config.TollFreeDates.Contains(date.Date) || _config.TollFreeDaysOfWeek.Contains(date.DayOfWeek)) ? true : false;
            

    }

}