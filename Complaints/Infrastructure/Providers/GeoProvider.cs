﻿using Complaints.Domain.Interfaces;
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
        private const string API_GOOGLE_STRING_NAME = "GOOGLE_API";
        private readonly IConfiguration _configuration;

        public GeoProvider(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<FormDTO> getAdress(FormDTO form)
        {
            string addressToGeocode = form.adress;

            string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?key={1}&address={0}&sensor=false", Uri.EscapeDataString(addressToGeocode), _configuration.GetConnectionString(API_GOOGLE_STRING_NAME));
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(requestUri);

            var yourXml = XElement.Parse(response);
            var dict = yourXml.Descendants()
                  .Where(node => node.Name == "location")
                  .Descendants()
                  .ToDictionary(node => node.Name.ToString(), node => node.Value);

            form.lat = Convert.ToDouble(dict["lat"]);
            form.lon = Convert.ToDouble(dict["lng"]);

            return form;
        }
    }
}
