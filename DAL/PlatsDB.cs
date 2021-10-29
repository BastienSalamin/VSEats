using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class PlatsDB
    {
        private IConfiguration Configuration { get; }

        public PlatsDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public List<Plat> GetPlats()
        {
            List<Plat> results = null;
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
                                results = new List<Plat>();

                            Plat plat = new Plat();

                            plat.IdPlat = (int)dr["IDPLAT"];

                            if (dr["NOM"] != DBNull.Value)
                                plat.Nom = (string)dr["NOM"];

                            if (dr["PRIX"] != DBNull.Value)
                                plat.Prix = (float)dr["PRIX"];

                            if (dr["DESCRIPTION"] != DBNull.Value)
                                plat.Description = (string)dr["DESCRIPTION"];
                            
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
