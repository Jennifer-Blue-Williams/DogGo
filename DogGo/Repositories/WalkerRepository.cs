﻿using DogGo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public class WalkerRepository : IWalkerRepository
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public WalkerRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public List<Walker> GetAllWalkers()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT w.Id, w.Name as 'Name', w.ImageUrl, n.Name as 'Hood', n.Id as 'hoodId'
                        FROM Walker w
                        Join Neighborhood n on n.Id = w.NeighborhoodId 
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Walker> walkers = new List<Walker>();
                        while (reader.Read())
                        {
                            Walker walker = new Walker
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                                 Neighborhood = new Neighborhood()
                                {
                                    Name = reader.GetString(reader.GetOrdinal("Hood")),
                                   Id = reader.GetInt32(reader.GetOrdinal("HoodId"))
                                }
                            };
                            walkers.Add(walker);
                        }

                        return walkers;
                    }
                }
            }
        }

        public Walker GetWalkerById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, [Name], ImageUrl, NeighborhoodId
                        FROM Walker
                        WHERE Id = @id
                    ";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Walker walker = new Walker
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                                NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),                            
                            };

                            reader.Close();
                            return walker;
                        }
                        else
                        {
                            reader.Close();
                            return null;
                        }
                    }
                }
            }
        }

        public void UpdateWalker(object walker)
        {
            throw new System.NotImplementedException();
        }
        public List<Walker> GetWalkersInNeighborhood(int neighborhoodId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT Id, [Name], ImageUrl, NeighborhoodId
                FROM Walker
                WHERE NeighborhoodId = @neighborhoodId
            ";

                    cmd.Parameters.AddWithValue("@neighborhoodId", neighborhoodId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        List<Walker> walkers = new List<Walker>();
                        while (reader.Read())
                        {
                            Walker walker = new Walker
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                                NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId"))
                            };

                            walkers.Add(walker);
                        }

                        return walkers;
                    }
                }
            }
        }
        public List<Walker> GetAllWalkersInHoodByOwnerId(int ownerNeighborhoodId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT distinct w.Id, w.Name as 'Name', w.ImageUrl, n.Name as 'Hood', n.Id as 'hoodId'
                        FROM Walker w
                        Join Neighborhood n on n.Id = w.NeighborhoodId
                        Join Owner o on o.NeighborhoodId = n.Id
                        WHERE w.NeighborhoodId = @ownerNeighborhoodId
                    ";

                    cmd.Parameters.AddWithValue("@ownerNeighborhoodId", ownerNeighborhoodId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Walker> walkers = new List<Walker>();
                        while (reader.Read())
                        {
                            Walker walker = new Walker
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                                Neighborhood = new Neighborhood()
                                {
                                    Name = reader.GetString(reader.GetOrdinal("Hood")),
                                    Id = reader.GetInt32(reader.GetOrdinal("HoodId"))
                                }
                            };

                            walkers.Add(walker);
                        }

                        return walkers;
                    }
                }
            }
        }
    }
}
