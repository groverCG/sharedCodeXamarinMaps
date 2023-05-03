using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SharedCode.Models;

namespace SharedCode.Services
{
	public interface IPlaceRepository
	{
		Task<List<Place>> GetPlaces();
	}

	public class PlacesRepository : IPlaceRepository
	{
        public async Task<List<Place>> GetPlaces()
        {
            //var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalizedResources), "places.json");
			var assembly = IntrospectionExtensions.GetTypeInfo(typeof(PlacesRepository)).Assembly;
			Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.places.json");
            //using (StreamReader r = new StreamReader(path))
            using (StreamReader r = new StreamReader(stream))
            {
				string json = await r.ReadToEndAsync();
				List<Place> places = JsonConvert.DeserializeObject<List<Place>>(json);
				return places;
			}
        }
    }
}

