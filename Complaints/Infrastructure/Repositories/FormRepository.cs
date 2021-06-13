using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Forms.Domain.Entities;
using Forms.Infrastructure.DTO;
using MySql.Data.MySqlClient;
using System.Text;
using Forms.Domain.Interfaces;

namespace Forms.Infrastructure.Repositories
{
    public class FormRepository : IFormRepository
    {
        private const string CONNECTION_STRING_NAME = "JkhDB";

        private readonly IConfiguration _configuration;

        public FormRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task AddForm(FormDTO form)
        {
            var connection = new MySqlConnection(_configuration.GetConnectionString(CONNECTION_STRING_NAME));
            await connection.OpenAsync();
            using (var cmd = new MySqlCommand($" INSERT INTO jkh.forms (adress, date, mundep_UID, lat, lon) VALUES ('{form.adress}', curdate(), {form.mundepUID}, {form.lat}, {form.lon})", connection))
            {
                await cmd.ExecuteNonQueryAsync();
            }
            Console.WriteLine(form.lat);
        }

        public async Task DeleteForm(Form form)
        {
            var connection = new MySqlConnection(_configuration.GetConnectionString(CONNECTION_STRING_NAME));
            await connection.OpenAsync();
            using (var cmd = new MySqlCommand($"DELETE FROM jkh.forms WHERE UID = {form.UID}", connection))
            {
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task EditForm(Form form)
        {   var connection = new MySqlConnection(_configuration.GetConnectionString(CONNECTION_STRING_NAME));

            await connection.OpenAsync();
            using (var cmd = new MySqlCommand($"UPDATE jkh.forms t SET t.mundep_UID = '{form.mundepUID}', t.adress = '{form.adress}' WHERE t.UID = {form.UID}", connection))
            {
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
