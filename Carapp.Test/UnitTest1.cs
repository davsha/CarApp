using TollFeeCalculator;
using static CarApp.Enums;

namespace Carapp.Test
{
    //hade kunnat ta det längre med NSubstitute/fakeiteasy/Moq. kände inte att det behövdes här. 
    public class UnitTest1
    {
        private readonly TollCalculator _tollCalculator;
        private readonly TollFeeConfig _simplifiedConfig;

        public UnitTest1()
        {
            //Simple config
            _simplifiedConfig = new TollFeeConfig(

                 new List<DayOfWeek>()
                {
                    DayOfWeek.Saturday,
                    DayOfWeek.Sunday
                },
                new List<DateTime>()
                {
                    new DateTime(2013, 1, 1),
                    new DateTime(2013, 12, 24), 


                },
                Enum.GetValues(typeof(TollFreeVehicleType)).Cast<TollFreeVehicleType>()
                                                           .Select(v => v.ToString())
                                                           .ToList(),
                new List<TollInterval>()
                {
                    new TollInterval { StartHour = 6, StartMinute = 0, EndHour = 6, EndMinute = 29, Fee = 8 }, 
                    new TollInterval { StartHour = 6, StartMinute = 30, EndHour = 6, EndMinute = 59, Fee = 13 },
                },
                60,
                60);
             
            _tollCalculator = new TollCalculator(_simplifiedConfig);
        }

        [Fact]
        public void TollCalculator_IsTollFreeVehicle_ReturnTrue()
        {

            List<IVehicle> result = new()
            {
                //new Car(),
                new Motorbike(),
                new Tractor()
            };

            result.ForEach(vehicle => _tollCalculator.IsTollFreeVehicle(vehicle).Should().BeTrue());

        }

        [Fact]
        public void TollCalculator_IsNotTollFreeVehicle_ReturnFalse()
        {
            bool result = _tollCalculator.IsTollFreeVehicle(new Car());

            result.Should().BeFalse();

        }

        [Theory]
        [InlineData("2013-01-01")]
        public void TollCalculator_IsTollFreeDates_ReturnTrue(DateTime newYearDate)
        {

            bool result = _tollCalculator.IsTollFreeDate(newYearDate);

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("2013-01-02 06:35")]
        public void TollCalculator_IsNotTollFreeDate_ReturnFeeOverZero(DateTime date)
        {
            _simplifiedConfig.TollIntervals.ToList().Should().NotBeEmpty();
            var maxvalue = _simplifiedConfig.TollIntervals.ToList().Select(x => x.Fee).Max();
            var minvalue = _simplifiedConfig.TollIntervals.ToList().Select(x => x.Fee).Min();

            int result = _tollCalculator.GetTollFee(date);

            result.Should().BeInRange(minvalue, maxvalue);
            result.Should().BeGreaterThanOrEqualTo(1);
            


        }

        [Fact]
        public void TollCalculator_IsNotTollFreeMultipleDates_ReturnFeeOverZero()
        {

            DateTime[] datesArr = {
                new DateTime(2013, 1, 2, 6, 25, 0),
                new DateTime(2013, 1, 2, 6, 35, 0)
            };

            int result = _tollCalculator.GetTollFee(new Car(), datesArr);

            result.Should().BeLessThanOrEqualTo(_simplifiedConfig.MaxDailyFee);
            result.Should().BeGreaterThanOrEqualTo(1);

        }
    }
}