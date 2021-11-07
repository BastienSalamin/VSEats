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
    public class RestaurantsDB : IRestaurantsDB
    {
        private IConfiguration Configuration { get; }

        public RestaurantsDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public List<Restaurants> GetRestaurants()
        {
            List<Restaurants> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Restaurants";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Restaurants>();

                            Restaurants restaurant = new Restaurants();

                            restaurant.IdRestaurant = (int)dr["IdRestaurant"];

                            restaurant.IdLocalite = (int)dr["IdLocalite"];

                            restaurant.Nom = (string)dr["Nom"];

                            restaurant.Adresse = (string)dr["Adresse"];

                            restaurant.DateOuverture = (DateTime)dr["IdLocalite"];

                            results.Add(restaurant);

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
