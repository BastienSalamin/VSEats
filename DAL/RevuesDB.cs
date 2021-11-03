using System;
using System.Collections.Generic;
using DTO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace DAL
{
    class RevuesDB
    {
        private IConfiguration Configuration { get; }

        public RevuesDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public List<Revues> GetRevues()
        {
            List<Revues> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Revues";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Revues>();

                            Revues revue = new Revues();

                            revue.IdRevue = (int)dr["IdRevue"];

                            revue.IdUtilisateur = (int)dr["IdUtilisateur"];

                            revue.IdRestaurant = (int)dr["IdRestaurant"];

                            if (dr["Etoiles"] != DBNull.Value)
                                revue.Etoiles = (int)dr["Etoiles"];

                            if (dr["Commentaire"] != DBNull.Value)
                                revue.Commentaire = (string)dr["Commentaire"];

                            revue.Date = (DateTime)dr["Date"];

                            results.Add(revue);

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
