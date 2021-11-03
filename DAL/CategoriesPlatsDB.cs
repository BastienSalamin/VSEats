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
    public class CategoriesPlatsDB
    {
        private IConfiguration Configuration { get; }

        public CategoriesPlatsDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public List<CategoriesPlats> GetCategoriesPlats()
        {
            List<CategoriesPlats> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from CategoriesPlats";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<CategoriesPlats>();

                            CategoriesPlats categoriePlat = new CategoriesPlats();

                            categoriePlat.IdPlat = (int)dr["IdPlat"];

                            categoriePlat.IdCategorie = (int)dr["IdCategorie"];


                            results.Add(categoriePlat);

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
