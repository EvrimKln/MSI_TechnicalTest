using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ValuationApi.Model;

namespace ValuationApi.Services.ValuationServices
{
    public interface ICalculator
    {
        List<Valuation> CalculateValuation(Vessel vessel, IEnumerable<TimeSeries> timeSeries);
    }
}
