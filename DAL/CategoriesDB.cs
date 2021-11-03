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
    public class CategoriesDB
    {
        private IConfiguration Configuration { get; }

        public CategoriesDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public List<Categories> GetCategorie()
        {
            List<Categories> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Categories";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Categories>();

                            Categories categorie = new Categories();

                            categorie.IdCategorie = (int)dr["IdCategorie"];

                            categorie.Type = (string)dr["Type"];

                            if (dr["Marque"] != DBNull.Value)
                                categorie.Marque = (string)dr["Marque"];


                            results.Add(categorie);

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
