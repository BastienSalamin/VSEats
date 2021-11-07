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
    public class RevuesDB : IRevuesDB
    {
        private IConfiguration Configuration { get; }

        public RevuesDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public int AddRevue(int idUtilisateur, int idRestaurant, int etoiles, string commentaire)
        {
            int result = 0;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {

                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Insert into Revues (IdUtilisateur, IdRestaurant, Etoiles, Commentaire, Date) values (@idUtilisateur, @idRestaurant, @etoiles, @commentaire, @date)";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@idUtilisateur", idUtilisateur);
                    cmd.Parameters.AddWithValue("@idRestaurant", idRestaurant);
                    cmd.Parameters.AddWithValue("@etoiles", etoiles);
                    cmd.Parameters.AddWithValue("@commentaire", commentaire);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now);


                    cn.Open();



                    result = cmd.ExecuteNonQuery();
                }


            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
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
