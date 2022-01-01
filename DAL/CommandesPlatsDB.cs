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
    public class CommandesPlatsDB : ICommandesPlatsDB
    {
        private IConfiguration Configuration { get; }

        public CommandesPlatsDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public int AddQuantite(int idCommande, int idPlat, int quantite)
        {
            int result = 0;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {

                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Insert into CommandesPlats (IdCommande, IdPlat, Quantite) values (@idCommande, @idPlat, @quantite)";

                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idPlat", idPlat);
                    cmd.Parameters.AddWithValue("@idCommande", idCommande);
                    cmd.Parameters.AddWithValue("@quantite", quantite);

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

        public int UpdateQuantite(int idCommande, int idPlat)
        {
            int result = 0;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {

                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Update CommandesPlats Set Quantite = Quantite + 1 WHERE IdPlat = @idPlat AND IdCommande = @idCommande";

                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idPlat", idPlat);
                    cmd.Parameters.AddWithValue("@idCommande", idCommande);

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

        public List<CommandesPlats> GetCommandesPlats()
        {
            List<CommandesPlats> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from CommandesPlats";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<CommandesPlats>();

                            CommandesPlats commandePlat = new CommandesPlats();

                            commandePlat.IdCommande = (int)dr["IdCommande"];

                            commandePlat.IdPlat = (int)dr["IdPlat"];

                            commandePlat.Quantite = (int)dr["Quantite"];

                            results.Add(commandePlat);

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
