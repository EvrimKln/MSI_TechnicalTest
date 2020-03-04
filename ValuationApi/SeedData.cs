using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ValuationApi.Model;

namespace ValuationApi
{
    public class SeedData
    {
        public void Initialize(ApiContext context)
        {           
            context.Database.EnsureCreated();
            AddTestData(context);
        }

        private void AddTestData(ApiContext context)
        {
            #region AddVesselType  
            var date = DateTime.Today;

            var vesselType = new VesselType
            {                
                Type = "Dry Bulk",
                CreatedBy = 1,
                CreationDate = date,
                IsActive = true
            };
            context.VesselTypes.Add(vesselType);

            vesselType = new VesselType
            {                
                Type = "Oil Tanker",
                CreatedBy = 1,
                CreationDate = date,
                IsActive = true
            };
            context.VesselTypes.Add(vesselType);

            vesselType = new VesselType
            {                
                Type = "Containership",
                CreatedBy = 1,
                CreationDate = date,
                IsActive = true
            };
            context.VesselTypes.Add(vesselType);
            #endregion AddVesselType

            #region AddVessel
            var vessel1 = new Vessel
            {                
                ImoNumber = 1,
                VesselTypeId = 1,
                Size = 25000,
                YearOfBuild = 2009,
                Name = "Vessel 1",
                IsActive = true,
                CreationDate = DateTime.Today,
                CreatedBy = 1
            };
            context.Vessels.Add(vessel1);

            vessel1 = new Vessel
            {                
                ImoNumber = 2,
                VesselTypeId = 1,
                Size = 25800,
                YearOfBuild = 2010,
                Name = "Vessel 2",
                IsActive = true,
                CreationDate = DateTime.Today,
                CreatedBy = 1
            };
            context.Vessels.Add(vessel1);

            vessel1 = new Vessel
            {                
                ImoNumber = 3,
                VesselTypeId = 2,
                Size = 50000,
                YearOfBuild = 2012,
                Name = "Vessel 3",
                IsActive = true,
                CreationDate = DateTime.Today,
                CreatedBy = 1
            };
            context.Vessels.Add(vessel1);

            vessel1 = new Vessel
            {                
                ImoNumber =4,
                VesselTypeId = 3,
                Size = 26000,
                YearOfBuild = 2011,
                Name = "Vessel 4",
                IsActive = true,
                CreationDate = DateTime.Today,
                CreatedBy = 1
            };
            context.Vessels.Add(vessel1);

            #endregion AddVessel
            context.SaveChanges();

            #region AddTimeSeries
            //Years we want
            int[] years = new int[] { 2020, 2021, 2022 };

            foreach (var vesselTypeItem in context.VesselTypes)
            {
                for (int i = 0; i < years.Length; i++)
                {
                    var timeSeries = new TimeSeries
                    {
                        VesselTypeId = vesselTypeItem.Id,
                        Year = years[i],                                              
                        CreatedBy = 1,
                        CreationDate = date,
                        IsActive = true
                    };
                    context.TimeSeries.Add(timeSeries);
                }
            }
            #endregion AddTimeSeries

            context.SaveChanges();
        }
    }

}
