using Newtonsoft.Json;

namespace VishalSecondInterview
{
    public class Exoplanet
    {
        private const string DATASET_URL = "https://gist.githubusercontent.com/joelbirchler/66cf8045fcbb6515557347c05d789b4a/raw/9a196385b44d4288431eef74896c0512bad3defe/exoplanets";

        public static async Task<List<ExoplanetData>> DownloadExoplanetData()
        {
            using var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.GetStringAsync(DATASET_URL);
                return JsonConvert.DeserializeObject<List<ExoplanetData>>(response);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error fetching data: {e.Message}");
                return new List<ExoplanetData>();
            }            
        }

        public static void DisplayOrphanPlanets(List<ExoplanetData> exoplanets)
        {
            int orphanCount = 0;
            foreach (var planet in exoplanets)
            {
                if (planet.TypeFlag == 3)
                {
                    orphanCount++;
                }
            }
            Console.WriteLine($"Number of orphan planets: {orphanCount}");
        }

        public static void DisplayPlanetOrbittingHottestStar(List<ExoplanetData> exoplanets)
        {
            ExoplanetData? hottestPlanet = null;
            double? maxTemperature = null;

            foreach (var planet in exoplanets)
            {
                if (planet.HostStarTempK.HasValue &&
                    (maxTemperature == null || planet.HostStarTempK > maxTemperature))
                {
                    maxTemperature = planet.HostStarTempK;
                    hottestPlanet = planet;
                }
            }

            if (hottestPlanet != null)
            {
                Console.WriteLine($"Planet orbiting the hottest star: {hottestPlanet.PlanetIdentifier}");
            }
            else
            {
                Console.WriteLine("No planets found with temperature data.");
            }
        }

        // This function counts null value RadiusJpt as Small
        public static void DisplayPlanetTimelineBySize(List<ExoplanetData> exoplanets)
        {
            var timeline = new Dictionary<int, Dictionary<SizeGroup, int>>(); // Year -> Size Group -> Count

            foreach (var planet in exoplanets)
            {
                if (planet.DiscoveryYear.HasValue)
                {
                    int year = planet.DiscoveryYear.Value;
                    SizeGroup sizeGroup = GetSizeGroup(planet);

                    if (!timeline.ContainsKey(year))
                    {
                        timeline[year] = new Dictionary<SizeGroup, int>
                    {
                        { SizeGroup.Small, 0 },
                        { SizeGroup.Medium, 0 },
                        { SizeGroup.Large, 0 }
                    };
                    }

                    timeline[year][sizeGroup]++;
                }
            }

            Console.WriteLine("Timeline of planets discovered per year grouped by size:");
            foreach (var year in timeline.Keys)
            {
                var yearData = timeline[year];
                Console.WriteLine($"{year}: Small = {yearData[SizeGroup.Small]}, Medium = {yearData[SizeGroup.Medium]}, Large = {yearData[SizeGroup.Large]}");
            }
        }

        public static SizeGroup GetSizeGroup(ExoplanetData planet)
        {
            if (!planet.RadiusJpt.HasValue || planet.RadiusJpt.Value < 1)
                return SizeGroup.Small;
            if (planet.RadiusJpt.Value < 2)
                return SizeGroup.Medium;
            return SizeGroup.Large;
        }

        static async Task Main(string[] args)
        {
            try
            {
                var exoplanets = await DownloadExoplanetData();
                DisplayOrphanPlanets(exoplanets);
                DisplayPlanetOrbittingHottestStar(exoplanets);
                DisplayPlanetTimelineBySize(exoplanets);
                // Please uncomment this part to see when we want to avoid null values
                // DisplayPlanetTimelineBySize2(exoplanets);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }

        // Not counting RadiusJpt when value is null
        // Please uncomment this part to see when we want to avoid null values. Uncomment commented part in SizeGroup.cs as well
        /**
        private static void DisplayPlanetTimelineBySize2(List<ExoplanetData> exoplanets)
        {
            var timeline = new Dictionary<int, Dictionary<SizeGroup, int>>(); // Year -> Size Group -> Count

            foreach (var planet in exoplanets)
            {
                if (planet.DiscoveryYear.HasValue)
                {
                    int year = planet.DiscoveryYear.Value;
                    SizeGroup sizeGroup = GetSizeGroup2(planet);

                    if(sizeGroup != SizeGroup.NoRadius){
                        if (!timeline.ContainsKey(year))
                        {
                            timeline[year] = new Dictionary<SizeGroup, int>
                            {
                                { SizeGroup.Small, 0 },
                                { SizeGroup.Medium, 0 },
                                { SizeGroup.Large, 0 }
                            };
                        }

                        timeline[year][sizeGroup]++;
                    }
                }
            }

            Console.WriteLine("Timeline of planets discovered per year grouped by size:");
            foreach (var year in timeline.Keys)
            {
                var yearData = timeline[year];
                Console.WriteLine($"{year}: Small = {yearData[SizeGroup.Small]}, Medium = {yearData[SizeGroup.Medium]}, Large = {yearData[SizeGroup.Large]}");
            }
        }

        private static SizeGroup GetSizeGroup2(ExoplanetData planet)
        {
            if(planet.RadiusJpt.HasValue){
                if (!planet.RadiusJpt.HasValue ||planet.RadiusJpt.Value < 1)
                    return SizeGroup.Small;
                if (planet.RadiusJpt.Value < 2)
                    return SizeGroup.Medium;
                return SizeGroup.Large;
            }
            return SizeGroup.NoRadius;
        }
        */
    }
}