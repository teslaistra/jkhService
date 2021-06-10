using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Topics.Domain.Entities;
using Topics.Domain.Interfaces;
using Topics.Infrastructure.DTO;
using MySql.Data.MySqlClient;
using System.Text;

namespace Topics.Infrastructure.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private const string CONNECTION_STRING_NAME = "JkhDB";

        private readonly IConfiguration _configuration;

        public TopicRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<Topic[]> GetTopics()
        {
            List<TopicDTO> topic = new List<TopicDTO>();

            using (var connection = new MySqlConnection(_configuration.GetConnectionString(CONNECTION_STRING_NAME)))
            {
                await connection.OpenAsync();
                using (var cmd = new MySqlCommand("SELECT * FROM jkh.categories", connection))
                {
                    var reader = await cmd.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        topic.Add(new TopicDTO() {
                            id = int.Parse(reader["UID"].ToString()),
                            name = Encoding.UTF8.GetString(((byte[])reader["name"])),
                            description = Encoding.UTF8.GetString(((byte[])reader["description"])) 
                        });
                        
                    }
                }
            }
            return topic.Select(e => e.ToEntity()).ToArray();
        }

        public async Task AddTopic(Topic topic)
        {
            var connection = new MySqlConnection(_configuration.GetConnectionString(CONNECTION_STRING_NAME));
            await connection.OpenAsync();
            using (var cmd = new MySqlCommand($" INSERT INTO jkh.categories (name, description) VALUES ('{topic.Name}', '{topic.Description}')", connection))
            {
                await cmd.ExecuteNonQueryAsync();
            }   
        }

        public async Task DeleteTopic(Topic topic)
        {
            var connection = new MySqlConnection(_configuration.GetConnectionString(CONNECTION_STRING_NAME));
            await connection.OpenAsync();
            using (var cmd = new MySqlCommand($" DELETE FROM jkh.categories WHERE UID = {topic.Id}", connection))
            {
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
