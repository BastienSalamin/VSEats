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
    public class LocalitesDB : ILocalitesDB
    {
        private IConfiguration Configuration { get; }

        public LocalitesDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public int GetLocalite(int npa)
        {
            int result = 0;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {

                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select IdLocalite from Localites WHERE NPA = @npa";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@npa", npa);


                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            Localites localite = new Localites();

                            localite.IdLocalite = (int)dr["IdLocalite"];

                            result = localite.IdLocalite;

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

        public List<Localites> GetLocalites()
        {
            List<Localites> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Localites";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Localites>();

                            Localites localite = new Localites();

                            localite.IdLocalite = (int)dr["IdLocalite"];

                            localite.NPA = (int)dr["NPA"];

                            localite.Ville = (string)dr["Ville"];

                            results.Add(localite);

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
