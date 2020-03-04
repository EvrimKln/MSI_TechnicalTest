using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ValuationApi;
using ValuationApi.Controllers;
using ValuationApi.Model;
using ValuationApi.Services.ValuationServices;
using Xunit;

namespace ValuationTest
{
    public class ValuationTests
    {
        public readonly ICalculator _calculator;
        public readonly ApiContext _context;

        public ValuationTests()
        {
            var services = new ServiceCollection();
            services.AddTransient<ICalculator, DefaultCalculator>();
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("Msi"));
            services.AddControllers();
            services.AddScoped<ApiContext>();
            services.AddScoped<SeedData>();

            var serviceProvider = services.BuildServiceProvider();

            _calculator = serviceProvider.GetService<ICalculator>();

            _context = serviceProvider.GetRequiredService<ApiContext>();

            var seed = serviceProvider.GetRequiredService<SeedData>();

            try
            {
                seed.Initialize(_context);
            }
            catch (Exception ex)
            {
                var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred seeding the DB.");
            }
        }

        [Fact]
        public void CalculateValuation_Test()
        {
            DryBulkValuation dryBulkValuation = new DryBulkValuation();
            
            Vessel vessel = _context.Vessels.Find(1);
            Assert.Equal(1, vessel.Id);
            List<TimeSeries> timeSeries = new List<TimeSeries>();
            timeSeries = _context.TimeSeries.Where(t => t.IsActive && t.VesselTypeId == vessel.VesselTypeId).ToList();
            List<Valuation> list = _calculator.CalculateValuation(vessel, timeSeries);
            
            Assert.Equal(timeSeries.Count(), list.Count());

            var val = list.Where(v => v.Year == 2020).FirstOrDefault();
            Assert.Equal((decimal)28.022, val.FairMarketValue);

            Assert.Equal(11, val.Age);
        }

        [Fact]
        public void ValuationController_Calculate_Test()
        {
            var mockRepo = new Mock<ApiContext>();
            var mockVal = new Mock<ICalculator>();

            var controller = new ValuationController(_context, _calculator);

            List<int> vesselImoNumbers = new List<int> { 1, 2 };
            var result = controller.CalculateValuations(vesselImoNumbers);
                        
            Assert.Equal(result.Count(), vesselImoNumbers.Count*3);
        }
    }
}
