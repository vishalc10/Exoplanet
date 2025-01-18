using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VishalSecondInterview
{
    public class ExoplanetData
    {
        public string PlanetIdentifier { get; set; }
        public int TypeFlag { get; set; }
        public int? DiscoveryYear { get; set; }
        public double? RadiusJpt { get; set; }
        public double? HostStarTempK { get; set; }
    }
}
