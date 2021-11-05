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
                                plat.Prix = (float)dr["Prix"];

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
