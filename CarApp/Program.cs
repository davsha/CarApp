using CarApp;
using TollFeeCalculator;
using static CarApp.Enums;

//Init config
int maxTimeIntervalMinutes = 60;
int maxDailyFee = 60;
List<DayOfWeek> tollFreeDaysOfWeek = new()
    {
        DayOfWeek.Saturday, DayOfWeek.Sunday
    };
List<DateTime> tollFreeDates = new()
    {
        new DateTime(2013, 1, 1),
        new DateTime(2013, 3, 28),
        new DateTime(2013, 3, 29),
        new DateTime(2013, 4, 1),
        new DateTime(2013, 4, 30),
        new DateTime(2013, 5, 1),
        new DateTime(2013, 5, 8),
        new DateTime(2013, 5, 9),
        new DateTime(2013, 6, 5),
        new DateTime(2013, 6, 6),
        new DateTime(2013, 6, 21),
        new DateTime(2013, 7, 1),
        new DateTime(2013, 11, 1),
        new DateTime(2013, 12, 24),
        new DateTime(2013, 12, 25),
        new DateTime(2013, 12, 26),
        new DateTime(2013, 12, 31)

    };
List<string> tollFreeVehicleTypes = Enum.GetValues(typeof(TollFreeVehicleType))
                                        .Cast<TollFreeVehicleType>()
                                        .Select(v => v.ToString())
                                        .ToList();
List<TollInterval> tollIntervals = new()
{
        new TollInterval { StartHour = 6, StartMinute = 0, EndHour = 6, EndMinute = 29, Fee = 8 },
        new TollInterval { StartHour = 6, StartMinute = 30, EndHour = 6, EndMinute = 59, Fee = 13 },
        new TollInterval { StartHour = 7, StartMinute = 0, EndHour = 7, EndMinute = 59, Fee = 18 },
        new TollInterval { StartHour = 8, StartMinute = 0, EndHour = 8, EndMinute = 29, Fee = 13 },
        new TollInterval { StartHour = 8, StartMinute = 30, EndHour = 14, EndMinute = 59, Fee = 8 },
        new TollInterval { StartHour = 14, StartMinute = 0, EndHour = 14, EndMinute = 59, Fee = 8 },
        new TollInterval { StartHour = 15, StartMinute = 0, EndHour = 15, EndMinute = 29, Fee = 13 },
        new TollInterval { StartHour = 15, StartMinute = 30, EndHour = 16, EndMinute = 59, Fee = 18 },
        new TollInterval { StartHour = 17, StartMinute = 0, EndHour = 17, EndMinute = 59, Fee = 13 },
        new TollInterval { StartHour = 18, StartMinute = 0, EndHour = 18, EndMinute = 29, Fee = 8 },
        new TollInterval { StartHour = 18, StartMinute = 30, EndHour = 5, EndMinute = 59, Fee = 0 },
     };

TollFeeConfig config = new TollFeeConfig(tollFreeDaysOfWeek, tollFreeDates, tollFreeVehicleTypes, tollIntervals, maxTimeIntervalMinutes, maxDailyFee);
TollCalculator toll = new TollCalculator(config);

IVehicle c = new Car();
IVehicle m = new Motorbike();
IVehicle t = new Motorbike();

DateTime[] dates = {       new DateTime(2013, 8, 16, 05, 28, 0),//ej debit
                           new DateTime(2013, 8, 16, 06, 28, 0),//8 kr ska ej bet högre beloppet ska bet i nästa
                           new DateTime(2013, 8, 16, 06, 38, 0),//13 kr ska bet
                           new DateTime(2013, 8, 16, 07, 58, 0),//18 kr ska bet
                           new DateTime(2013, 8, 16, 14, 20, 0) //8 kr ska bet //Tot 39kr
                                                                };
Console.WriteLine(toll.GetTollFee(m, dates)); //Tullfri fordon
Console.WriteLine(toll.GetTollFee(t, dates)); //Tullfri fordon
Console.WriteLine(toll.GetTollFee(c, dates));

DateTime[] datesMax = {       new DateTime(2013, 8, 16, 05, 28, 0),//ej debit
                           new DateTime(2013, 8, 16, 06, 28, 0),//8 kr ska ej bet högre beloppet ska bet
                           new DateTime(2013, 8, 16, 06, 38, 0),//13 kr ska bet
                           new DateTime(2013, 8, 16, 07, 58, 0),//18 kr ska bet
                           new DateTime(2013, 8, 16, 14, 20, 0),//8 kr ska bet
                           new DateTime(2013, 8, 16, 14, 40, 0),//8 kr ska ej bet
                           new DateTime(2013, 8, 16, 15, 50, 0),// 18 ska bet
                           new DateTime(2013, 8, 16, 15, 52, 0),// 18 ska ej bet
                           new DateTime(2013, 8, 16, 16, 58, 0),// 18 ska bet
                           new DateTime(2013, 8, 16, 18, 20, 0) // 8 kr ska bet men slutligen bli 60kr max. 
                                                                };

Console.WriteLine(toll.GetTollFee(c, datesMax));


DateTime[] datesCheck = {  new DateTime(2013, 8, 16, 05, 20, 0),//ej debit 
                           new DateTime(2013, 8, 16, 06, 10, 0),//8kr
                           new DateTime(2013, 8, 16, 18, 20, 0),//8kr
                           new DateTime(2013, 8, 16, 18, 35, 0)//ej debit, ska inte skriva över 8kr, 10 min mellan. Tot 16kr
                        };

Console.WriteLine(toll.GetTollFee(c, datesCheck));



