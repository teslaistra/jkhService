using Complaints.Domain.Interfaces;
using Forms.Domain.Entities;
using Forms.Infrastructure.DTO;
using Geocoding;
using Geocoding.Google;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using System.Net.Http.Json;
using System.Xml;

namespace Complaints.Infrastructure.Providers
{
    public class GeoProvider : IGeoProvider
    {


            private readonly IConfiguration _configuration;

            public GeoProvider(IConfiguration configuration)
            {
                _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            }

        public async Task getAdress(Form form)
        {
            string addressToGeocode = form.adress;

            string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?key={1}&address={0}&sensor=false", Uri.EscapeDataString(addressToGeocode), "AIzaSyBF6bQvFLZ009_a4r0Y20LJOYB0hGP_iFM");
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(requestUri);
            //Console.WriteLine(response);
            var yourXml = XElement.Parse(response); // Parse the response

            var dict = yourXml.Descendants()
                  .Where(node => node.Name == "location")
                  .Descendants()
                  .ToDictionary(node => node.Name.ToString(), node => node.Value);

            var exampleUsername = dict["lat"];
            var exampleUsername2 = dict["lng"];

            Console.WriteLine(exampleUsername2);
        }
    }
}
