using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ValuationApi.Model;

namespace ValuationApi.Services.ValuationServices
{
    public interface IValuation
    {
        bool IsMatch(Vessel item);
        List<Valuation> Valuate(Vessel item, IEnumerable<TimeSeries> timeSeries);
    }
}
