using Microsoft.Extensions.Configuration;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class PlatsDB : IPlatsDB
    {
        private IConfiguration Configuration { get; }

        public PlatsDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public double GetPrixPlat(int idPlat)
        {
            double result = 0;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    String query = "SELECT Prix FROM PLATS WHERE IdPlat = @idPlat";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@IdPlat", idPlat);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Plats plat = new Plats();

                            plat.Prix = (double)dr["Prix"];

                            result = plat.Prix;

                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        public int GetPlatID(string nom, int idRestaurant)
        {
            int result = 0;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select IdPlat from Plats p, Restaurants r WHERE p.IdRestaurant = r.IdRestaurant AND p.Nom = @nom AND r.IdRestaurant = @idRestaurant";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@idRestaurant", idRestaurant);
                    cmd.Parameters.AddWithValue("@nom", nom);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Plats plat = new Plats();

                            plat.IdPlat = (int)dr["IdPlat"];

                            result = plat.IdPlat;

                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        public string GetNomPlat(int idPlat)
        {
            string result = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    String query = "SELECT Nom FROM PLATS WHERE IdPlat = @idPlat";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@IdPlat", idPlat);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Plats plat = new Plats();

                            if (dr["Nom"] != DBNull.Value)
                                plat.Nom = (string)dr["Nom"];

                            result = plat.Nom;

                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        public List<Plats> GetPlats()
        {
            List<Plats> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Plats";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Plats>();

                            Plats plat = new Plats();

                            plat.IdPlat = (int)dr["IdPlat"];

                            plat.IdRestaurant = (int)dr["IdRestaurant"];

                            if (dr["Nom"] != DBNull.Value)
                                plat.Nom = (string)dr["Nom"];

                            if (dr["Prix"] != DBNull.Value)
                                plat.Prix = (double)dr["Prix"];

                            if (dr["Description"] != DBNull.Value)
                                plat.Description = (string)dr["Description"];

                            results.Add(plat);

                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return results;
        }

        public List<Plats> GetPlats(int idRestaurant)
        {
            List<Plats> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Plats WHERE IdRestaurant = @idRestaurant";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@IdRestaurant", idRestaurant);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Plats>();

                            Plats plat = new Plats();

                            plat.IdPlat = (int)dr["IdPlat"];

                            plat.IdRestaurant = (int)dr["IdRestaurant"];

                            if (dr["Nom"] != DBNull.Value)
                                plat.Nom = (string)dr["Nom"];

                            if (dr["Prix"] != DBNull.Value)
                                plat.Prix = (double)dr["Prix"];

                            if (dr["Description"] != DBNull.Value)
                                plat.Description = (string)dr["Description"];

                            results.Add(plat);

                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return results;
        }
    }
}
