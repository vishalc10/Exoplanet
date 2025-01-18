using Microsoft.VisualStudio.TestPlatform.TestHost;
using VishalSecondInterview;

namespace ExoplanetTests
{
    public class UnitTest1
    {
        [Fact]
        public void GetSizeGroup_ShouldReturnSmall_WhenRadiusIsNull()
        {
            var planet = new ExoplanetData { RadiusJpt = null };
            var result = Exoplanet.GetSizeGroup(planet);
            Assert.Equal(SizeGroup.Small, result);
        }

        [Fact]
        public void GetSizeGroup_ShouldReturnSmall_WhenRadiusIsLessThan1()
        {
            var planet = new ExoplanetData { RadiusJpt = 0.6 };
            var result = Exoplanet.GetSizeGroup(planet);
            Assert.Equal(SizeGroup.Small, result);
        }

        [Fact]
        public void GetSizeGroup_ShouldReturnMedium_WhenRadiusIsBetween1And2()
        {
            var planet = new ExoplanetData { RadiusJpt = 1.2 };
            var result = Exoplanet.GetSizeGroup(planet);
            Assert.Equal(SizeGroup.Medium, result);
        }

        [Fact]
        public void GetSizeGroup_ShouldReturnLarge_WhenRadiusIsGreaterThan2()
        {
            var planet = new ExoplanetData { RadiusJpt = 5.2 };
            var result = Exoplanet.GetSizeGroup(planet);
            Assert.Equal(SizeGroup.Large, result);
        }

        [Fact]
        public void DisplayPlanetOrbittingHottestStar_ShouldDisplayPlanetWithHighestStarTemperature()
        {
            var exoplanets = new List<ExoplanetData>
            {
                new ExoplanetData { PlanetIdentifier = "PlanetA", HostStarTempK = 5000 },
                new ExoplanetData { PlanetIdentifier = "PlanetB", HostStarTempK = 6000 },
                new ExoplanetData { PlanetIdentifier = "PlanetC", HostStarTempK = 4000 }
            };

            using var sw = new System.IO.StringWriter();
            Console.SetOut(sw);

            Exoplanet.DisplayPlanetOrbittingHottestStar(exoplanets);

            var output = sw.ToString().Trim();

            Assert.Equal("Planet orbiting the hottest star: PlanetB", output);
        }

        [Fact]
        public void DisplayPlanetOrbittingHottestStar_ShouldDisplayNoPlanetsWithTemperatureData()
        {
            var exoplanets = new List<ExoplanetData>
            {
                new ExoplanetData { PlanetIdentifier = "PlanetA", HostStarTempK = null },
                new ExoplanetData { PlanetIdentifier = "PlanetB", HostStarTempK = null },
                new ExoplanetData { PlanetIdentifier = "PlanetC", HostStarTempK = null }
            };

            using var sw = new System.IO.StringWriter();
            Console.SetOut(sw);

            Exoplanet.DisplayPlanetOrbittingHottestStar(exoplanets);

            var output = sw.ToString().Trim();

            Assert.Equal("No planets found with temperature data.", output);
        }

        [Fact]
        public void DisplayPlanetTimelineBySize_ShouldGroupPlanetsBySizeAndYear()
        {
            var exoplanets = new List<ExoplanetData>
            {
                new ExoplanetData { DiscoveryYear = 2004, RadiusJpt = 0.5 },
                new ExoplanetData { DiscoveryYear = 2004, RadiusJpt = 1.4 },
                new ExoplanetData { DiscoveryYear = 2004, RadiusJpt = 4.2 },
                new ExoplanetData { DiscoveryYear = 2005, RadiusJpt = 0.3 },
                new ExoplanetData { DiscoveryYear = 2005, RadiusJpt = 1.8 }
            };

            using var sw = new System.IO.StringWriter();
            Console.SetOut(sw);

            Exoplanet.DisplayPlanetTimelineBySize(exoplanets);

            var output = sw.ToString().Trim();

            var expected = "Timeline of planets discovered per year grouped by size:\r\n" +
                           "2004: Small = 1, Medium = 1, Large = 1\r\n" +
                           "2005: Small = 1, Medium = 1, Large = 0";

            Assert.Equal(expected, output);
        }
    }
}