using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Models.Route
{
    public class RouteDetailsViewModel
    {
        public BusinessLogic.Models.Route Route { get; set; }
        public List<BusinessLogic.Models.Portion> Portions { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalDuration { get; set; }
    }
}
