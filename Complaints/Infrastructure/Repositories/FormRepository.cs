using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Forms.Infrastructure.DTO;
using MySql.Data.MySqlClient;
using System.Text;
using Forms.Domain.Interfaces;
using Windows.UI.Xaml.Controls.Primitives;
using Forms.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

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
            using var cmd = new MySqlCommand($" INSERT INTO jkh.forms (Adress, date, mundep_UID, lat, lon, user_UID) VALUES ('{form.Adress}', curdate(), {form.MundepUID}, {form.Lat}, {form.Lon}, {form.UserUID})", connection);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task EditForm(FormDTO form)
        {
            var connection = new MySqlConnection(_configuration.GetConnectionString(CONNECTION_STRING_NAME));
            await connection.OpenAsync();
            using (MySqlCommand cmdSel = new MySqlCommand($"SELECT * FROM jkh.forms WHERE UID = {form.UID}", connection))
            {
                var reader = await cmdSel.ExecuteReaderAsync();
                reader.Read();

                form.Lat = form.Lat == 0 ? Convert.ToDouble(Encoding.UTF8.GetString(((byte[])reader["lat"]))) : form.Lat;
                form.Lon = form.Lon == 0 ? Convert.ToDouble(Encoding.UTF8.GetString(((byte[])reader["Lon"]))) : form.Lon;

                form.MundepUID = form.MundepUID == 0 ? Convert.ToInt32(reader["mundep_UID"]) : form.MundepUID;
                form.UserUID = form.UserUID == 0 ? Convert.ToInt32(reader["user_UID"]) : form.UserUID;
                form.Adress = form.Adress == "" ? Encoding.UTF8.GetString(((byte[])reader["adress"])) : form.Adress;
            }
            connection.Close();

            connection = new MySqlConnection(_configuration.GetConnectionString(CONNECTION_STRING_NAME));
            await connection.OpenAsync();
            using var cmdUpd = new MySqlCommand($"UPDATE jkh.forms t SET t.mundep_UID = '{form.MundepUID}', t.Adress = '{form.Adress}', t.lat = {form.Lat}, t.lon = {form.Lon} WHERE t.UID = {form.UID}", connection);
            await cmdUpd.ExecuteNonQueryAsync();
        }

        public async Task<FormDTO[]> GetForms(FormDTO form)
        {
            List<FormDTO> forms = new List<FormDTO>();

            using (var connection = new MySqlConnection(_configuration.GetConnectionString(CONNECTION_STRING_NAME)))
            {
                string query = $"SELECT * FROM jkh.forms where user_UID = {form.UserUID} and mundep_UID = {form.MundepUID}";
                if (form.MundepUID == 0)
                {
                    query = $"SELECT * FROM jkh.forms where user_UID = {form.UserUID}";
                }
                else if (form.UserUID == 0)
                {
                    query = $"SELECT * FROM jkh.forms where mundep_UID = {form.MundepUID}";
                }
                await connection.OpenAsync();
                using var cmd = new MySqlCommand(query, connection);
                var reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    forms.Add(new FormDTO()
                    {
                        UID = int.Parse(reader["UID"].ToString()),
                        Adress = Encoding.UTF8.GetString(((byte[])reader["adress"])),
                        Date = Convert.ToDateTime(reader["date"]),
                        MundepUID = int.Parse(reader["mundep_UID"].ToString()),
                        UserUID = int.Parse(reader["user_UID"].ToString()),
                        Lat = double.Parse(reader["lat"].ToString()),
                        Lon = double.Parse(reader["lon"].ToString())
                    });
                    Console.WriteLine(reader["lat"].ToString());

                }
            }
            return forms.ToArray();
        }
    }
}
