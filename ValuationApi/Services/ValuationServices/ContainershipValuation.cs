using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ValuationApi.Model;

namespace ValuationApi.Services.ValuationServices
{
    public class ContainershipValuation : IValuation
    {
        //coefficients are different in each vessel type
        //maybe in the future formule will be different as well
        private const int CONTAINERSHIP_TYPE = 3;
        private const double F_CO_1 = 0.002;
        private const double F_CO_2 = 0.006;
        private const double F_CO_3 = 2;

        private const double S_CO_1 = 0.001;
        private const double S_CO_2 = 0.012;
        private const double S_CO_3 = 3;

        private const double O_CO_1 = 0.001;
        private const double O_CO_2 = 0.002;
        private const double O_CO_3 = 3;
        public bool IsMatch(Vessel vessel)
        {
            return vessel.VesselTypeId.Equals(CONTAINERSHIP_TYPE);
        }

        public List<Valuation> Valuate(Vessel vessel, IEnumerable<TimeSeries> timeSeries)
        {
            try
            {
                Valuation valuation = new Valuation();
                List<Valuation> valuationAll = new List<Valuation>();

                foreach (var item in timeSeries)
                {
                    valuation = new Valuation();
                    valuation.ImoNumber = vessel.ImoNumber;
                    valuation.IsActive = true;
                    valuation.CreatedBy = 1;
                    valuation.CreationDate = DateTime.Today;
                    valuation.Year = item.Year;
                    valuation.Age = (item.Year - vessel.YearOfBuild);
                    valuation.VesselTypeId = CONTAINERSHIP_TYPE;
                    valuation.FairMarketValue = (decimal)(F_CO_1 * vessel.Size + F_CO_2 * (item.Year - vessel.YearOfBuild) + F_CO_3);
                    valuation.ScrapPrice = (decimal)(S_CO_1 * vessel.Size + S_CO_2 * (item.Year - vessel.YearOfBuild) + S_CO_3);
                    valuation.OperatingCosts = (decimal)(O_CO_1 * vessel.Size + O_CO_2 * (item.Year - vessel.YearOfBuild) + O_CO_3);

                    valuationAll.Add(valuation);
                }

                return valuationAll;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
